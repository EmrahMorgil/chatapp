import React from 'react'
import UserBar from './UserBar/UserBar'
import Users from './Users/Users'
import { UserViewDto } from '../../Core/Modals/Dto/UserViewDto'
import { HandleLogout } from '../helpers/HandleLogout';


interface UserProps {
  fnGetMessages?: Function;
  users?: UserViewDto[];
}


const User: React.FC<UserProps> = (props) => {

  const fnGetMessages = (user: UserViewDto) => {
    localStorage.setItem("takerUser", JSON.stringify({ id: user.id, name: user.name, image: user.image }));
    props.fnGetMessages!(user.id)

    var unreadUsers: string[] = JSON.parse(localStorage.getItem("unreadUsers")!)
    if(unreadUsers){
      var newUnreadUsers = unreadUsers.filter((i)=>i!=user.id);
      localStorage.setItem("unreadUsers", JSON.stringify(newUnreadUsers));
      document.getElementById(user.id!)?.classList.remove("block");
      document.getElementById("u"+user.id!)?.classList.add("block");
    }
  }

  return (
    <div className='col-xl-3 col-md-3' style={{margin: "0px", padding: "0px"}}>

    <div className='d-none d-sm-block' style={{ height: "50rem", backgroundColor: "#111B21", padding: "0px", margin: "0px" }}>
      <UserBar />
      <Users users={props.users} fnGetMessages={props.fnGetMessages} />


    </div>
    <div className="dropdown d-lg-none d-md-none ms-5">
        <button className="btn btn-secondary dropdown-toggle" id="showUsers" data-bs-toggle="dropdown" aria-expanded="false">
        <span className="" aria-hidden="true">Users</span>
        </button>
        <button className='btn btn-danger' onClick={HandleLogout}>Logout</button>
        <ul className="dropdown-menu" aria-labelledby="showUsers">
          {/* <li><button className='dropdown-item btn' onClick={()=>window.location.href = `${process.env.REACT_APP_BASE_URL}/profile`}>Profile</button></li> */}
          <li>
            {props.users?.map((u: UserViewDto)=>
            <button key={u.id} className="dropdown-item btn" onClick={()=>fnGetMessages(u)}>{u.name}</button>
            )}
          </li>
        </ul>
      </div>
    </div>
  )
}

export default User;