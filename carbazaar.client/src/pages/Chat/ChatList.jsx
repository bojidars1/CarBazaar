import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/api';
import { Box, Typography, CircularProgress, List, ListItem, ListItemText } from '@mui/material';

const ChatList = () => {
    const navigate = useNavigate();

    const [chats, setChats] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchChats = async () => {
        try {
            const response = api.get('/Chat/summaries');
            setChats(response.data);
        } catch (err) {
            setError('Failed to fetch chat summaries. Try again');
        } finally {
            setLoading(false);
        }
    };

    const handleChatClick = (carId, participantId) => {
        console.log('open chat');
    };

    useEffect(() => {
        fetchChats();
    }, []);

    return (
        <Box sx={{ p: 3 }}>
            <Typography variant="h4" sx={{ mb: 2 }}>Chats</Typography>
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
                            button 
                            key={`${chat.CarListingId}-${chat.OtherParticipantId}`} 
                            onClick={() => handleChatClick(chat.CarListingId, chat.OtherParticipantId)}
                        >
                            <ListItemText
                                primary={`Chat with ${chat.OtherParticipantName}`}
                                secondary={`Last Message: ${chat.LastMessage}`}
                            />
                        </ListItem>
                    ))}
                </List>
            )}
        </Box>
    );
};

export default ChatList;