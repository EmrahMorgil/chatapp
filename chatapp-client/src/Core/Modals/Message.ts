import { Base } from "./Base";

export class mdlMessage extends Base {
    senderId?: string;
    message?: string;
    room?: string;
}