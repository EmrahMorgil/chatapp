export default class mdlCreateUserRequest {
    email: string;
    name: string;
    password: string;
    image: string;

    constructor(pEmail: string = '', pName: string = '', pPassword: string = '', pImage: string = '') {
        this.email = pEmail;
        this.name = pName;
        this.password = pPassword;
        this.image = pImage;
    }
}