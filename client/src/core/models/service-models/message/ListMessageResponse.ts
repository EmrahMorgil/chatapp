import mdlMessageDto from "../../../dto/MessageDto";
import mdlMessage from "../../Message";
import mdlDataResponse from "../DataResponse";

export class mdlListMessageResponse extends mdlDataResponse<Array<mdlMessageDto>> { }
