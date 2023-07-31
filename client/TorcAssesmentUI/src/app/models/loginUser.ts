export class LoginUser{
    username!: string;
    password!: string;

    public constructor(init?: Partial<LoginUser>) {
        Object.assign(this, init);
    }
}