import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/api';
import { Box, Typography, CircularProgress, List, ListItem, ListItemText, Button } from '@mui/material';

const ChatList = () => {
    const navigate = useNavigate();

    const [chats, setChats] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchChats = async () => {
        try {
            const response = await api.get('/Chat/summaries');
            setChats(response.data);
        } catch (err) {
            setError('Failed to fetch chat summaries. Try again');
        } finally {
            setLoading(false);
        }
    };

    const handleChatClick = (carListingId, participantId) => {
        navigate(`/chat/${carListingId}/${participantId}`);
    };

    useEffect(() => {
        fetchChats();
    }, []);

    return (
        <Box sx={{ p: 3 }}>
            <Typography variant="h4" sx={{ mb: 2, textAlign: 'center' }}>Chats</Typography>
            {loading ? (
                <CircularProgress />
            ) : error ? (
                <Typography color="error">{error}</Typography>
            ) : chats.length === 0 ? (
                <Typography>No chats found</Typography>
            ) : (
                <List>
                    {chats.map(chat => (
                        <ListItem
                            component={Button}
                            key={`${chat.carListingId}-${chat.otherParticipantId}`}
                            onClick={() => handleChatClick(chat.carListingId, chat.otherParticipantId)}
                        >
                            <ListItemText
                                primary={`Chat with ${chat.otherParticipantName}`}
                                secondary={`Last Message: ${chat.lastMessage}`}
                            />
                        </ListItem>
                    ))}
                </List>
            )}
        </Box>
    );
};

export default ChatList;