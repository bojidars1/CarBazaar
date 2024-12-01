import { Button, Paper, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const DeleteCarListing = () => {
    const { id } = useParams();

    return (
    <Paper elevation={3}>
        <Typography variant='h3' color='error'>Delete Car Listing: ?</Typography>
        <Button variant='contained' color='error'>Delete</Button>
        <Button variant='outlined' color='secondary'>Back to Listings</Button>
    </Paper>
    );
};

export default DeleteCarListing;