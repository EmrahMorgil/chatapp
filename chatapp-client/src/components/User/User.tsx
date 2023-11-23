import React from 'react'
import UserBar from './UserBar/UserBar'
import Users from './Users/Users'
import { getUsers } from '../../services/userService'
import { mdlUser } from '../../Core/Modals/User'
import { GetUsersDto } from '../../Core/Modals/Dto/GetUsersDto'
import { UserViewDto } from '../../Core/Modals/Dto/UserViewDto'


interface UserProps {
  fnGetMessages?: Function;
}


const User: React.FC<UserProps> = (props) => {

  const [users, setUsers] = React.useState<UserViewDto[]>([]);


  React.useEffect(() => {
    const fetchData = async () => {
      var request = new GetUsersDto();
      const activeUser = localStorage.getItem("activeUser");
      var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;
      var token = localStorage.getItem("token");
      if (activeUserParse && activeUserParse.id && token) {
        request.id = activeUserParse.id;
        const response = await getUsers(request, token);
        if (response.success)
          setUsers(response.body);
      }
    }
    fetchData();
  }, [])


  return (
    <div className='col-xl-3' style={{ height: "50rem", backgroundColor: "#111B21", padding: "0px", margin: "0px" }}>
      <UserBar />
      <Users users={users} fnGetMessages={props.fnGetMessages} />
    </div>
  )
}

export default User;