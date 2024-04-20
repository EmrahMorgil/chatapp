export default class mdlUpdateUserRequest {
    email?: string;
    name?: string;
    oldPassword?: string;
    newPassword?: string;
    newPasswordVerify?: string;
    image?: string;

    constructor(pEmail: string = '', pName: string = '', pOldPassword: string = '', pNewPassword: string = '', pNewPasswordVerify: string = '', pImage: string = '') {
        this.email = pEmail;
        this.name = pName;
        this.oldPassword = pOldPassword;
        this.newPassword = pNewPassword;
        this.newPasswordVerify = pNewPasswordVerify;
        this.image = pImage;
    }
}