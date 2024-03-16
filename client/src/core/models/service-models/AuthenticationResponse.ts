import mdlUser from "../User";
import mdlDataResponse from "./DataResponse";

export default class mdlAuthenticationResponse extends mdlDataResponse<mdlUser>{
    token?: string;
}