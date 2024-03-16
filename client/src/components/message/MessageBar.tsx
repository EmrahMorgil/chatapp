import React from 'react'
import mdlUser from '../../core/models/User';
import getUserImage from '../helpers/ImageHelper';

const MessageBar = () => {

  const takerUser: mdlUser = JSON.parse(sessionStorage.getItem("takerUser")!);

  return (
    <div className='d-flex align-items-center' style={{ backgroundColor: "#202C33", height: "70px", width: "100%" }}>
      {takerUser && <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={getUserImage(takerUser?.image)} alt="" width={"50px"} height={"50px"} />
        <div className="d-flex flex-column justify-content-center">
          <p style={{ color: "white", padding: "0px", margin: "0px" }}>{takerUser?.name}</p>
        </div>
      </div>}
    </div>
  )
}

export default MessageBar