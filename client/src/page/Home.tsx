import React from "react";
import User from "../components/user/UserContainer";
import Message from "../components/message/MessageContainer";
import * as signalR from "@microsoft/signalr";
import { toast } from "react-toastify";
import { HandleLogout } from "../components/helpers/HandleLogout";
import LoadingSpinner from "../components/helpers/LoadingSpinner";
import mdlUserDto from "../core/dto/UserDto";
import mdlUser from "../core/models/User";
import mdlOnlineUsers from "../core/models/OnlineUsers";
import UserService from "../utils/UserService";
import { mdlListMessageRequest } from "../core/models/service-models/message/ListMessageRequest";
import MessageService from "../utils/MessageService";
import mdlListUserRequest from "../core/models/service-models/user/ListUserRequest";
import mdlMessageDto from "../core/dto/MessageDto";
import CookieManager from "../components/helpers/CookieManager";

const Home = () => {
  const getmessage = new Audio("../../sounds/getmessage.wav");
  const sendtomessage = new Audio("../../sounds/sendtomessage.wav");
  const joinroom = new Audio("../../sounds/joinroom.wav");
  const leaveroom = new Audio("../../sounds/leaveroom.wav");

  const [connection, setConnection] = React.useState<signalR.HubConnection | null>(null);
  const [messages, setMessages] = React.useState<mdlMessageDto[]>([]);
  const [pageOnReload, setPageOnReload] = React.useState(true);
  const [users, setUsers] = React.useState<mdlUserDto[]>([]);
  const [loadingScreen, setLoadingScreen] = React.useState(false);
  const activeUser: mdlUser = JSON.parse(CookieManager.getCookie("activeUser")!);

  React.useEffect(() => {

    if (!connection)
      fnGetConnection();

    if (pageOnReload) {
      sessionStorage.removeItem("takerUser");
      sessionStorage.removeItem("room");
      setPageOnReload(false);
    }
  }, []);


  const fnGetConnection = () => {

    setLoadingScreen(true);

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${process.env.REACT_APP_SERVER_URI}/api/chat-hub?username=${activeUser.name}&userid=${activeUser.id}`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", (message: mdlMessageDto) => {
      if (message.senderId !== activeUser.id) {
        getmessage.play();
        if (sessionStorage.getItem("room") == null || sessionStorage.getItem("room") !== message.room) {
          toast.success(`${message.senderName}: ${message.content}`);
          document.getElementById(message.senderId!)?.classList.remove("d-none");
          document.getElementById("u" + message.senderId!)?.classList.add("d-none");
        }
      }
      else
        sendtomessage.play();

      if (message.room === sessionStorage.getItem("room"))
        setMessages((prevMessages) => [...prevMessages, message]);
    });

    newConnection.on("UserConnection", async (onlineUsers: mdlOnlineUsers) => {
      if (onlineUsers.message?.includes("join") && onlineUsers.lastUserId !== activeUser.id) {
        toast.success(onlineUsers.message);
        joinroom.play();

        var Rooms: string[] = [];
        onlineUsers.usersIds?.forEach((u: string) => {
          if (activeUser && activeUser.id && u !== activeUser.id) {
            let prepareRoomId = [u, activeUser.id];
            prepareRoomId.sort();
            Rooms.push(prepareRoomId[0] + prepareRoomId[1]);
          }
        });
        Rooms.forEach((room: string) => {
          newConnection?.invoke("JoinRoom", room);
        });
      } else if (onlineUsers.message?.includes("disconnect")) {
        toast.error(onlineUsers.message);
        leaveroom.play();
      }

      await getUsers(onlineUsers);
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
    setLoadingScreen(false);
  }

  const getUsers = async (onlineUsers: mdlOnlineUsers) => {
    var token = CookieManager.getCookie("token");
    const response = await UserService.List(new mdlListUserRequest(activeUser.id!), token!);

    if (response.success && response.body != undefined) {
      response.body?.forEach((user) => {
        user.status = onlineUsers.usersIds?.some(onlineUser => onlineUser === user.id);
      });
      setUsers(response.body);
    } else
      HandleLogout();
  }

  const getMessages = async (receiverUserId: string) => {

    setLoadingScreen(true);
    const items = document.querySelectorAll('.user');
    items.forEach(item => item.classList.remove("activeUser"));
    document.getElementById("id" + receiverUserId)?.classList.add("activeUser");

    let prepareRoomId = [receiverUserId, activeUser.id!];
    prepareRoomId.sort();
    var listMessageRequest = new mdlListMessageRequest(prepareRoomId[0] + prepareRoomId[1]);
    var token = CookieManager.getCookie("token");

    if (listMessageRequest && token) {
      let response = await MessageService.List(listMessageRequest, token);
      if (response.success && response.body) {
        setMessages(response.body);
        sessionStorage.setItem("room", listMessageRequest.room);
        connection?.invoke("JoinRoom", listMessageRequest.room);
      }
    }
    setLoadingScreen(false);
  }

  function scrollToBottom() {
    const messagesContainer = document.getElementById('messages-container');
    if (messagesContainer)
      messagesContainer.scrollTop = messagesContainer.scrollHeight;
  }

  return (
    <div>
      <div className={`custom-row-md align-items-center justify-content-center mt-md-5 ${loadingScreen && "loading-screen-active"}`}>
        <User getMessages={getMessages} users={users} />
        <Message messages={messages} scrollToBottom={scrollToBottom} />
        {loadingScreen && <LoadingSpinner />}
      </div>
    </div>
  );
};

export default Home;
