import React from 'react'
import UserItems from './UserItems';
import { UserViewDto } from '../../../Core/Modals/Dto/UserViewDto';
import { mdlUser } from '../../../Core/Modals/User';

interface UsersProps {
  users?: UserViewDto[];
  fnGetMessages?: Function;
}

const Users: React.FC<UsersProps> = (props) => {

  var activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);

  return (
    <div className='user-scroll' style={{ height: "730px", paddingTop: "1rem" }}>
      {props.users?.map((i: UserViewDto, key: number) => {
        if (i.id !== activeUser.id)
          return <UserItems key={key} item={i} fnGetMessages={props.fnGetMessages} />
      })}
    </div>
  )
}

export default Users