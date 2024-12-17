import { Button, Paper, Typography, Box, CircularProgress } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import api from '../../api/api';

const DeleteCarListing = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [carListingName, setCarListingName] = useState('');
    const [loading, setLoading] = useState(true);

    const handleSubmit = async () => {
        try {
            await api.delete(`/CarListing/${id}`);
            navigate('/carlisting/list');
        } catch (err) {
            console.error(err);
            navigate('/error');
        }
    };

    useEffect(() => {
        const fetchDeleteCarListingDto = async () => {
            try {
                const response = await api.get(`/CarListing/delete/${id}`);
                setCarListingName(response.data.name);
            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false);
            }
        };

        fetchDeleteCarListingDto();
    }, [id]);

    if (loading) {
        return (
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    textAlign: 'center',
                }}
            >
                <CircularProgress />
            </Box>
        );
    }

    if (!carListingName) {
        return (
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    textAlign: 'center',
                }}
            >
                <Typography variant="h6" color="error">
                    Car listing not found.
                </Typography>
            </Box>
        );
    }

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                px: 2,
            }}
        >
            <Paper
                elevation={6}
                sx={{
                    p: 4,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    maxWidth: { xs: '100%', sm: '90%', md: '60%' },
                    textAlign: 'center',
                    borderRadius: 3,
                    boxShadow: 5,
                    backgroundColor: '#fefefe',
                }}
            >
                <Typography
                    variant="h4"
                    color="error"
                    sx={{ fontWeight: 'bold', mb: 2, fontSize: { xs: '1.8rem', md: '2.5rem' } }}
                >
                    Delete Car Listing
                </Typography>
                <Typography
                    variant="h6"
                    color="text.secondary"
                    sx={{ mb: 4, fontSize: { xs: '1rem', md: '1.2rem' } }}
                >
                    Are you sure you want to delete <strong>{carListingName}</strong>?
                </Typography>

                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'space-around',
                        width: '100%',
                        gap: 2,
                        flexDirection: { xs: 'column', sm: 'row' },
                    }}
                >
                    <Button
                        variant="contained"
                        color="secondary"
                        onClick={() => navigate('/carlisting/list')}
                        sx={{
                            flex: 1,
                            borderRadius: 2,
                            px: 3,
                            py: 1.5,
                            fontSize: { xs: '0.9rem', md: '1rem' },
                        }}
                    >
                        Back to Listings
                    </Button>
                    <Button
                        variant="contained"
                        color="error"
                        onClick={handleSubmit}
                        sx={{
                            flex: 1,
                            borderRadius: 2,
                            px: 3,
                            py: 1.5,
                            fontSize: { xs: '0.9rem', md: '1rem' },
                        }}
                    >
                        Delete
                    </Button>
                </Box>
            </Paper>
        </Box>
    );
};

export default DeleteCarListing;