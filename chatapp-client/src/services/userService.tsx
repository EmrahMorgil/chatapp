import axios from "axios";
import { UserDto } from "../Core/Modals/Dto/UserDto";
import { UserLoginDto } from "../Core/Modals/Dto/UserLoginDto";
import { GetUsersDto } from "../Core/Modals/Dto/GetUsersDto";


export const getUsers = async (user: GetUsersDto) => {
  const response = await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/GetUsers`, user);
  return response.data;
}

export const userRegister = async (user: UserDto) => {
  const response = await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/Register`, user);
  return response.data;
}

export const userLogin = async (user: UserLoginDto) => {
  const response = await axios.post(`${process.env.REACT_APP_SERVER_URI}/api/User/Login`, user);
  return response.data;
}