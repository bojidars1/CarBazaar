import { Box, CircularProgress, Typography, Grid2, Pagination, Button } from '@mui/material';
import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import api from '../../api/api';
import CarListingCard from '../../features/CarListing/components/CarListingCard';
import { Link } from 'react-router-dom';

const UserCarListingList = () => {
    const [carListings, setCarListings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const pageSize = 1;

    const fetchCarListings = async (page) => {
        const params = {
            page,
            pageSize
        };

        try {
            const response = await api.get('/UserCarListing/get-listings', { params });
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

    if (carListings.length === 0) {
        return (
            <Box sx={{ p: 2, display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '1em'}}>
                <Typography variant="h4" sx={{ textAlign: 'center', mb: 3 }}>
                    My Car Listings
                </Typography>
                <Button
                component={Link} to="/carlisting/add"
                variant='contained'
                color='primary'
                size='large'
                sx={{
                textTransform: 'none',
                fontWeight: 'bold',
                }}
                >
                Sell Your Car Now
                </Button>
            </Box>
            
        );
    }

    return (
        <Box sx={{ p: 2 }}>
            <Typography variant="h4" sx={{ textAlign: 'center', mb: 3 }}>
               My Car Listings
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

export default UserCarListingList;