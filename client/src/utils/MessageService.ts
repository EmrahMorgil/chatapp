import mdlCreateMessageRequest from "../core/models/service-models/message/CreateMessageRequest";
import mdlCreateMessageResponse from "../core/models/service-models/message/CreateMessageResponse";
import { mdlListMessageRequest } from "../core/models/service-models/message/ListMessageRequest";
import { mdlListMessageResponse } from "../core/models/service-models/message/ListMessageResponse";
import ApiClient from "./ApiClient";

module MessageService {
  function servicePath(): string {
    return "message/";
  }
  export const List = async (
    req: mdlListMessageRequest,
    token: string
  ): Promise<mdlListMessageResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "list", req, { headers: { "Authorization": token } });
    var cResponse = response as mdlListMessageResponse;
    return cResponse;
  };

  export const Create = async (
    req: mdlCreateMessageRequest,
    token: string
  ): Promise<mdlCreateMessageResponse> => {
    const response = await ApiClient.PostAsync(servicePath() + "create", req, { headers: { "Authorization": token } });
    var cResponse = response as mdlCreateMessageResponse;
    return cResponse;
  };
}

export default MessageService;
