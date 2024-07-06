import React, { useRef } from "react";
import User from "../components/user/UserContainer";
import Message from "../components/message/MessageContainer";
import * as signalR from "@microsoft/signalr";
import { toast } from "react-toastify";
import { HandleLogout } from "../components/helpers/HandleLogout";
import LoadingSpinner from "../components/helpers/LoadingSpinner";
import mdlUserDto from "../core/dto/UserDto";
import mdlOnlineUsers from "../core/models/OnlineUsers";
import UserService from "../utils/UserService";
import { mdlListMessageRequest } from "../core/models/service-models/message/ListMessageRequest";
import MessageService from "../utils/MessageService";
import mdlMessageDto from "../core/dto/MessageDto";
import CookieManager from "../components/helpers/CookieManager";
import getUserImage from "../components/helpers/ImageHelper";
import sounds from "../components/data/Sounds";
import { enmSoundType } from "../core/enums/SoundType";
import mdlUserDetailDto from "../core/dto/UserDetailDto";

const Home = () => {
  const [connection, setConnection] =
    React.useState<signalR.HubConnection | null>(null);
  const [messages, setMessages] = React.useState<mdlMessageDto[]>([]);
  const [pageOnReload, setPageOnReload] = React.useState(true);
  const [users, setUsers] = React.useState<mdlUserDto[]>([]);
  const [loadingScreen, setLoadingScreen] = React.useState(false);
  const [activeUser, setActiveUser] = React.useState<mdlUserDetailDto>();

  React.useEffect(() => {
    if (!connection) {
      if (!activeUser) fnGetActiveUser();
      if (activeUser) {
        sounds.forEach((s) => {
          s.sound.muted = false;
        });
        fnRegisterNotification();
        fnGetConnection();
        if (pageOnReload) {
          sessionStorage.removeItem("takerUser");
          sessionStorage.removeItem("room");
          setPageOnReload(false);
        }
      }
    }
  }, [activeUser]);

  const playAudio = (soundType: enmSoundType) => {
    sounds
      .find((s) => s.type == soundType)
      ?.sound.play()
      .catch((err) => {
        resetAudio();
      });
  };

  const resetAudio = () => {
    sounds.forEach((s) => {
      s.sound.pause();
      s.sound.currentTime = 0;
      s.sound.muted = true;
    });
  };

  const sendNotification = (pTitle: string, pBody: string, pIcon: string) => {
    const title = pTitle;
    const options: NotificationOptions = {
      body: pBody,
      icon: pIcon,
    };

    navigator.serviceWorker.ready.then(function (registration) {
      registration.showNotification(title, options);
    });
  };

  const fnGetActiveUser = async () => {
    const response = await UserService.Detail();
    if (response.success && response.body) {
      setActiveUser(response.body);
    } else {
      toast.warning(response.message);
      HandleLogout();
    }
  };

  const fnRegisterNotification = () => {
    setLoadingScreen(true);

    Notification.requestPermission().then(function (permission) {
      if (permission === "denied")
        toast.error("Please allow notifications and sounds");
    });
  };

  const fnGetConnection = () => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${process.env.REACT_APP_API_URI}/api/chat-hub?userid=${activeUser?.id}`
      )
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", (message: mdlMessageDto) => {
      if (message.userId !== activeUser?.id) {
        playAudio(enmSoundType.get);
        // getmessage.play();
        sendNotification(message.userName!,message.content!,getUserImage(message.userImage));
        if (
          sessionStorage.getItem("room") == null ||
          sessionStorage.getItem("room") !== message.room
        ) {
          // unread message için sarı bildirim
          document
            .getElementById(message.userId!)
            ?.classList.remove("d-none");
          // document.getElementById("u" + message.senderId!)?.classList.add("d-none");
        }
      } else {
        playAudio(enmSoundType.send);
      }
      if (message.room === sessionStorage.getItem("room"))
        setMessages((prevMessages) => [...prevMessages, message]);
    });

    newConnection.on("UserConnection", async (onlineUsers: mdlOnlineUsers) => {
      if (onlineUsers.status?.includes("join")) {
        if (onlineUsers.user?.id !== activeUser?.id) {
          sendNotification(
            onlineUsers.user?.name!,
            "Joined from server",
            getUserImage(onlineUsers.user?.image)
          );
          playAudio(enmSoundType.join);
        }

        var Rooms: string[] = [];
        onlineUsers.onlineUsers?.forEach((u: mdlUserDto) => {
          if (activeUser && activeUser?.id && u.id !== activeUser?.id) {
            let prepareRoomId = [u.id!, activeUser?.id!];
            prepareRoomId.sort();
            Rooms.push(prepareRoomId[0] + prepareRoomId[1]);
          }
        });
        Rooms.forEach((room: string) => {
          newConnection?.invoke("JoinRoom", room);
        });
      } else if (onlineUsers.status?.includes("leave")) {
        sendNotification(
          onlineUsers.user?.name!,
          "Leaved from server",
          getUserImage(onlineUsers.user?.image)
        );
        playAudio(enmSoundType.leave);
      }

      await getUsers(onlineUsers);
    });

    const startConnection = async () => {
      try {
        await newConnection.start();
      } catch (error) {
        console.error("Error while establishing connection:", error);
        setTimeout(startConnection, 2000);
      }
    };
    startConnection();
    setLoadingScreen(false);
  };

  const getUsers = async (onlineUsers: mdlOnlineUsers) => {
    const response = await UserService.List();

    if (response.success && response.body != undefined) {
      response.body?.forEach((user) => {
        user.status = onlineUsers.onlineUsers?.some(
          (onlineUser) => onlineUser.id === user.id
        );
      });
      setUsers(response.body);
    } else HandleLogout();
  };

  const getMessages = async (receiverUserId: string) => {
    setLoadingScreen(true);
    const items = document.querySelectorAll(".user");
    items.forEach((item) => item.classList.remove("activeUser"));
    document.getElementById("id" + receiverUserId)?.classList.add("activeUser");

    let prepareRoomId = [receiverUserId, activeUser?.id!];
    prepareRoomId.sort();
    var listMessageRequest = new mdlListMessageRequest(
      prepareRoomId[0] + prepareRoomId[1]
    );
    var token = CookieManager.getCookie("token");

    if (listMessageRequest && token) {
      let response = await MessageService.List(listMessageRequest);
      if (response.success && response.body) {
        setMessages(response.body);
        sessionStorage.setItem("room", listMessageRequest.room);
        connection?.invoke("JoinRoom", listMessageRequest.room);
      }
    }
    setLoadingScreen(false);
  };

  function scrollToBottom() {
    const messagesContainer = document.getElementById("messages-container");
    if (messagesContainer)
      messagesContainer.scrollTop = messagesContainer.scrollHeight;
  }

  return (
    <div>
      <div
        className={`custom-row-md align-items-center justify-content-center mt-md-5 ${
          loadingScreen && "loading-screen-active"
        }`}
      >
        <User getMessages={getMessages} users={users} activeUser={activeUser}/>
        <Message messages={messages} scrollToBottom={scrollToBottom} activeUser={activeUser}/>
        {loadingScreen && <LoadingSpinner />}
      </div>
    </div>
  );
};

export default Home;
