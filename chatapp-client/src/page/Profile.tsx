import React from 'react'
import { mdlUser } from '../Core/Modals/User';
import {toast} from "react-toastify";
import { userUpdate } from '../services/userService';

class mdlUpdateUser extends mdlUser{
    verifyPassword?: string;
}

const Profile = () => {
    const activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);
    const [user, setUser] = React.useState<mdlUpdateUser>({name: activeUser.name, email: activeUser.email, password: "",verifyPassword: "", image: activeUser.image});

    const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>)=>{
        setUser({...user, [e.target.name]:e.target.value});
    }

    const handleClick = async()=>{
        if(user.password !== user.verifyPassword){
            toast.error("Passwords do not match!");
        }else if(user.email !== "" && user.name !== "" && user.image !== "" && user.password !== "" && user.verifyPassword !== ""){
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
            }
        }else{
            toast.error("All fields must be filled!");
        }
    }

    return (
        <div style={{ marginTop: "20vh" }} className='d-flex align-items-center flex-column gap-2'>
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
            <div className='mt-2'>
                <button className='btn btn-success' style={{width: "120px"}} onClick={()=>window.location.href = `${process.env.REACT_APP_BASE_URL}`}>Go to back</button>
                <button className='btn btn-warning' style={{width: "120px"}} onClick={handleClick}>Save</button>
            </div>
        </div>
    )
}

export default Profile