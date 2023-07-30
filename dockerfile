FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . /app

WORKDIR /app/src
RUN ["dotnet", "restore"]

WORKDIR /app/src/Torc.Assesment.Api

FROM build AS publish
RUN dotnet publish "Torc.Assesment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM microsoft/mssql-server-windows-express
COPY /app/create-db.sql .
ENV ACCEPT_EULA Y
ENV SA_PASSWORD jmlcol
RUN sqlcmd -i torc_assesment_db.sql

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Torc.Assesment.Api.dll"]