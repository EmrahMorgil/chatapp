
import axios from "axios";
import { MessageDto } from "../Core/Modals/Dto/MessageDto";
import { GetMessagesDto } from "../Core/Modals/Dto/GetMessagesDto";
import { HandleLogout } from "../components/helpers/HandleLogout";

export const getMessages = async (getMessages: GetMessagesDto, token: string) => {
    try {
        return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/Message/GetMessages`, getMessages, { headers: { "Authorization": `Bearer ${token}` } }).then(res => res.data);
    } catch (error) {
        HandleLogout();
    }
}

export const addMessage = async (message: MessageDto, token: string) => {
    try {
        return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/Message/AddMessage`, message, { headers: { "Authorization": `Bearer ${token}` } }).then((res => res.data));
    } catch (error) {
        HandleLogout();
    }
}
