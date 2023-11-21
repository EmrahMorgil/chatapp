"use client";
import React, { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import styles from "./page.module.css";

const Page = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [message, setMessage] = useState<string>("");
  const [messages, setMessages] = useState<Array<string>>([]);
  const [activeUsers, setActiveUsers] = useState<Array<string>>([]);

  const fnConnect = () => {

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5000/chat-hub")
      .withAutomaticReconnect()
      .build();

    newConnection.on("ReceiveMessage", (receivedMessage) => {
      setMessages((prevMessages) => [...prevMessages, receivedMessage]);
    });

    newConnection.on("clients", (clients) => {
      setActiveUsers(clients);
    });

    setConnection(newConnection);

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


  const sendMessage = () => {
    if (connection && message !== "") {
      connection.invoke("SendMessage", message);
      setMessage('');
    }
  };

  return (
    <div className="container">
      <input onChange={(e: any) => setMessage(e.target.value)} />
      <button onClick={sendMessage}>Click</button>
      <button onClick={() => fnConnect()}>connect</button>
      <div>
        <h4>Messages</h4>
        {messages.map((m, index) =>
          <p key={index}>{m}</p>
        )}
      </div>
      <div style={{ marginTop: "15rem" }}>
        <h4>Active User Ids</h4>
        {activeUsers.map((u, index) =>
          <p key={index} style={{ color: "red" }}>{u}</p>
        )}
      </div>


      <>
        {/* Button trigger modal */}
        <button
          type="button"
          className="btn btn-primary"
          data-bs-toggle="modal"
          data-bs-target="#exampleModal"
        >
          Launch demo modal
        </button>
        {/* Modal */}
        <div
          className="modal fade"
          id="exampleModal"
          tabIndex={-1}
          aria-labelledby="exampleModalLabel"
          aria-hidden="true"
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title" id="exampleModalLabel">
                  Modal title
                </h5>
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                />
              </div>
              <div className="modal-body">...</div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  data-bs-dismiss="modal"
                >
                  Close
                </button>
                <button type="button" className="btn btn-primary">
                  Save changes
                </button>
              </div>
            </div>
          </div>
        </div>
      </>


    </div>
  );
};

export default Page;
