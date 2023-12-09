import React from 'react'
import { mdlUser } from '../Core/Modals/User';
import { UserDto } from '../Core/Modals/Dto/UserDto';
import { userRegister } from '../services/userService';
import {toast} from "react-toastify";
import LoadingSpinner from '../components/helpers/LoadingSpinner';

const Register = () => {

  const [user, setUser] = React.useState({ email: "", name: "", password: "", image: "data:image/jpeg;base64,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" });
  const [loadingScreen, setLoadingScreen] = React.useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  }

  const addNewUser = async () => {
    if (user.name !== "" && user.password !== "" && user.email !== "" && validateEmail(user.email)) {
      const newUser = new UserDto();
      newUser.email = user.email;
      newUser.name = user.name;
      newUser.password = user.password;
      newUser.image = user.image;
      setLoadingScreen(true);
      const response = await userRegister(newUser);
      if (response.success) {
        setLoadingScreen(false);
        toast.success("Registration successful!");
        localStorage.setItem("activeUser", JSON.stringify(response.body));
        localStorage.setItem("token", response.token);
        setTimeout(()=>{
          window.location.href = `${process.env.REACT_APP_BASE_URL}`;
        }, 1000);
      } else {
        toast.warning("This email is being used!");
      }
    }else if(!validateEmail(user.email)){
      toast.warning("Invalid email entry!");
    } 
    else {
      toast.warning("All fields must be filled!");
    }
    setLoadingScreen(false);

  }

  const validateEmail = (email: string) => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(String(email).toLowerCase());
  };

  return (
    <div className={`d-flex flex-column justify-content-center align-items-center ${loadingScreen && "loading-screen-active"}`} style={{ marginTop: "10rem" }}>
      <h1 className="text-center mb-5 header-gradient">Register</h1>
      <img src={user.image} style={{width: "120px", height: "120px", borderRadius: "50%", marginBottom: "2rem", border: "1px solid grey"}} />
      <form style={{ width: "300px" }} encType="multipart/form-data">
        <div className="form-outline mb-4">
          <input type="email" name='email' className="form-control" placeholder='Email' onChange={handleChange} />
        </div>
        <div className="form-outline mb-4">
          <input type="text" name='name' className="form-control" placeholder='Name' onChange={handleChange} />
        </div>
        <div className="form-outline mb-4">
          <input type="password" name='password' className="form-control" placeholder='Password' onChange={handleChange} />
        </div>
        <div className="form-outline mb-4">
          <input type="text" name="image" className='form-control' placeholder="Image" onChange={handleChange} />
        </div>
        <div className='d-flex gap-3 justify-content-center align-items-center'>

          <p style={{color: "white", marginBottom: "0px"}}>
            I have an account <a href="/login">Login</a>
          </p>

          
          <button type="button" className="btn btn-warning " onClick={addNewUser}>
            Sign Up
          </button>
        </div>

      </form>
      {loadingScreen && <LoadingSpinner />}
    </div>
  )
}

export default Register