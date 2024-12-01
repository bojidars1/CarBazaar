import { Button, Dialog, DialogActions, DialogContent, DialogTitle, Typography } from '@mui/material';
import axios from 'axios';
import React from 'react';

const DeleteCarListingDialog = (carListing, openContact, handleContactClose) => {
    const handleDeletion = async () => {
        try {
            await axios.delete('https://localhost:7100/api/CarListing/');
        }
    };

    <Dialog open={openContact} onClose={handleContactClose}>
        <DialogTitle>Delete CarListing: {carListing.name}</DialogTitle>
        <DialogContent>
            <Typography>Are you sure you want to delete this listing?</Typography>
        </DialogContent>
        <DialogActions>
            <Button onClick={handleDeletion} variant='contained' color='error'>Delete</Button>
            <Button onClick={handleContactClose} variant='outlined' color='secondary'>Close</Button>
        </DialogActions>
    </Dialog>
};

export default DeleteCarListingDialog;