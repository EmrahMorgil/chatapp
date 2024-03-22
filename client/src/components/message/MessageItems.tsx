import React from "react";
import moment from "moment";
import mdlUser from "../../core/models/User";
import mdlMessageDto from "../../core/dto/MessageDto";
import CookieManager from "../helpers/CookieManager";


class mdlMessageItemProps {
  message?: mdlMessageDto;
}

const MessageItems: React.FC<mdlMessageItemProps> = (props) => {

  var activeUser: mdlUser = JSON.parse(CookieManager.getCookie("activeUser")!);

  return (
    <div className="d-flex flex-column m-1">
      {
        <div className={`${props.message?.senderUser?.id === activeUser.id ? "align-self-end" : "align-self-start"}`} style={{ backgroundColor: "#202C33", padding: "1rem", borderRadius: "10%" }}>
          <span style={{ marginRight: "9px", whiteSpace: "pre-wrap" }}>
            {props.message?.content?.length! > 50
              ? props.message?.content?.match(/.{1,50}/g)!.map((m, index) => (
                <React.Fragment key={index}>
                  {index > 0 && <br />}
                  {m}
                </React.Fragment>
              ))
              : props.message?.content}
          </span>
          <span style={{ color: "grey" }}>{moment(props.message?.createdDate).format("HH:mm")}</span>
        </div>
      }
    </div>

  );
};

export default MessageItems;
