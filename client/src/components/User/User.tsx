import React from 'react'
import UserBar from './UserBar/UserBar'
import Users from './Users/Users'
import { UserViewDto } from '../../Core/Modals/Dto/UserViewDto'
import { HandleLogout } from '../helpers/HandleLogout';
import { mdlUser } from '../../Core/Modals/User';


interface UserProps {
  fnGetMessages?: Function;
  users?: UserViewDto[];
}


const User: React.FC<UserProps> = (props) => {

  var activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);
  var takerUser: mdlUser = JSON.parse(localStorage.getItem("takerUser")!);

  const fnGetMessages = (user: UserViewDto) => {
    localStorage.setItem("takerUser", JSON.stringify({ id: user.id, name: user.name, image: user.image }));
    props.fnGetMessages!(user.id)

    var unreadUsers: string[] = JSON.parse(localStorage.getItem("unreadUsers")!)
    if(unreadUsers){
      var newUnreadUsers = unreadUsers.filter((i)=>i!=user.id);
      localStorage.setItem("unreadUsers", JSON.stringify(newUnreadUsers));
      document.getElementById("u"+user.id!)?.classList.add("d-none");
      document.getElementById(user.id!)?.classList.remove("d-none");
    }
  }

  return (
    <div className='col-xl-3 col-md-3' style={{margin: "0px", padding: "0px"}}>

    <div className='d-none d-sm-block' style={{ height: "50rem", backgroundColor: "#111B21", padding: "0px", margin: "0px" }}>
      <UserBar />
      <Users users={props.users} fnGetMessages={props.fnGetMessages} />


    </div>
    <div className="dropdown d-lg-none d-md-none d-flex">
        <button className="btn btn-dark dropdown-toggle w-50" id="showUsers" data-bs-toggle="dropdown" aria-expanded="false">
        <span className="" aria-hidden="true">{takerUser ? takerUser.name : "Users"}</span>
        </button>
        

        <div className="dropdown w-50">
        <button className="btn btn-secondary dropdown-toggle w-100" id="dropdownMenuButton3" data-bs-toggle="dropdown" aria-expanded="false">
        <span className="rotate" aria-hidden="true">⚙️</span>
        </button>
        <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton3">
          <li><button className='dropdown-item btn'>Profile</button></li>
          <li><button className="dropdown-item btn" onClick={HandleLogout}>Logout</button></li>
        </ul>
      </div>


        <ul className="dropdown-menu" aria-labelledby="showUsers">
          {/* <li><button className='dropdown-item btn' onClick={()=>window.location.href = `${process.env.REACT_APP_BASE_URL}/profile`}>Profile</button></li> */}
          <li>
            {props.users?.map((u: UserViewDto)=>{
              if(u.id !== activeUser.id)
              return <button key={u.id} className="dropdown-item btn" onClick={()=>fnGetMessages(u)}>{u.name}</button>
            }
            )}
          </li>
        </ul>
      </div>
    </div>
  )
}

export default User;