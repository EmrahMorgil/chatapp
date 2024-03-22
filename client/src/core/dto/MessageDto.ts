import mdlUserDto from "./UserDto";

export default class mdlMessageDto {
    id?: string;
    createdDate?: string;
    senderUser?: mdlUserDto;
    content?: string;
    room?: string;
}
