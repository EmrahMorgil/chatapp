export default class mdlLoginUserRequest {
  email: string;
  password: string;

  constructor(pEmail: string = '', pPassword: string = "") {
    this.email = pEmail;
    this.password = pPassword;
  }

}
