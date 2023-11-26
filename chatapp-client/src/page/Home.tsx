import React from "react";
import User from "../components/User/User";
import Message from "../components/Message/Message";
import { mdlMessage } from "../Core/Modals/Message";
import { mdlUser } from "../Core/Modals/User";
import { getMessages } from "../services/messageService";
import { GetMessagesDto } from "../Core/Modals/Dto/GetMessagesDto";
import * as signalR from "@microsoft/signalr";
import {toast} from "react-toastify";
import { GetUsersDto } from "../Core/Modals/Dto/GetUsersDto";
import { getUsers } from "../services/userService";
import { UserViewDto } from "../Core/Modals/Dto/UserViewDto";
import { HandleLogout } from "../components/helpers/HandleLogout";
import { UserConnect } from "../Core/Modals/Dto/UserConnect";


const Home = () => {
  const getmessage = new Audio("../../sounds/getmessage.wav");
  const sendtomessage = new Audio("../../sounds/sendtomessage.wav");
  const joinroom = new Audio("../../sounds/joinroom.wav");
  const leaveroom = new Audio("../../sounds/leaveroom.wav");

  const [connection, setConnection] = React.useState<signalR.HubConnection | null>(null);
  const [messages, setMessages] = React.useState<mdlMessage[]>([]);
  const [pageOnReload, setPageOnReload] = React.useState(true);
  const [users, setUsers] = React.useState<UserViewDto[]>([]);
  var activeUser = localStorage.getItem("activeUser");
  var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

  React.useEffect(() => {

    if (!connection){
      fnGetConnection();
    }

    if (pageOnReload) {
      localStorage.removeItem("takerUser");
      localStorage.removeItem("room");
      setPageOnReload(false);
    }
  }, []);


  const fnGetConnection = () => {
    var activeUser = localStorage.getItem("activeUser");
    var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${process.env.REACT_APP_SERVER_URI}/chat-hub?username=${activeUserParse.name}&userid=${activeUserParse.id}`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", (message: mdlMessage) => {
      if (message.senderId !== activeUserParse.id)
        getmessage.play();
      else
        sendtomessage.play();
      setMessages((prevMessages) => [...prevMessages, message]);
    });

    newConnection.on("UserConnection", async(userConnect: UserConnect) => {
      var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

      if(userConnect.message?.includes("join") && userConnect.lastUserId !== activeUserParse.id){
        toast.success(userConnect.message);
        joinroom.play();
      }else if(userConnect.message?.includes("disconnect")){
        toast.error(userConnect.message);
        leaveroom.play();
      }
        var token = localStorage.getItem("token");
        const response = await getUsers(activeUserParse, token!);
        
        if (!response.success)
          HandleLogout();

        response.body.forEach((user: UserViewDto) => {
          user.status = userConnect.usersIds?.some(onlineUser => onlineUser === user.id);
        });

        setUsers(response.body);
    });

    

    const startConnection = async () => {
      try {
        await newConnection.start();
      } catch (error) {
        console.error('Error while establishing connection:', error);
        setTimeout(startConnection, 2000);
      }
    };
    startConnection();
  }

  const fnGetMessages = async (id?: string) => {

    const takerUser = localStorage.getItem("takerUser");
    const activeTakerUserParse: mdlUser = takerUser ? JSON.parse(takerUser) : null;
    let dynamicId = undefined;


    if (id || activeTakerUserParse) {
      dynamicId = id || activeTakerUserParse.id;
    }

    const items = document.querySelectorAll('.user');
    items.forEach(item => item.classList.remove("activeUser"));
    document.getElementById("id" + dynamicId)?.classList.add("activeUser");

    var message: GetMessagesDto = { takerId: dynamicId!, senderId: activeUserParse.id };
    var token = localStorage.getItem("token");

    if (activeUserParse && dynamicId && token) {
      let response = await getMessages(message, token);
      if (response.success) {
        connection?.invoke("JoinRoom", response.body.room);
        setMessages(response.body.messages);
        localStorage.setItem("room", response.body.room);
      }
    }

  }

  function scrollToBottom() {
    const messagesContainer = document.getElementById('messages-container');
    if (messagesContainer)
      messagesContainer.scrollTop = 10000;
  }


  return (
    <div>
      <div className="row align-items-center justify-content-center" style={{ height: "100vh" }}>
        <User fnGetMessages={fnGetMessages} users={users}/>
        <Message messages={messages} scrollToBottom={scrollToBottom} />
      </div>
    </div>
  );
};

export default Home;
