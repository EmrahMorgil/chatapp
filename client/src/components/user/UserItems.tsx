import React from "react";
import getUserImage from "../helpers/ImageHelper";
import mdlUserDto from "../../core/dto/UserDto";

interface IUserItemsProps {
  item?: mdlUserDto;
  getMessages?: Function;
}

const UserItems: React.FC<IUserItemsProps> = (props) => {

  const fnGetMessages = (user: mdlUserDto) => {

    const takerUser: mdlUserDto = JSON.parse(sessionStorage.getItem("takerUser")!);

    if (takerUser?.id !== user.id) {
      sessionStorage.setItem("takerUser", JSON.stringify(user));
      props.getMessages!(user.id);
      document.getElementById(props.item?.id!)?.classList.add("d-none");
      document.getElementById("u" + props.item?.id!)?.classList.remove("d-none");
    }
  }

  return (
    <div id={"id" + props.item?.id} className="user" style={{ padding: "20px" }} onClick={() => fnGetMessages(props.item!)}>
      <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={getUserImage(props.item?.image)} alt="" width={"70px"} height={"70px"} />
        <div className="d-flex flex-column justify-content-center">
          <div className="d-flex align-items-center" style={{ color: "white", padding: "0px", margin: "0px" }}><span>{props.item?.name}</span>
          <span id={props.item?.id} className={`ms-3 unread-message-point d-none`}></span>
            <span id={"u" + props.item?.id} className={`ms-2 ${props.item?.status ? "online-point" : "offline-point"}`}></span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserItems;
