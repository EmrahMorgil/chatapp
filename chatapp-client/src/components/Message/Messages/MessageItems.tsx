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
        <div className={`${props.message?.senderId === activeUserParse.id ? "align-self-end" : "align-self-start"}`} style={{ backgroundColor: "#202C33", padding: "1rem", borderRadius: "10%" }}>
          {/* <span style={{ marginRight: "9px" }}>{props.message?.message}</span> */}
          <span style={{ marginRight: "9px", whiteSpace: "pre-wrap" }}>
              {props.message?.message?.length! > 50
                ? props.message?.message?.match(/.{1,50}/g)!.map((m, index) => (
                    <React.Fragment key={index}>
                      {index > 0 && <br />} 
                      {m}
                    </React.Fragment>
                  ))
                : props.message?.message}
          </span>
          <span style={{ color: "grey" }}>{moment(props.message?.createdDate).format("HH:mm")}</span>
        </div>
      }
    </div>

  );
};

export default MessageItems;
