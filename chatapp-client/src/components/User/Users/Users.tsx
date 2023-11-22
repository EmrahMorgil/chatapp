import React from 'react'
import UserItems from './UserItems';
import { mdlUser } from '../../../Core/Modals/User';

interface UsersProps {
  users?: mdlUser[];
  fnGetMessages?: Function;
}

const Users: React.FC<UsersProps> = (props) => {

  var activeUser = localStorage.getItem("activeUser");
  var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

  return (
    <div className='user-scroll' style={{ height: "730px", paddingTop: "1rem" }}>
      {props.users?.map((i: mdlUser, key: number) => {
        if (i.id !== activeUserParse.id)
          return <UserItems key={key} item={i} fnGetMessages={props.fnGetMessages} />
      })}
    </div>
  )
}

export default Users