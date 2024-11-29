import { Card, CardContent, CardMedia, Typography, Box, Button } from '@mui/material';
import React from 'react';

const CarListingCard = ({ car, onDetailsClick }) => {
    return (
        <Card sx={{ maxWidth: 345, margin: 2, boxShadow: 3 }}>
            {/* Car Image */}
            <CardMedia
            component="img"
            height='200'
            image={car.imageURL || 'https://www.ilusso.com/wp-content/uploads/3-3-1024x683.jpg'}
            alt={car.name}
            />

            {/* Car Details */}
            <CardContent>
                <Typography variant='h6' component="div" sx={{ fontWeight: 'bold' }}>
                    {car.name}
                </Typography>

                <Typography variant='body2' color='text.secondary'>
                    Price: ${car.price}
                </Typography>
            </CardContent>

            {/* CTA Button */}
            <Box sx={{ p: 2 }}>
                <Button
                variant='contained'
                color='primary'
                fullWidth
                onClick={() => onDetailsClick(car.id)}
                >
                    View Details
                </Button>
            </Box>
        </Card>
    );
};

export default CarListingCard;