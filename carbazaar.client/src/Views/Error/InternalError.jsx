import React from 'react';
import { Box, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const InternalError = () => {
    const navigate = useNavigate();

    return (
        <Box
            sx={{
                height: '100vh',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                textAlign: 'center',
                p: 3,
            }}
        >
            <Typography variant="h1" color="error" gutterBottom>
                500
            </Typography>
            <Typography variant="h5" gutterBottom>
                Internal Server Error
            </Typography>
            <Typography variant="body1" gutterBottom>
                Something went wrong on our end.
            </Typography>
            <Button variant="contained" color="primary" onClick={() => navigate('/')}>
                Go to Home
            </Button>
        </Box>
    );
};

export default InternalError;