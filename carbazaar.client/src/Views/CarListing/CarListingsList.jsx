import { Box, CircularProgress, Typography, Grid2, Pagination } from '@mui/material';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import CarListingCard from '../../components/CarListing/CarListingCard';
import { useLocation } from 'react-router-dom';
import api from '../../api/api';

const CarListings = () => {
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
            const response = await api.get('/CarListing/search', { params });
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
                          <CarListingCard key={car.id} car={car} />
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