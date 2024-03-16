export default class mdlUpdateUserRequest {
    id?: string;
    email?: string;
    name?: string;
    oldPassword?: string;
    newPassword?: string;
    newPasswordVerify?: string;
    image?: string;

    constructor(pId: string = '', pEmail: string = '', pName: string = '', pOldPassword: string = '', pNewPassword: string = '', pNewPasswordVerify: string = '', pImage: string = '') {
        this.id = pId;
        this.email = pEmail;
        this.name = pName;
        this.oldPassword = pOldPassword;
        this.newPassword = pNewPassword;
        this.newPasswordVerify = pNewPasswordVerify;
        this.image = pImage;
    }
}