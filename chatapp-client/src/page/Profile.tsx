import React from 'react'
import { mdlUser } from '../Core/Modals/User';

const Profile = () => {
    const activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);

    return (
        <div style={{ marginTop: "25vh" }} className='d-flex align-items-center flex-column gap-2'>
            <img style={{ width: "100px", height: "100px" }} src={"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT25iHjX8XUaBcJC68oNRl7XnuQmA7j4EOb4A&usqp=CAU"} />
            <label>Name :</label>
            <input value={activeUser.name} />
            <label>Email :</label>
            <input value={activeUser.email} />
            <label>Current Password :</label>
            <input value={""} />
            <label>New Password :</label>
            <input value={""} />
            <label>Verify Password :</label>
            <input value={""} />
        </div>
    )
}

export default Profile