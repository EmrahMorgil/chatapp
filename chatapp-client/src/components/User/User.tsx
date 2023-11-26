import React from 'react'
import UserBar from './UserBar/UserBar'
import Users from './Users/Users'
import { UserViewDto } from '../../Core/Modals/Dto/UserViewDto'


interface UserProps {
  fnGetMessages?: Function;
  users?: UserViewDto[];
}


const User: React.FC<UserProps> = (props) => {


  return (
    <div className='col-xl-3' style={{ height: "50rem", backgroundColor: "#111B21", padding: "0px", margin: "0px" }}>
      <UserBar />
      <Users users={props.users} fnGetMessages={props.fnGetMessages} />
    </div>
  )
}

export default User;