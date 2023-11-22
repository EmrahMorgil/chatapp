import React from "react";
import { mdlUser } from "../../../Core/Modals/User";

interface UserItemsProps {
  item?: mdlUser;
  fnGetMessages?: Function;
}

const UserItems: React.FC<UserItemsProps> = (props) => {

  const fnGetMessages = (user: mdlUser) => {
    localStorage.setItem("takerUser", JSON.stringify({ image: user.image, id: user.id, name: user.name }));
    props.fnGetMessages!(props.item?.id)
  }

  return (
    <div id={"id" + String(props.item?.id)} className="user" style={{ padding: "20px" }} onClick={() => fnGetMessages(props.item!)}>
      <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={props.item?.image} alt="" width={"70px"} height={"70px"} />
        <div className="d-flex flex-column justify-content-center">
          <p style={{ color: "white", padding: "0px", margin: "0px" }}>{props.item?.name}</p>
          {/* <p style={{ color: "grey", padding: "0px", marginTop: "0px" }}>{props.item?.lastMessage}</p> */}
        </div>
      </div>
    </div>
  );
};

export default UserItems;
