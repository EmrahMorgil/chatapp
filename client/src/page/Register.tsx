import React from 'react'
import { toast } from "react-toastify";
import LoadingSpinner from '../components/helpers/LoadingSpinner';
import mdlCreateUserRequest from '../core/models/service-models/user/CreateUserRequest';
import UserService from '../utils/UserService';
import CookieManager from '../components/helpers/CookieManager';
import ImageValidationHelper from '../components/helpers/ImageValidationHelper';
import getUserImage from '../components/helpers/ImageHelper';

const Register = () => {

  const [user, setUser] = React.useState<mdlCreateUserRequest>(new mdlCreateUserRequest());
  const [uploadFile, setUploadFile] = React.useState<FormData>();
  const [loadingScreen, setLoadingScreen] = React.useState<boolean>(false);

  const uploadImage = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file: File = event.target.files![0];

    if (file && ImageValidationHelper(file)) {
      const reader = new FileReader();
      const formData = new FormData();
      formData.append('image', file);
      setUploadFile(formData);
      reader.onload = (e) => {
        const previewImage: any = document.getElementById('preview-image');
        if (previewImage) {
          previewImage.src = e.target?.result;
        }
      };
      reader.readAsDataURL(file);
    } else
      toast.warning("Invalid File Format");
  }

  const handleRegister = async () => {
    setLoadingScreen(true);
    if (uploadFile) {
      const uploadResponse = await UserService.UploadImage(uploadFile);
      if (uploadResponse.success) {
        const newUser = new mdlCreateUserRequest(user.email, user.name, user.password, uploadResponse.body);
        const response = await UserService.Create(newUser);
        if (response.success && response.token) {
          toast.success(response.message);
          CookieManager.setCookie("token", response.token, 1);
          CookieManager.setCookie("activeUser", JSON.stringify(response.body), 1);
          window.location.href = `${process.env.REACT_APP_BASE_URL}`;
        } else {
          toast.warning(response.message);
        }
      } else {
        toast.warning(uploadResponse.message);
      }
    } else
      toast.warning("Image Cannot Be Empty");
    setLoadingScreen(false);
  }


  return (
    <div className={`d-flex flex-column justify-content-center align-items-center ${loadingScreen && "loading-screen-active"}`} style={{ marginTop: "10rem" }}>
      <h1 className="text-center mb-5 header-gradient">Register</h1>
      <img id="preview-image" src={getUserImage("default-user.jpg")} style={{ width: "120px", height: "120px", borderRadius: "50%", marginBottom: "2rem", border: "1px solid grey" }} />
      <div style={{ width: "300px" }}>
        <div className="form-outline mb-4">
          <input type="email" name='email' className="form-control" placeholder='Email' onChange={(e) => setUser({ ...user, email: e.target.value })} />
        </div>
        <div className="form-outline mb-4">
          <input type="text" name='name' className="form-control" placeholder='Name' onChange={(e) => setUser({ ...user, name: e.target.value })} />
        </div>
        <div className="form-outline mb-4">
          <input type="password" name='password' className="form-control" placeholder='Password' onChange={(e) => setUser({ ...user, password: e.target.value })} />
        </div>
        <div className="form-outline mb-4">
          <input type="file" className='form-control' onChange={uploadImage} />
        </div>
        <div className='d-flex gap-3 justify-content-center align-items-center'>

          <p style={{ color: "white", marginBottom: "0px" }}>
            I have an account <a href="/login">Login</a>
          </p>

          <button type="button" className="btn btn-warning " onClick={handleRegister}>
            Sign Up
          </button>
        </div>

      </div>
      {loadingScreen && <LoadingSpinner />}
    </div>
  )
}

export default Register