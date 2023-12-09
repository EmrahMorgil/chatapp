import React from "react";
import { mdlMessage } from "../../../Core/Modals/Message";
import MessageItems from "./MessageItems";
import { mdlUser } from "../../../Core/Modals/User";

interface MessagesProps {
  messages?: mdlMessage[];
  scrollToBottom?: Function;
}

const Messages: React.FC<MessagesProps> = (props) => {

  const takerUser = localStorage.getItem("takerUser");
  const activeTakerUserParse: mdlUser = takerUser ? JSON.parse(takerUser) : null;



  React.useEffect(() => {
    if (activeTakerUserParse)
      props.scrollToBottom!();
  }, [activeTakerUserParse, props.messages])


  if (!activeTakerUserParse)
    return <div className="message-scroll p-4 d-flex justify-content-center align-items-center custom-heigth"
      style={{ backgroundColor: "#0C1317", color: "white" }}>
      <h5>Send a message!</h5>
    </div>

  return (
    <div id="messages-container" className="message-scroll p-4 custom-heigth message-background" style={{ color: "white" }}>
      {props.messages?.map((i: mdlMessage, key) => {
        return <MessageItems message={i} key={key} />;
      })}
    </div>
  );
};

export default Messages;
