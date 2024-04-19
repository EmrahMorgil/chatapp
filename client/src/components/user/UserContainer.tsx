import React from 'react'
import Users from './Users'
import mdlUserDto from '../../core/dto/UserDto';
import UserBar from './UserBar';


class mdlUserProps {
  getMessages?: Function;
  users?: mdlUserDto[];
}


const UserContainer: React.FC<mdlUserProps> = (props) => {

  return (
      <div className='col-xl-3 col-md-3' style={{ height: "50rem", backgroundColor: "#111B21", padding: "0px", margin: "0px" }}>
        <UserBar />
        <Users users={props.users} getMessages={props.getMessages} />
      </div>
  )
}

export default UserContainer;