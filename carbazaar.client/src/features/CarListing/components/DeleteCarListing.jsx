import { Button, Paper, Typography, Box } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const DeleteCarListing = () => {
    const { id } = useParams();
    

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
            <Button variant='outlined' color='secondary' sx={{ width: '40%' }}>
                Back to Listings
            </Button>
            <Button variant='contained' color='error' sx={{ width: '40%' }}>
                Delete
            </Button>
        </Box>
    </Paper>
    );
};

export default DeleteCarListing;