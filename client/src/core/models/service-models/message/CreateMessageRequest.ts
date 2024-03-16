export default class mdlCreateMessageRequest {
    senderId: string;
    content: string;
    room: string;
    constructor(pSenderId: string, pContent: string, pRoom: string) {
        this.senderId = pSenderId;
        this.content = pContent;
        this.room = pRoom;
    }
}