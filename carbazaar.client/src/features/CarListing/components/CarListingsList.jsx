import { Box, CircularProgress, Typography, Card, Grid2, CardMedia, CardContent, CardActions, Button } from '@mui/material';
import axios from 'axios';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import CarListingCard from './CarListingCard';
import { useLocation, useNavigate } from 'react-router-dom';

const CarListings = () => {
    const navigate = useNavigate();
    const location = useLocation();

    const carListingsState = location.state?.data;

    const [carListings, setCarListings] = useState(carListingsState || []);
    const [loading, setLoading] = useState(!carListingsState);
    const [error, setError] = useState('');

    useEffect(() => {
        if (!carListingsState) {
            const getCarListings = async () => {
                try {
                    const response = await axios.get('https://localhost:7100/api/CarListing');
                    setCarListings(response.data);
                } catch (err) {
                    setError("Failed to get car listings. Please try again.")
                } finally {
                    setLoading(false);
                }
            };

            getCarListings();
        }
    }, [carListingsState]);

    const handleDetailsClick = (id) => {
        navigate(`/carlisting/details/${id}`)
    };

    return (
        <Box sx={{ p: 2 }}>
            <Typography variant="h4" sx={{ textAlign: 'center', mb: 3 }}>
                Car Listings
            </Typography>

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5 }}>
                    <CircularProgress />
                </Box>
            ) : error ? (
                <Typography color="error" variant="h6" sx={{ textAlign: 'center' }}>
                    {error}
                </Typography>
            ) : (
                <Grid2 container spacing={2} direction="column" sx={{ mt: 2 }}>
                    {carListings.map((car) => (
                        <CarListingCard key={car.id} car={car} onDetailsClick={handleDetailsClick} />
                    ))}
                </Grid2>
            )}
        </Box>
    );
};

export default CarListings;