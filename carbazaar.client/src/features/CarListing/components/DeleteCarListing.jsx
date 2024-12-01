import { Button, Paper, Typography, Box } from '@mui/material';
import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const DeleteCarListing = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const handleSubmit = async () => {
        try {
            await axios.delete('https://localhost:7100/api/CarListing/${id}');
            navigate('/carlisting/list');
        } catch (err) {
            console.error(err);
        }
    };

    return (
    <Paper elevation={6} sx={{
        p: 4,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        maxWidth: '80%',
        margin: 'auto',
        borderRadius: 2,
        boxShadow: 3
    }}>
        <Typography variant='h4' color='error'>Delete Car Listing: <strong>Listing</strong>?</Typography>
        <Box sx={{
            display: 'flex',
            justifyContent: 'space-between',
            width: '100%',
            mt: 5
        }}>
            <Button variant='outlined' color='secondary' onClick={() => navigate("/carlisting/list")} sx={{ width: '40%' }}>
                Back to Listings
            </Button>
            <Button variant='contained' color='error' onClick={handleSubmit} sx={{ width: '40%' }}>
                Delete
            </Button>
        </Box>
    </Paper>
    );
};

export default DeleteCarListing;