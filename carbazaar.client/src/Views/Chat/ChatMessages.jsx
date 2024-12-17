import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import api from '../../api/api';
import { Box, Button, CircularProgress, Typography, TextField } from '@mui/material';
import * as signalR from '@microsoft/signalr';
import { useSelector } from 'react-redux';

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
            const response = await api.get(`/Chat/messages/${carListingId}/${participantId}`);
            setMessages(response.data);
        }
        catch (err) {
            console.error(err);
            navigate('/error');
        }
        finally {
            setLoading(false);
        }
    };

    const sendMessage = async () => {
        if (!newMessage.trim()) return;

        try {
            await api.post('/Chat/send', {
                receiverId: participantId,
                message: newMessage,
                carListingId
            });
            setNewMessage('');
            fetchMessages();
        } catch (err) {
            console.error(err);
            navigate('/error');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMessages();

        const connect = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:7100/chathub', {
            accessTokenFactory: () => localStorage.getItem('token')
        })
        .withAutomaticReconnect()
        .build();

       setConnection(connect);

       connect.on('ReceiveMessage', (message) => {
        setMessages(prev => [...prev, message]);
       });

       connect.start().catch((err) => console.error(err));

        return () => {
          connect.stop();
        };
    }, []);

    return (
        <Box sx={{ p: 3 }}>
            <Typography variant="h4" sx={{ mb: 3, textAlign: 'center' }}>Chat</Typography>
            <Box sx={{ mb: 2 }}>
                {loading ? (
                    <CircularProgress />
                ) : messages.length === 0 ? (
                    <Typography>No messages yet</Typography>
                ) : (
                    messages.map((msg, index) => (
                        <Box key={index} sx={{ mb: 1 }}>
                            <Typography><strong>{msg.senderId === participantId ? "Them" : "You"}:</strong> {msg.message}</Typography>
                        </Box>
                    ))
                )}
            </Box>
            <TextField
                fullWidth
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                placeholder="Type a message..."
                sx={{ mb: 2 }}
            />
            <Button 
            variant="contained" 
            onClick={() => {
                sendMessage();
                if (connection) {
                    connection.invoke('SendMessage', carListingId, participantId, newMessage)
                    .catch(err => console.error("Error sending message: ", err));
                    
                    const notificationMessage = `You have a new message from ${user.userEmail || 'Client'}`;
                    connection.invoke('SendNotification', participantId, notificationMessage)
                    .catch(err => console.error("Error sending notification: ", err));
                }
            }}>
                Send
            </Button>
        </Box>
    );
};

export default ChatMessages;