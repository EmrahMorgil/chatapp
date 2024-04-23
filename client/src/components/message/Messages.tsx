import React from "react";
import MessageItems from "./MessageItems";
import mdlUser from "../../core/models/User";
import mdlMessageDto from "../../core/dto/MessageDto";
import mdlUserDetailDto from "../../core/dto/UserDetailDto";

interface IMessagesProps {
  messages?: mdlMessageDto[];
  scrollToBottom?: Function;
  activeUser?: mdlUserDetailDto;
}

const Messages: React.FC<IMessagesProps> = (props) => {

  const takerUser: mdlUser = JSON.parse(sessionStorage.getItem("takerUser")!);

  React.useEffect(() => {
    if (takerUser)
      props.scrollToBottom!();
  }, [takerUser, props.messages])


  if (!takerUser)
    return <div className="message-scroll p-4 d-flex justify-content-center align-items-center custom-heigth"
      style={{ backgroundColor: "#0C1317", color: "white" }}>
      <h5>Send a message!</h5>
    </div>

  return (
    <div id="messages-container" className="message-scroll p-4 custom-heigth message-background" style={{ color: "white" }}>
      {props.messages?.map((i: mdlMessageDto, key) => {
        return <MessageItems message={i} key={key} activeUser={props.activeUser}/>;
      })}
    </div>
  );
};

export default Messages;
