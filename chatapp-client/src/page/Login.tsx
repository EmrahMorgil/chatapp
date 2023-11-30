import React from "react";
import { userLogin } from "../services/userService";
import { UserLoginDto } from "../Core/Modals/Dto/UserLoginDto";
import {toast} from "react-toastify";


const Login = () => {

  const [user, setUser] = React.useState({ email: "", password: "" });

  const handleChange = (e: any) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  }

  const userControl = async () => {
    if (user.email !== "" && user.password !== "") {
      const newUser = new UserLoginDto();
      newUser.email = user.email;
      newUser.password = user.password;
      const response = await userLogin(newUser);
      if (response.success) {
        localStorage.setItem("activeUser", JSON.stringify(response.body));
        localStorage.setItem("token", response.token);
        window.location.href = `${process.env.REACT_APP_BASE_URL}`;
      } else {
        toast.warning("Hatalı giriş");
      }
    }else{
      toast.warning("All fields must be filled!");
    }
  }


  return (
    <div className="d-flex justify-content-center align-items-center" style={{ marginTop: "15rem" }}>
      <form style={{ width: "300px" }}>
        <div className="form-outline mb-4">
          <input type="email" name="email" className="form-control" onChange={handleChange} />

        </div>
        <div className="form-outline mb-4">
          <input type="password" name="password" className="form-control" onChange={handleChange} />

        </div>

        <button type="button" className="btn btn-primary btn-block mb-4" onClick={userControl}>
          Sign in
        </button>
        <div className="text-center">
          <p>
            Not a member? <a href="/register">Register</a>
          </p>

        </div>
      </form>
    </div>
  );
};

export default Login;
