
import axios from "axios";
import { MessageDto } from "../Core/Modals/Dto/MessageDto";
import { GetMessagesDto } from "../Core/Modals/Dto/GetMessagesDto";

export const getMessages = async (getMessages: GetMessagesDto) => {
    const response = await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/Message/GetMessages`, getMessages);
    return response.data;
}

export const addMessage = async (message: MessageDto) => {
    const response = await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/Message/AddMessage`, message);
    return response.data;
}



