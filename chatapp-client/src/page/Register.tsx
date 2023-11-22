import React from 'react'
import { mdlUser } from '../Core/Modals/User';
import { UserDto } from '../Core/Modals/Dto/UserDto';
import { userRegister } from '../services/userService';

const Register = () => {

  const [user, setUser] = React.useState({ email: "", name: "", password: "", image: "default" });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  }

  const addNewUser = async () => {
    if (user.name !== "" && user.password !== "" && user.email !== "") {
      const newUser = new UserDto();
      newUser.email = user.email;
      newUser.name = user.name;
      newUser.password = user.password;
      newUser.image = user.image;
      const response = await userRegister(newUser);
      if (response.success) {
        localStorage.setItem("activeUser", JSON.stringify(response.body));
        window.location.href = `${process.env.REACT_APP_BASE_URL}`;
      } else {
        alert("Bu email kullanılıyor!");
      }
    } else {
      alert("Tüm veri girişlerini yapınız!");
    }

  }

  return (
    <div className="d-flex justify-content-center align-items-center" style={{ marginTop: "15rem" }}>
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
        <div className='d-flex gap-3'>

          <button type='button' className='btn btn-primary' onClick={() => window.location.href = `${process.env.REACT_APP_BASE_URL}/login`}>
            Sign In
          </button>
          <button type="button" className="btn btn-warning " onClick={addNewUser}>
            Sign Up
          </button>
        </div>

      </form>
    </div>
  )
}

export default Register