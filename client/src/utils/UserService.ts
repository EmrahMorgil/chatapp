import CookieManager from "../components/helpers/CookieManager";
import mdlCreateUserRequest from "../core/models/service-models/user/CreateUserRequest";
import mdlCreateUserResponse from "../core/models/service-models/user/CreateUserResponse";
import { mdlDetailUserRequest } from "../core/models/service-models/user/DetailUserRequest";
import mdlDetailUserResponse from "../core/models/service-models/user/DetailUserResponse";
import mdlListUserRequest from "../core/models/service-models/user/ListUserRequest";
import mdlListUserResponse from "../core/models/service-models/user/ListUserResponse";
import mdlLoginUserRequest from "../core/models/service-models/user/LoginUserRequest";
import mdlLoginUserResponse from "../core/models/service-models/user/LoginUserResponse";
import mdlUpdateUserRequest from "../core/models/service-models/user/UpdateUserRequest";
import mdlUpdateUserResponse from "../core/models/service-models/user/UpdateUserResponse";
import ApiClient from "./ApiClient";

module UserService {
  const token = CookieManager.getCookie("token") as string;
  function servicePath(): string {
    return "user/";
  }
  export const Create = async (
    req: mdlCreateUserRequest
  ): Promise<mdlCreateUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "create", req);
    var cResponse = response as mdlCreateUserResponse;
    return cResponse;
  };

  export const Update = async (
    req: mdlUpdateUserRequest
  ): Promise<mdlUpdateUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "update", req, { headers: { "Authorization": token } });
    var cResponse = response as mdlUpdateUserResponse;
    return cResponse;
  };

  export const List = async (
    req: mdlListUserRequest
  ): Promise<mdlListUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "list", req, { headers: { "Authorization": token } });
    var cResponse = response as mdlListUserResponse;
    return cResponse;
  };

  export const Login = async (
    req: mdlLoginUserRequest
  ): Promise<mdlLoginUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "login", req);
    var cResponse = response as mdlLoginUserResponse;
    return cResponse;
  };

  export const Detail = async (
    req: mdlDetailUserRequest
  ): Promise<mdlDetailUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "detail", req, {headers: { "Authorization": token}});
    var cResponse = response as mdlDetailUserResponse;
    return cResponse;
  };
}

export default UserService;
