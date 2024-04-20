export default class mdlCreateMessageRequest {
    content: string;
    room: string;
    constructor(pContent: string, pRoom: string) {
        this.content = pContent;
        this.room = pRoom;
    }
}