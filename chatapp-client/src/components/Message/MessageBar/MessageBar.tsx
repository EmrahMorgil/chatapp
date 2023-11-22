import React from 'react'
import { mdlUser } from '../../../Core/Modals/User';

const MessageBar = () => {

  const takerUser = localStorage.getItem("takerUser");
  var takerUserParse: mdlUser = takerUser ? JSON.parse(takerUser) : null;



  return (
    <div className='d-flex align-items-center' style={{ backgroundColor: "#202C33", height: "70px", width: "100%" }}>
      {takerUserParse && <div className="d-flex gap-3">
        <img style={{ borderRadius: "50%", marginLeft: "30px" }} src={takerUserParse?.image} alt="" width={"50px"} height={"50px"} />
        <div className="d-flex flex-column justify-content-center">
          <p style={{ color: "white", padding: "0px", margin: "0px" }}>{takerUserParse?.name}</p>
        </div>
      </div>}

    </div>
  )
}

export default MessageBar