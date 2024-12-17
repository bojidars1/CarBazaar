import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/api';
import {
    Box,
    Typography,
    CircularProgress,
    List,
    ListItem,
    ListItemText,
    Button,
    Pagination,
    Stack
} from '@mui/material';

const ChatList = () => {
    const navigate = useNavigate();

    const [chats, setChats] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 10;

    const fetchChats = async (page) => {
        setLoading(true);
        setError(null);
        try {
            const response = await api.get('/Chat/summaries', {
                params: {
                    page: page,
                    pageSize: pageSize
                }
            });
            setChats(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (err) {
            console.error(err);
            setError('Failed to fetch chat summaries. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    const handlePageChange = (event, value) => {
        setCurrentPage(value);
    };

    const handleChatClick = (carListingId, participantId) => {
        navigate(`/chat/${carListingId}/${participantId}`);
    };

    useEffect(() => {
        fetchChats(currentPage);
    }, [currentPage]);

    return (
        <Box sx={{ p: 3 }}>
            <Typography variant="h4" sx={{ mb: 2, textAlign: 'center' }}>Chats</Typography>
            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
                    <CircularProgress />
                </Box>
            ) : error ? (
                <Typography color="error" sx={{ textAlign: 'center' }}>{error}</Typography>
            ) : chats.length === 0 ? (
                <Typography sx={{ textAlign: 'center' }}>No chats found</Typography>
            ) : (
                <>
                    <List>
                        {chats.map(chat => (
                            <ListItem
                                button
                                key={`${chat.carListingId}-${chat.otherParticipantId}`}
                                onClick={() => handleChatClick(chat.carListingId, chat.otherParticipantId)}
                                sx={{
                                    border: '1px solid #ddd',
                                    borderRadius: '8px',
                                    mb: 1,
                                    '&:hover': {
                                        backgroundColor: '#f5f5f5',
                                    },
                                }}
                            >
                                <ListItemText
                                    primary={`Chat with ${chat.otherParticipantName || 'Unknown'}`}
                                    secondary={
                                        <>
                                            <Typography
                                                component="span"
                                                variant="body2"
                                                color="textPrimary"
                                            >
                                                Last Message: {chat.lastMessage}
                                            </Typography>
                                            <br />
                                            <Typography
                                                component="span"
                                                variant="caption"
                                                color="textSecondary"
                                            >
                                                {new Date(chat.lastMessageTimestamp).toLocaleString()}
                                            </Typography>
                                        </>
                                    }
                                />
                            </ListItem>
                        ))}
                    </List>
                    <Stack spacing={2} sx={{ mt: 2, alignItems: 'center' }}>
                        <Pagination
                            count={totalPages}
                            page={currentPage}
                            onChange={handlePageChange}
                            color="primary"
                            showFirstButton
                            showLastButton
                        />
                    </Stack>
                </>
            )}
        </Box>
    );
};

export default ChatList;