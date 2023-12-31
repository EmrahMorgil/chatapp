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
import LoadingSpinner from "../components/helpers/LoadingSpinner";

const Home = () => {
  const getmessage = new Audio("../../sounds/getmessage.wav");
  const sendtomessage = new Audio("../../sounds/sendtomessage.wav");
  const joinroom = new Audio("../../sounds/joinroom.wav");
  const leaveroom = new Audio("../../sounds/leaveroom.wav");

  const [connection, setConnection] = React.useState<signalR.HubConnection | null>(null);
  const [messages, setMessages] = React.useState<mdlMessage[]>([]);
  const [pageOnReload, setPageOnReload] = React.useState(true);
  const [users, setUsers] = React.useState<UserViewDto[]>([]);
  const [loadingScreen, setLoadingScreen] = React.useState(false);
  const activeUser: mdlUser = JSON.parse(localStorage.getItem("activeUser")!);

  React.useEffect(() => {

    if (!connection){
      setLoadingScreen(true);
      fnGetConnection();
    }

    if (pageOnReload) {
      localStorage.removeItem("takerUser");
      localStorage.removeItem("room");
      setPageOnReload(false);
    }
  }, []);


  const fnGetConnection = () => {
    
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${process.env.REACT_APP_SERVER_URI}/chat-hub?username=${activeUser.name}&userid=${activeUser.id}`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", (message: mdlMessage) => {
      if(message.room?.includes(activeUser.id!)){
        if (message.senderId !== activeUser.id){
          getmessage.play();
          if(localStorage.getItem("room") == null || localStorage.getItem("room") !== message.room){
            var unreadUsers: string[] = JSON.parse(localStorage.getItem("unreadUsers")!);
            toast.success(`${message.senderUserName}: ${message.message}`);
            var newUnreadUsers: string[] = [];
            if(unreadUsers)
              newUnreadUsers = unreadUsers;
            newUnreadUsers.push(message.senderId!);
            localStorage.setItem("unreadUsers", JSON.stringify(newUnreadUsers));
            document.getElementById("u"+message.senderId!)?.classList.add("d-none");
            document.getElementById(message.senderId!)?.classList.remove("d-none");
          }
        }
      else
        sendtomessage.play();
      
        if(message.room === localStorage.getItem("room"))
        setMessages((prevMessages) => [...prevMessages, message]);
      }
    });

    newConnection.on("UserConnection", async(userConnect: UserConnect) => {

      if(userConnect.message?.includes("join") && userConnect.lastUserId !== activeUser.id){
        toast.success(userConnect.message);
        joinroom.play();
      }else if(userConnect.message?.includes("disconnect")){
        toast.error(userConnect.message);
        leaveroom.play();
      }
        var token = localStorage.getItem("token");
        const response = await getUsers(activeUser, token!);
        
        if (!response.success)
          HandleLogout();

        response.body.forEach((user: UserViewDto) => {
          user.status = userConnect.usersIds?.some(onlineUser => onlineUser === user.id);
        });

        setUsers(response.body);

        // Kullanıcı giriş yaptığı zaman listelenen kullanıcılar ile oda oluşturulup o odayı websocket dinler. 
        // Yeni bir kullanıcı katıldığı zaman fonksiyona tekrar girer ve yeni gelen kullanıcı ile birlikte tüm kullanıcılar ile
        // odalar güncellenir.
        if(!userConnect.message?.includes("disconnect") && userConnect.message?.includes(activeUser.name!)){

          setTimeout(() => {
            
            var users = response.body;
            var Rooms: string[] = [];
            
            users.forEach((u: UserViewDto) => {
              if(activeUser && activeUser.id && u.id !== activeUser.id){
                var oneRoom = u.id+activeUser.id;
                var twoRoom = activeUser.id+u.id;
                var setRoom = [oneRoom, twoRoom];
                setRoom.sort();
                Rooms.push(setRoom[0]);
              }
            });
            
            Rooms.forEach((room: string) => {
              newConnection?.invoke("JoinRoom", room);
            });
            
            setLoadingScreen(false);
          }, 1000);
        }
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

    setLoadingScreen(true);
    const takerUser = JSON.parse(localStorage.getItem("takerUser")!);
    let dynamicId = undefined;


    if (id || takerUser) {
      dynamicId = id || takerUser.id;
    }

    const items = document.querySelectorAll('.user');
    items.forEach(item => item.classList.remove("activeUser"));
    document.getElementById("id" + dynamicId)?.classList.add("activeUser");

    var message: GetMessagesDto = { takerId: dynamicId!, senderId: activeUser.id };
    var token = localStorage.getItem("token");

    if (activeUser && dynamicId && token) {
      let response = await getMessages(message, token);
      if (response.success) {
        setMessages(response.body.messages);
        localStorage.setItem("room", response.body.room);
      }
    }
    setLoadingScreen(false);
  }

  function scrollToBottom() {
    const messagesContainer = document.getElementById('messages-container');
    if (messagesContainer)
      messagesContainer.scrollTop = 10000;
  }


  return (
    <div>
      <div className={`custom-row-md align-items-center justify-content-center mt-md-5 ${loadingScreen && "loading-screen-active"}`}>
        <User fnGetMessages={fnGetMessages} users={users}/>
        <Message messages={messages} scrollToBottom={scrollToBottom} />
        {loadingScreen && <LoadingSpinner />}
      </div>
    </div>
  );
};

export default Home;
