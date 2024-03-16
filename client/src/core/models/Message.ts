import Base from "./Base";

export default class mdlMessage extends Base{
    senderId?: string;
    content?: string;
    room?: string;
}