import React from "react";
import { toast } from "react-toastify";
import LoadingSpinner from "../components/helpers/LoadingSpinner";
import mdlLoginUserRequest from "../core/models/service-models/user/LoginUserRequest";
import UserService from "../utils/UserService";
import CookieManager from "../components/helpers/CookieManager";

const Login = () => {
  const [user, setUser] = React.useState(new mdlLoginUserRequest());
  const [loadingScreen, setLoadingScreen] = React.useState(false);

  const handleLogin = async () => {
    setLoadingScreen(true);
    const newUser = new mdlLoginUserRequest(user.email, user.password);
    const response = await UserService.Login(newUser);
    if (response.success && response.token) {
      toast.success(response.message);
      CookieManager.setCookie("token", response.token, 1);
      CookieManager.setCookie("activeUser", JSON.stringify(response.body), 1);
      window.location.href = window.location.origin
    } else {
      toast.warning(response.message);
    }
    setLoadingScreen(false);
  };

  return (
    <div
      className={`d-flex justify-content-center align-items-center ${
        loadingScreen && "loading-screen-active"
      }`}
      style={{ marginTop: "15rem" }}
    >
      <form style={{ width: "300px" }}>
        <h1 className="text-center mb-5 header-gradient">Login</h1>
        <div className="form-outline mb-4">
          <input
            type="email"
            name="email"
            className="form-control"
            onChange={(e) => setUser({ ...user, email: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4">
          <input
            type="password"
            name="password"
            className="form-control"
            onChange={(e) => setUser({ ...user, password: e.target.value })}
          />
        </div>
        <button
          type="button"
          className="btn btn-primary btn-block mb-4"
          onClick={handleLogin}
        >
          Sign in
        </button>
        <div className="text-center">
          <p style={{ color: "white" }}>
            Not a member? <a href="/register">Register</a>
          </p>
        </div>
      </form>
      {loadingScreen && <LoadingSpinner />}
    </div>
  );
};

export default Login;
