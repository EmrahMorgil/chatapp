import React from 'react'
import UserItems from './UserItems';
import mdlUserDto from '../../core/dto/UserDto';

interface IUserProps {
  users?: mdlUserDto[];
  getMessages?: Function;
}

const Users: React.FC<IUserProps> = (props) => {

  return (
    <div className='user-scroll' style={{ height: "730px", paddingTop: "1rem" }}>
      {props.users?.map((i: mdlUserDto, key: number) => {
          return <UserItems key={key} item={i} getMessages={props.getMessages} />
      })}
    </div>
  )
}

export default Users