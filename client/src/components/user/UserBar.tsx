import React from 'react'
import { HandleLogout } from '../helpers/HandleLogout';
import { toast } from "react-toastify";
import getUserImage from '../helpers/ImageHelper';
import mdlUser from '../../core/models/User';
import UserService from '../../utils/UserService';
import mdlUpdateUserRequest from '../../core/models/service-models/user/UpdateUserRequest';
import CookieManager from '../helpers/CookieManager';
import ImageValidationHelper from '../helpers/ImageValidationHelper';


const UserBar = () => {

  const activeUser: mdlUser = JSON.parse(CookieManager.getCookie("activeUser")!);
  const [user, setUser] = React.useState<mdlUpdateUserRequest>(new mdlUpdateUserRequest(activeUser.id, activeUser.email, activeUser.name, "", "", "", activeUser.image));
  const [uploadFile, setUploadFile] = React.useState<FormData>();
  const [passwordChange, setPasswordChange] = React.useState(false);

  const handleUpdate = async () => {
    let image = activeUser.image;
    if (uploadFile) {
      const uploadResponse = await UserService.UploadImage(uploadFile);
      if(uploadResponse.success)
        image = uploadResponse.body;
    }
    var token = CookieManager.getCookie("token");
    var request = new mdlUpdateUserRequest(user.id, user.email, user.name, user.oldPassword, user.newPassword, user.newPasswordVerify, image);
    var response = await UserService.Update(request, token!);
    if (response.success && response.token) {
      toast.success(response.message);
      CookieManager.setCookie("token", response.token, 1);
      CookieManager.setCookie("activeUser", JSON.stringify(response.body), 1);
      window.location.reload();
    } else {
      toast.warning(response.message);
    }
  }

  const uploadImage = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file: File = event.target.files![0];

    if (file && ImageValidationHelper(file)) {
      const reader = new FileReader();
      const formData = new FormData();
      formData.append('image', file);
      setUploadFile(formData);
      reader.onload = (e) => {
        const previewImage: any = document.getElementById('bar-preview-image');
        if (previewImage) {
          previewImage.src = e.target?.result;
        }
      };
      reader.readAsDataURL(file);
    } else
      toast.warning("Invalid File Format");
  }

  return (
    <div style={{ backgroundColor: "#202C33", height: "70px", width: "100%", display: "flex", alignItems: "center", justifyContent: "space-between" }}>
      <div>
        <img style={{ borderRadius: "50%", marginLeft: "30px", cursor: "pointer" }} alt='' src={getUserImage(activeUser.image)} width={"50px"} height={"50px"} data-bs-toggle="modal" data-bs-target="#profile" />
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
                  <img id='bar-preview-image' style={{ width: "120px", height: "120px", borderRadius: "50%" }} src={getUserImage(user.image)} />
                  <label>Name :</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, name: e.target.value })} className='form-control' value={user.name} name='name' />
                  </div>
                  <label>Email :</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, email: e.target.value })} className='form-control' value={user.email} name='email' />
                  </div>
                  <label>Image :</label>
                  <div>
                    <input type="file" className='form-control' onChange={uploadImage} name='file'/>
                  </div>
                  <div className='d-flex gap-2'>
                  <label>I want to change my password</label>
                  <input type='checkbox' onChange={()=>setPasswordChange(!passwordChange)}/>
                  </div>
                  {passwordChange ? <>
                  <label>Old Password :</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, oldPassword: e.target.value })} className='form-control' value={user.oldPassword} name='oldPassword' type='password' />
                  </div>
                  <label>New Password :</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, newPassword: e.target.value })} className='form-control' value={user.newPassword} name='newPassword' type='password' />
                  </div>
                  <label>New Password Verify:</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, newPasswordVerify: e.target.value })} className='form-control' value={user.newPasswordVerify} name='newPasswordVerify' type='password' />
                  </div>
                  </> : <>
                  <label>Password :</label>
                  <div>
                    <input onChange={(e) => setUser({ ...user, oldPassword: e.target.value })} className='form-control' value={user.oldPassword} name='oldPassword' type='password' />
                  </div>
                  </>}
                 
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
                <button className='btn btn-warning' style={{ width: "120px" }} onClick={handleUpdate} >
                  Save
                </button>
              </div>
            </div>
          </div>
        </div>

        <div className={"ms-3 online-point-header"}></div>
        <span style={{ color: "white" }} className='ms-3'>Online</span>
      </div>
      <div className="dropdown" style={{ marginRight: "10px" }}>
        <button className="btn btn-secondary dropdown-toggle" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
          <span className="rotate" aria-hidden="true">⚙️</span>
        </button>
        <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><button className="dropdown-item btn" onClick={HandleLogout}>Logout</button></li>
        </ul>
      </div>
    </div>
  )
}

export default UserBar