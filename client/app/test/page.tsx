"use client";
import React, { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import axios from "axios";

export class AddMessageCommand {
  senderId?: string;
  room?: string;
  message?: string;
}
export class GetAllMessagesCommand {
  takerId?: string;
  senderId?: string;
}
export class mdlMessage {
  id?: string;
  message?: string;
  room?: string;
  createdDate?: Date;
}

const Page = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [message, setMessage] = useState<string>("");
  const [messages, setMessages] = useState<Array<mdlMessage>>([]);
  const [activeUsers, setActiveUsers] = useState<Array<string>>([]);
  const [room, setRoom] = useState("");


  useEffect(() => {
    if (!connection)
      fnConnect();
  }, [])


  const fnConnect = () => {

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5000/chat-hub")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", (message) => {
      setMessages((prevMessages) => [...prevMessages, message]);
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

  const sendMessage = async () => {
    if (connection && room && message) {
      // connection.invoke("SendMessageToRoom", room, message);
      var x = new AddMessageCommand();
      x.room = room;
      x.message = message;
      x.senderId = "1";
      let request = await axios.post("https://localhost:5000/api/Message/AddMessage", x);
      setMessage('');
    }
  }

  const connectRoom = async () => {
    if (connection && room) {
      connection.invoke("JoinRoom", room);
      var x = new GetAllMessagesCommand();
      x.takerId = room;
      x.senderId = "5";
      let request = await axios.post("https://localhost:5000/api/Message/GetMessages", x);
      setMessages(request.data.body)
    }
  }

  return (
    <div className="container">
      <input value={message} onChange={(e: any) => setMessage(e.target.value)} />
      <button onClick={sendMessage}>Click</button>
      <input value={room} onChange={(e) => setRoom(e.target.value)} />
      <button onClick={() => connectRoom()}>connect</button>
      <div>
        <h4>Messages</h4>
        {messages.map((m, index) =>
          <p key={index}>{m.message}</p>
        )}
      </div>
      <div style={{ marginTop: "15rem" }}>
        <h4>Active User Ids</h4>
        {activeUsers.map((u, index) =>
          <p key={index} style={{ color: "red" }}>{u}</p>
        )}
      </div>

    </div>
  );
};

export default Page;
