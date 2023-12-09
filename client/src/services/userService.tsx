import axios from "axios";
import { UserDto } from "../Core/Modals/Dto/UserDto";
import { UserLoginDto } from "../Core/Modals/Dto/UserLoginDto";
import { GetUsersDto } from "../Core/Modals/Dto/GetUsersDto";
import { HandleLogout } from "../components/helpers/HandleLogout";
import { mdlUser } from "../Core/Modals/User";


export const getUsers = async (user: GetUsersDto, token: string) => {
  try {
    return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/GetUsers`, user, { headers: { "Authorization": `Bearer ${token}` } }).then(res => res.data);
  } catch (error) {
    HandleLogout();
  }
}

export const userRegister = async (user: UserDto) => {
  return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/Register`, user).then(res => res.data);
}

export const userLogin = async (user: UserLoginDto) => {
  return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/Login`, user).then(res => res.data);
}

export const userUpdate = async(user: mdlUser)=>{
  return await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/Update`, user).then(res => res.data);
}