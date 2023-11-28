import React from "react";
import { mdlMessage } from "../../../Core/Modals/Message";
import { mdlUser } from "../../../Core/Modals/User";
import { addMessage } from "../../../services/messageService";
import { MessageDto } from "../../../Core/Modals/Dto/MessageDto";

interface MessageInputProps {
  scrollToBottom?: Function;
}

const MessageInput: React.FC<MessageInputProps> = (props) => {

  const [inputValue, setInputValue] = React.useState<string>("");
  const takerUser = localStorage.getItem("takerUser");
  const activeTakerUserParse: mdlUser = takerUser ? JSON.parse(takerUser) : null;

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    var token = localStorage.getItem("token");

    if (inputValue.trim() !== "" && token) {
      var activeUser = localStorage.getItem("activeUser");
      var room = localStorage.getItem("room");
      var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

      let newMessage = new MessageDto();
      newMessage.message = inputValue;
      newMessage.senderId = activeUserParse.id;
      newMessage.room = room ?? "";
      newMessage.senderUserName = activeUserParse.name;
      await addMessage(newMessage, token);
      setInputValue("");
      props.scrollToBottom!();
    }else{
      setInputValue("");
    }
  }


  return (
    <div style={{ backgroundColor: "#202C33", padding: "10px", justifyContent: "center", alignItems: "center", alignSelf: "flex-end", display: activeTakerUserParse ? "flex" : "none" }} className="gap-3">
      <svg xmlns="http://www.w3.org/2000/svg" height="30px" viewBox="0 0 512 512"><path style={{ fill: "#ffffff" }} d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM96.8 314.1c-3.8-13.7 7.4-26.1 21.6-26.1H393.6c14.2 0 25.5 12.4 21.6 26.1C396.2 382 332.1 432 256 432s-140.2-50-159.2-117.9zM144.4 192a32 32 0 1 1 64 0 32 32 0 1 1 -64 0zm192-32a32 32 0 1 1 0 64 32 32 0 1 1 0-64z" /></svg>
      <svg xmlns="http://www.w3.org/2000/svg" height="30px" viewBox="0 0 448 512"><path style={{ fill: "#ffffff" }} d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" /></svg>

      <form style={{ width: "100%", height: "40px" }} onSubmit={handleSubmit}>
        <input type="text" value={inputValue} style={{ width: "100%", height: "40px" }} onChange={(e: any) => setInputValue(e.target.value)} />
      </form>
    </div>
  );
};

export default MessageInput;
