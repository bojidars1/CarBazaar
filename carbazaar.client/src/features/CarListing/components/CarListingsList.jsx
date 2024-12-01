import { Box, CircularProgress, Typography } from '@mui/material';
import axios from 'axios';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import CarListingCard from './CarListingCard';
import { useNavigate } from 'react-router-dom';

const CarListings = () => {
    const navigate = useNavigate();

    const [carListings, setCarListings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
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
    }, []);

    const handleDetailsClick = (id) => {
        navigate(`/${id}`)
    };

    return (
        <Box sx={{ p: 2 }}>
            <Typography variant='h4' sx={{ textAlign: 'center', mb: 3}}>
                Car Listings
            </Typography>
            
            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5 }}>
                    <CircularProgress />
                </Box>
            ) : error ? (
                <Typography color='error' variant='h6' sx={{ textAlign: 'center' }}>
                    {error}
                </Typography>
            ) : (
                <Box sx={{ display: 'flex', flexWrap: 'wrap', justifyContent: 'center' }}
                >
                    {carListings.map((car) => (
                        <CarListingCard key={car.id} car={car} onDetailsClick={handleDetailsClick} />
                    ))}
                </Box>
            )}
        </Box>
    );
};

export default CarListings;