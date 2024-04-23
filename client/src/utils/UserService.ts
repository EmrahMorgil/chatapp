import CookieManager from "../components/helpers/CookieManager";
import mdlCreateUserRequest from "../core/models/service-models/user/CreateUserRequest";
import mdlCreateUserResponse from "../core/models/service-models/user/CreateUserResponse";
import mdlDetailUserResponse from "../core/models/service-models/user/DetailUserResponse";
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
    const response = await ApiClient.PostAsync(servicePath() + "create", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    var cResponse = response as mdlCreateUserResponse;
    return cResponse;
  };

  export const Update = async (
    req: mdlUpdateUserRequest
  ): Promise<mdlUpdateUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "update", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    var cResponse = response as mdlUpdateUserResponse;
    return cResponse;
  };

  export const Login = async (
    req: mdlLoginUserRequest
  ): Promise<mdlLoginUserResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "login", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json" },
    });
    var cResponse = response as mdlLoginUserResponse;
    return cResponse;
  };

  export const List = async (): Promise<mdlListUserResponse> => {
    const response = await ApiClient.GetAsync(servicePath() + "list", {
      method: "GET",
      headers: { Authorization: token },
    });
    return response as mdlListUserResponse;
  };

  export const Detail = async (): Promise<mdlDetailUserResponse> => {
    const response = await ApiClient.GetAsync(servicePath() + "detail", {
      method: "GET",
      headers: { Authorization: token },
    });
    return response as mdlDetailUserResponse;
  };
}

export default UserService;
