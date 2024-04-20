import React from "react";
import { toast } from "react-toastify";
import LoadingSpinner from "../components/helpers/LoadingSpinner";
import mdlCreateUserRequest from "../core/models/service-models/user/CreateUserRequest";
import UserService from "../utils/UserService";
import CookieManager from "../components/helpers/CookieManager";
import getUserImage from "../components/helpers/ImageHelper";

const Register = () => {
  const [user, setUser] = React.useState<mdlCreateUserRequest>(
    new mdlCreateUserRequest("", "", "", getUserImage("default-user.jpg"))
  );
  const [uploadFile, setUploadFile] = React.useState<File>();
  const [loadingScreen, setLoadingScreen] = React.useState<boolean>(false);

  const uploadImage = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file: File = event.target.files![0];

    if (file) {
      setUploadFile(file);
      const reader = new FileReader();
      reader.onload = (e) => {
        let result = e.target?.result as string;
        setUser({ ...user, image: result });
      };
      reader.readAsDataURL(file);
    }
  };

  const handleRegister = async () => {
    setLoadingScreen(true);
    var prepareImage = uploadFile?.name + "," + user.image;
    const newUser = new mdlCreateUserRequest(user.email,user.name,user.password,prepareImage);
    const response = await UserService.Create(newUser);
    if (response.success && response.token) {
      toast.success(response.message);
      CookieManager.setCookie("token", response.token, 1);
      window.location.href = window.location.origin;
    } else {
      toast.warning(response.message);
    }
    setLoadingScreen(false);
  };

  return (
    <div
      className={`d-flex flex-column justify-content-center align-items-center ${
        loadingScreen && "loading-screen-active"
      }`}
      style={{ marginTop: "10rem" }}
    >
      <h1 className="text-center mb-5 header-gradient">Register</h1>
      <img
        src={user.image}
        style={{
          width: "120px",
          height: "120px",
          borderRadius: "50%",
          marginBottom: "2rem",
          border: "1px solid grey",
        }}
      />
      <div style={{ width: "300px" }}>
        <div className="form-outline mb-4">
          <input
            type="email"
            name="email"
            className="form-control"
            placeholder="Email"
            onChange={(e) => setUser({ ...user, email: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4">
          <input
            type="text"
            name="name"
            className="form-control"
            placeholder="Name"
            onChange={(e) => setUser({ ...user, name: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4">
          <input
            type="password"
            name="password"
            className="form-control"
            placeholder="Password"
            onChange={(e) => setUser({ ...user, password: e.target.value })}
          />
        </div>
        <div className="form-outline mb-4">
          <input type="file" className="form-control" onChange={uploadImage} />
        </div>
        <div className="d-flex gap-3 justify-content-center align-items-center">
          <p style={{ color: "white", marginBottom: "0px" }}>
            I have an account <a href="/login">Login</a>
          </p>

          <button
            type="button"
            className="btn btn-warning "
            onClick={handleRegister}
          >
            Sign Up
          </button>
        </div>
      </div>
      {loadingScreen && <LoadingSpinner />}
    </div>
  );
};

export default Register;
