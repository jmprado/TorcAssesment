SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Joao Prado
-- Create date: 2023-06-22
-- Description:	Create a new order 
-- =============================================
ALTER PROCEDURE CreateOrder
	-- Add the parameters for the stored procedure here
	@productId int,
	@customerId int,
	@quantity int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @productPrice NUMERIC(18, 2);
	DECLARE @orderTotal NUMERIC(18, 2);

    -- Insert statements for procedure here
	SELECT @productPrice = Price FROM Product WHERE Id = @productId;

	SET @orderTotal = CONVERT(decimal(18,2), @productPrice * @quantity);

	INSERT INTO [Order] (CustomerId, ProductId, Total) 
	VALUES(@customerId, @productId, @orderTotal);

END
GO
