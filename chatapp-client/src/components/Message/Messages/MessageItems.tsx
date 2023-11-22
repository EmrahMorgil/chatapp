import React from "react";
import { MessageItemProps } from "../../../Core/Props/MessageItemProps";
import { mdlUser } from "../../../Core/Modals/User";
import moment from "moment";

const MessageItems: React.FC<MessageItemProps> = (props) => {
  var activeUser = localStorage.getItem("activeUser");
  var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

  return (
    <div className="d-flex flex-column m-1">
      {
        <div className={`${props.message?.senderId === activeUserParse.id ? "align-self-end" : "align-self-start"}`} style={{ backgroundColor: "#202C33", padding: "10px", borderRadius: "10%" }}>
          <span style={{ marginRight: "9px" }}>{props.message?.message}</span>
          <span style={{ color: "grey" }}>{moment(props.message?.createdDate).format("HH:mm")}</span>
        </div>
      }
    </div>

  );
};

export default MessageItems;
