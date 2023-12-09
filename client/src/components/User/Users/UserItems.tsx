import React from "react";
import { UserViewDto } from "../../../Core/Modals/Dto/UserViewDto";
import { mdlUser } from "../../../Core/Modals/User";

interface UserItemsProps {
  item?: UserViewDto;
  fnGetMessages?: Function;
}

const UserItems: React.FC<UserItemsProps> = (props) => {

  const fnGetMessages = (user: UserViewDto) => {
    
    const takerUser: mdlUser | null = JSON.parse(localStorage.getItem("takerUser")!) ?? null;

    if(takerUser?.id !== user.id){

      
      localStorage.setItem("takerUser", JSON.stringify({ id: user.id, name: user.name, image: user.image }));
      props.fnGetMessages!(props.item?.id)
      
      var unreadUsers: string[] = JSON.parse(localStorage.getItem("unreadUsers")!)
      if(unreadUsers){
        var newUnreadUsers = unreadUsers.filter((i)=>i!=user.id);
        localStorage.setItem("unreadUsers", JSON.stringify(newUnreadUsers));
        document.getElementById(props.item?.id!)?.classList.add("d-none");
        document.getElementById("u"+props.item?.id!)?.classList.remove("d-none");
      }
      
    }
  }
  var unreadMessageControl =  JSON.parse(localStorage.getItem("unreadUsers")!) && JSON.parse(localStorage.getItem("unreadUsers")!).some((i: string)=>i==props.item?.id);

  return (
    <div id={"id" + String(props.item?.id)} className="user" style={{ padding: "20px" }} onClick={() => fnGetMessages(props.item!)}>
      <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={props.item?.image} alt="" width={"70px"} height={"70px"} />
        <div className="d-flex flex-column justify-content-center">
          <div className="d-flex align-items-center" style={{ color: "white", padding: "0px", margin: "0px" }}><span>{props.item?.name}</span> 
          <span id={props.item?.id} className={`ms-3 unread-message-point ${unreadMessageControl ? "" : "d-none"}`}></span>
         <span id={"u"+props.item?.id} className={`ms-2 ${props.item?.status ? "online-point" : "offline-point"} ${unreadMessageControl ? "d-none" : ""}`}></span>
          </div>
         
        </div>
      </div>
    </div>
  );
};

export default UserItems;
