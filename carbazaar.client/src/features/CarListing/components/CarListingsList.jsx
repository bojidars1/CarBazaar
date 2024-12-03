import { Box, CircularProgress, Typography, Card, Grid2, CardMedia, CardContent, CardActions, Button, Pagination } from '@mui/material';
import axios from 'axios';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import CarListingCard from './CarListingCard';
import { useLocation, useNavigate } from 'react-router-dom';

const CarListings = () => {
    const navigate = useNavigate();
    const location = useLocation();

    const carListingsState = location.state?.params;

    const [carListings, setCarListings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const pageSize = 1;

    const fetchCarListings = async (page) => {
        const params = {
            type: carListingsState?.type,
            brand: carListingsState?.brand,
            priceRange: carListingsState?.priceRange,
            page,
            pageSize
        };

        try {
            const response = await axios.get('https://localhost:7100/api/CarListing/search', { params });
            setCarListings(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (err) {
            setError('Failed to get car listings. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchCarListings(page);
    }, [page]);

    const handlePageChange = (e, value) => {
        setPage(value);
    }

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
                <>
                   <Grid2 container spacing={2} direction="column" sx={{ mt: 2 }}>
                       {carListings.map((car) => (
                          <CarListingCard key={car.id} car={car} onDetailsClick={handleDetailsClick} />
                       ))}
                   </Grid2>

                   {/* Pagination */}
                   <Box sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    mt: 3
                   }}>
                    <Pagination
                    count={totalPages}
                    page={page}
                    onChange={handlePageChange}
                    color='primary'
                    size='large'
                    />
                   </Box>
                </>
            )}
        </Box>
    );
};

export default CarListings;