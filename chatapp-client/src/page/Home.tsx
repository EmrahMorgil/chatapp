import React from "react";
import User from "../components/User/User";
import Message from "../components/Message/Message";
import { mdlMessage } from "../Core/Modals/Message";
import { mdlUser } from "../Core/Modals/User";
import { getMessages } from "../services/messageService";
import { GetMessagesDto } from "../Core/Modals/Dto/GetMessagesDto";
import * as signalR from "@microsoft/signalr";
import {toast} from "react-toastify";


const Home = () => {
  const getmessage = new Audio("../../sounds/getmessage.wav");
  const sendtomessage = new Audio("../../sounds/sendtomessage.wav");
  const joinroom = new Audio("../../sounds/joinroom.wav");
  const leaveroom = new Audio("../../sounds/leaveroom.wav");

  const [connection, setConnection] = React.useState<signalR.HubConnection | null>(null);
  const [messages, setMessages] = React.useState<mdlMessage[]>([]);
  const [pageOnReload, setPageOnReload] = React.useState(true);
  var activeUser = localStorage.getItem("activeUser");
  var activeUserParse: mdlUser = activeUser ? JSON.parse(activeUser) : null;

  React.useEffect(() => {

    if (!connection)
      fnGetConnection();

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
      .withUrl(`${process.env.REACT_APP_SERVER_URI}/chat-hub?username=${activeUserParse.name}`)
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

    newConnection.on("UserConnection", (message: string) => {
      if(message.includes("join")){
        toast.success(message);
        joinroom.play();
      }else{
        toast.error(message);
        leaveroom.play();
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
        <User fnGetMessages={fnGetMessages} />
        <Message messages={messages} scrollToBottom={scrollToBottom} />
      </div>
    </div>
  );
};

export default Home;
