import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../../api/api';
import { Box, Button, CircularProgress, Typography, TextField } from '@mui/material';

const ChatMessages = () => {
    const { carListingId, participantId } = useParams();
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState("");
    const [loading, setLoading] = useState(true);

    const fetchMessages = async () => {
        try {
            const response = await api.get(`/Chat/messages/${carListingId}/${participantId}`);
            setMessages(response.data);
        } finally {
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
            console.log('Failed to send message');
        }
    };

    useEffect(() => {
        fetchMessages();
    }, []);

    return (
        <Box sx={{ p: 3 }}>
            <Typography variant="h4" sx={{ mb: 3 }}>Chat</Typography>
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
            <Button variant="contained" onClick={sendMessage}>Send</Button>
        </Box>
    );
};

export default ChatMessages;