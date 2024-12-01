import { Button, Paper, Typography } from '@mui/material';
import React from 'react';

const DeleteCarListing = (carlisting) => {
    <Paper elevation={3}>
        <Typography variant='h3' color='error'>Delete Car Listing: {carlisting.name}?</Typography>
        <Button variant='contained' color='error'>Delete</Button>
        <Button variant='outlined' color='secondary'>Back to Listings</Button>
    </Paper>
};

export default DeleteCarListing;