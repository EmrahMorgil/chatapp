import React from "react";
import moment from "moment";
import mdlMessageDto from "../../core/dto/MessageDto";
import mdlUserDetailDto from "../../core/dto/UserDetailDto";


interface IMessageItemsProps {
  message?: mdlMessageDto;
  activeUser?: mdlUserDetailDto;
}

const MessageItems: React.FC<IMessageItemsProps> = (props) => {

  return (
    <div className="d-flex flex-column m-1">
      {
        <div className={`${props.message?.senderUser?.id === props.activeUser?.id ? "align-self-end" : "align-self-start"}`} style={{ backgroundColor: "#202C33", padding: "1rem", borderRadius: "10%" }}>
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
