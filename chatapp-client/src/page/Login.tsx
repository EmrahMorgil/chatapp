import React from "react";
import { userLogin } from "../services/userService";
import { UserLoginDto } from "../Core/Modals/Dto/UserLoginDto";
import {toast} from "react-toastify";
import LoadingSpinner from "../components/helpers/LoadingSpinner";


const Login = () => {

  const [user, setUser] = React.useState({ email: "", password: "" });
  const [loadingScreen, setLoadingScreen] = React.useState(false);

  const handleChange = (e: any) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  }

  const userControl = async () => {
    if (user.email !== "" && user.password !== "") {
      const newUser = new UserLoginDto();
      newUser.email = user.email;
      newUser.password = user.password;
      setLoadingScreen(true);
      const response = await userLogin(newUser);
      if (response.success) {
        setLoadingScreen(false);
        toast.success("Giriş Başarılı!");
        localStorage.setItem("activeUser", JSON.stringify(response.body));
        localStorage.setItem("token", response.token);
        setTimeout(()=>{
          window.location.href = `${process.env.REACT_APP_BASE_URL}`;
        }, 1000);
      } else {
        toast.warning("Hatalı giriş");
      }
      setLoadingScreen(false);
    }else{
      toast.warning("All fields must be filled!");
    }
  }


  return (
    <div className={`d-flex justify-content-center align-items-center ${loadingScreen && "loading-screen-active"}`} style={{ marginTop: "15rem" }}>
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
          <p style={{color: "white"}}>
            Not a member? <a href="/register">Register</a>
          </p>

          
        </div>
      </form>
        {loadingScreen && <LoadingSpinner />}
    </div>
  );
};

export default Login;
