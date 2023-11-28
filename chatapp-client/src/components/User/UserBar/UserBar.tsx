import React from 'react'
import { mdlUser } from '../../../Core/Modals/User';
import { HandleLogout } from '../../helpers/HandleLogout';
import {toast} from "react-toastify";
import { userUpdate } from '../../../services/userService';

class mdlUpdateUser extends mdlUser{
  verifyPassword?: string;
}

const UserBar = () => {

  const activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);
  const [user, setUser] = React.useState<mdlUpdateUser>({name: activeUser.name, email: activeUser.email, password: "",verifyPassword: "", image: activeUser.image});

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>)=>{
      setUser({...user, [e.target.name]:e.target.value});
  }

  const handleClick = async()=>{
      if(user.password !== user.verifyPassword){
          toast.error("Passwords do not match!");
      }else if(user.email !== "" && user.name !== "" && user.image !== "" && user.password !== "" && user.verifyPassword !== ""){
        setUser({name: user.name, email: user.email, password: "",verifyPassword: "", image: user.image});
          var updateUser = new mdlUser();
          updateUser.id = activeUser.id;
          updateUser.name = user.name;
          updateUser.image = user.image;
          updateUser.password = user.password;
          updateUser.email = user.email;
          updateUser.createdDate = activeUser.createdDate;
          var response = await userUpdate(updateUser);
          if(response.success){
            toast.success("Process was completed successfully");
            localStorage.setItem("activeUser", JSON.stringify(response.body));
            window.location.reload();
          }
      }else{
          toast.error("All fields must be filled!");
      }
  }

  

  return (
    <div style={{ backgroundColor: "#202C33", height: "70px", width: "100%", display: "flex", alignItems: "center", justifyContent: "space-between" }}>
      <div>
        <img style={{ borderRadius: "50%", marginLeft: "30px", cursor: "pointer" }} alt='' src={activeUser.image} width={"50px"} height={"50px"} data-bs-toggle="modal" data-bs-target="#profile" />
        <div
          className="modal fade"
          id="profile"
          tabIndex={-1}
          aria-labelledby="profile-modal"
          aria-hidden="true"
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title" id="profile-modal">
                  Profile
                </h5>
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                />
              </div>
              <div className="modal-body">

              <div style={{ marginTop: "3vh" }} className='d-flex align-items-center flex-column gap-2'>
            <img style={{ width: "120px", height: "120px", borderRadius: "50%" }} src={user.image} />
            <label>Name :</label>
            <div>
                <input onChange={handleOnChange} className='form-control' value={user.name} name='name'/>
            </div>
            <label>Email :</label>
            <div>
                <input onChange={handleOnChange} className='form-control' value={user.email} name='email'/>
            </div>
            <label>Image :</label>
            <div>
                <input onChange={handleOnChange} className='form-control' value={user.image} name='image'/>
            </div>
            <label>New Password :</label>
            <div>
                <input onChange={handleOnChange} className='form-control' value={user.password} name='password' type='password'/>
            </div>
            <label>Verify Password :</label>
            <div>
                <input onChange={handleOnChange} className='form-control' value={user.verifyPassword} name='verifyPassword' type='password'/>
            </div>
        </div>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-danger"
                  data-bs-dismiss="modal"
                >
                  Close
                </button>
                <button className='btn btn-warning' style={{width: "120px"}} onClick={handleClick} >
                  Save
                </button>
              </div>
            </div>
          </div>
        </div>
        
        <div className={"ms-3 online-point-header"}></div>
        <span style={{color: "white"}} className='ms-3'>Online</span>
      </div>
      <div className="dropdown" style={{ marginRight: "10px" }}>
        <button className="btn btn-secondary dropdown-toggle"id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
        <span className="rotate" aria-hidden="true">⚙️</span>
        </button>
        <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          {/* <li><button className='dropdown-item btn' onClick={()=>window.location.href = `${process.env.REACT_APP_BASE_URL}/profile`}>Profile</button></li> */}
          <li><button className="dropdown-item btn" onClick={HandleLogout}>Logout</button></li>
        </ul>
      </div>
    </div>
  )
}

export default UserBar