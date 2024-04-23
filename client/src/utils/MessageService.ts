import CookieManager from "../components/helpers/CookieManager";
import mdlCreateMessageRequest from "../core/models/service-models/message/CreateMessageRequest";
import mdlCreateMessageResponse from "../core/models/service-models/message/CreateMessageResponse";
import { mdlListMessageRequest } from "../core/models/service-models/message/ListMessageRequest";
import { mdlListMessageResponse } from "../core/models/service-models/message/ListMessageResponse";
import ApiClient from "./ApiClient";

module MessageService {
  const token = CookieManager.getCookie("token") as string;
  function servicePath(): string {
    return "message/";
  }
  export const List = async (
    req: mdlListMessageRequest
  ): Promise<mdlListMessageResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "list", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response as mdlListMessageResponse;
  };

  export const Create = async (
    req: mdlCreateMessageRequest
  ): Promise<mdlCreateMessageResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "create", {
      method: "POST",
      body: JSON.stringify(req),
      headers: { "Content-Type": "application/json", Authorization: token },
    });
    return response as mdlCreateMessageResponse;
  };
}

export default MessageService;
