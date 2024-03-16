import mdlBaseResponse from "./BaseResponse";

export default class mdlDataResponse<T> extends mdlBaseResponse {
  body?: T;
}
