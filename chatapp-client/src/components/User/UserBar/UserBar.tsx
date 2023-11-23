import React from 'react'
import { mdlUser } from '../../../Core/Modals/User';
import { HandleLogout } from '../../helpers/HandleLogout';

const UserBar = () => {

  const activeUser = localStorage.getItem("activeUser");
  var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;


  return (
    <div style={{ backgroundColor: "#202C33", height: "70px", width: "100%", display: "flex", alignItems: "center", justifyContent: "space-between" }}>
      <div>
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} alt='' src={activeUserParse.image} width={"50px"} height={"50px"} />
      </div>
      <div className="dropdown" style={{ marginRight: "10px" }}>
        <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
          ⚙️
        </button>
        <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><button className="dropdown-item btn btn-danger" onClick={HandleLogout}>Logout</button></li>
        </ul>
      </div>
    </div>
  )
}

export default UserBar