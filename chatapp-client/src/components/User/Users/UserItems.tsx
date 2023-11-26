import React from "react";
import { UserViewDto } from "../../../Core/Modals/Dto/UserViewDto";

interface UserItemsProps {
  item?: UserViewDto;
  fnGetMessages?: Function;
}

const UserItems: React.FC<UserItemsProps> = (props) => {

  const fnGetMessages = (user: UserViewDto) => {
    localStorage.setItem("takerUser", JSON.stringify({ id: user.id, name: user.name, image: user.image }));
    props.fnGetMessages!(props.item?.id)
  }

  return (
    <div id={"id" + String(props.item?.id)} className="user" style={{ padding: "20px" }} onClick={() => fnGetMessages(props.item!)}>
      <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={props.item?.image} alt="" width={"70px"} height={"70px"} />
        <div className="d-flex flex-column justify-content-center">
          <div style={{ color: "white", padding: "0px", margin: "0px" }}>{props.item?.name} <span className={`ms-2 ${props.item?.status ? "online-point" : "offline-point"}`}></span></div>
        </div>
      </div>
    </div>
  );
};

export default UserItems;
