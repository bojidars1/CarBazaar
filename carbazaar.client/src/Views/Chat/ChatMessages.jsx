import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../api/api";
import {
  Box,
  Button,
  CircularProgress,
  Typography,
  TextField,
} from "@mui/material";
import * as signalR from "@microsoft/signalr";
import { useSelector } from "react-redux";

const ChatMessages = () => {
  const navigate = useNavigate();
  const user = useSelector((state) => state.user.user);

  const { carListingId, participantId } = useParams();
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState("");
  const [loading, setLoading] = useState(true);

  const [connection, setConnection] = useState(null);

  const fetchMessages = async () => {
    try {
      const response = await api.get(
        `/Chat/messages/${carListingId}/${participantId}`
      );
      setMessages(response.data);
    } catch (err) {
      console.error(err);
      navigate("/error");
    } finally {
      setLoading(false);
    }
  };

  const sendMessage = async () => {
    if (!newMessage.trim()) return;

    try {
      await api.post("/Chat/send", {
        receiverId: participantId,
        message: newMessage,
        carListingId,
      });
      setNewMessage("");
      fetchMessages();
    } catch (err) {
      console.error(err);
      navigate("/error");
    }
  };

  useEffect(() => {
    fetchMessages();

    const connect = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7100/chathub", {
        accessTokenFactory: () => localStorage.getItem("token"),
      })
      .withAutomaticReconnect()
      .build();

    setConnection(connect);

    connect.on("ReceiveMessage", (message) => {
      setMessages((prev) => [...prev, message]);
    });

    connect.start().catch((err) => console.error(err));

    return () => {
      connect.stop();
    };
  }, []);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        height: "90vh",
        maxWidth: "800px",
        margin: "auto",
        border: "1px solid #ddd",
        borderRadius: "8px",
        boxShadow: 2,
      }}
    >
      <Box
        sx={{
          backgroundColor: "primary.main",
          color: "white",
          padding: "16px",
          borderTopLeftRadius: "8px",
          borderTopRightRadius: "8px",
        }}
      >
        <Typography variant="h5" textAlign="center">
          Chat
        </Typography>
      </Box>

      <Box
        sx={{
          flex: 1,
          overflowY: "auto",
          padding: "16px",
          display: "flex",
          flexDirection: "column",
          gap: "8px",
          backgroundColor: "#f9f9f9",
        }}
      >
        {loading ? (
          <CircularProgress sx={{ alignSelf: "center" }} />
        ) : messages.length === 0 ? (
          <Typography textAlign="center">No messages yet</Typography>
        ) : (
          messages.map((msg, index) => (
            <Box
              key={index}
              sx={{
                display: "flex",
                justifyContent:
                  msg.senderId === user.userId ? "flex-end" : "flex-start",
              }}
            >
              <Box
                sx={{
                  backgroundColor:
                    msg.senderId === user.userId ? "primary.light" : "#ddd",
                  color: msg.senderId === user.userId ? "white" : "black",
                  padding: "8px 12px",
                  borderRadius: "16px",
                  maxWidth: "60%",
                  wordWrap: "break-word",
                }}
              >
                <Typography variant="body1" sx={{ fontSize: "0.9rem" }}>
                  {msg.message}
                </Typography>
              </Box>
            </Box>
          ))
        )}
      </Box>

      <Box
        sx={{
          display: "flex",
          padding: "8px",
          borderTop: "1px solid #ddd",
          backgroundColor: "white",
        }}
      >
        <TextField
          value={newMessage}
          onChange={(e) => setNewMessage(e.target.value)}
          placeholder="Type a message..."
          variant="outlined"
          fullWidth
          multiline
          maxRows={3}
          sx={{ mr: 1 }}
        />
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            sendMessage();
            if (connection) {
              connection
                .invoke("SendMessage", carListingId, participantId, newMessage)
                .catch((err) => console.error("Error sending message: ", err));

            //   const notificationMessage = `You have a new message from ${
            //     user.userEmail || "Client"
            //   }`;
            //   connection
            //     .invoke("SendNotification", participantId, notificationMessage)
            //     .catch((err) =>
            //       console.error("Error sending notification: ", err)
            //     );
            }
          }}
        >
          Send
        </Button>
      </Box>
    </Box>
  );
};

export default ChatMessages;