import mdlUserDto from "../dto/UserDto";

export default class mdlNotification {
    // usersIds?: Array<string>;
    // userName?: string;
    // lastUserId?: string;
    // image?: string;
    // status?: string;
    onlineUsers?: Array<mdlUserDto>;
    user?: mdlUserDto;
    status?: string;
}