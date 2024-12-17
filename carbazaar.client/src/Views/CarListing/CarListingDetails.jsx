import { Box, Button, CardContent, CardMedia, Dialog, DialogActions, DialogContent, DialogTitle, Grid2, Paper, Typography } from '@mui/material';
import api from '../../api/api';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Star, StarBorder } from '@mui/icons-material';
import { useSelector } from 'react-redux';

const CarListingDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const user = useSelector((state) => state.user.user);
    const token = useSelector((state) => state.auth.token);

    const [carListing, setCarListing] = useState(null);
    const [contactOpen, setContactOpen] = useState(false);
    const [isFavourite, setIsFavourite] = useState(false);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    const handleContactOpen = () => {
        setContactOpen(true);
    };

    const handleContactClose = () => {
        setContactOpen(false);
    };

    const handleToggleFavourite = async () => {
        try {
            if (isFavourite) {
                await api.delete(`/FavouriteCarListing/${id}`);
            } else {
                await api.post(`/FavouriteCarListing/${id}`);
            }
            setIsFavourite(!isFavourite);
        } catch (err) {
            setError('Failed to update favourites');
        }
    };

    const handleStartChat = () => {
        console.log(carListing)
        if (carListing && carListing.sellerId) {
            navigate(`/chat/${carListing.id}/${carListing.sellerId}`);
            handleContactClose();
        } else {
            setError('Seller information is unavailable.');
        }
    }

    useEffect(() => {
        const fetchCarDetailsAsync = async () => {
            try {
                const respone = await api.get(`/CarListing/${id}`);
                setCarListing(respone.data);
            } catch (err) {
                setError('Failed to fetch car listing details.');
            } finally {
                setLoading(false);
            }
        };

        const fetchCarFavouriteStatusAsync = async () => {
            try {
                const respone = await api.get(`/FavouriteCarListing/get-favourites`);
                setIsFavourite(respone.data.items.some(item => item.id === id));
            } catch (err) {
                setError('Failed to fetch car favourite status');
            }
        }

        fetchCarDetailsAsync();
        fetchCarFavouriteStatusAsync();
    }, [id]);

    if (loading) {
        return (
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh'
            }}>
                <Typography variant='h6'>Loading...</Typography>
            </Box>
        );
    }

    if (error || !carListing) {
        return (
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh'
            }}>
                <Typography variant='h6'>{error || "Car listing not found."}</Typography>
            </Box>
        );
    }

    return (
        <Box sx={{
            maxWidth: '1200px',
            margin: 'auto',
            mt: 5,
            p: 3,
            backgroundColor: '#f9f9f9',
            borderRadius: 3,
            boxShadow: '0 4px 20px rgba(0, 0, 0, 0.1)'
        }}>
            <Paper elevation={3} sx={{ overflow: 'hidden', borderRadius: 3 }}>
                <Grid2 container>
                    {/* Image Section */}
                    <Grid2 xs={12} md={6}>
                        <CardMedia 
                        component='img'
                        image={carListing.imageURL || 'https://www.ilusso.com/wp-content/uploads/3-3-1024x683.jpg'}
                        alt={carListing.name}
                        sx={{
                            height: '100%',
                            objectFit: 'cover'
                        }}
                        />
                    </Grid2>

                    {/* Car Details */}
                    <Grid2 xs={12} md={6}>
                        <CardContent sx={{ p: 4 }}>
                            <Typography variant='h4' gutterBottom>{carListing.name}</Typography>
                            <Typography variant='h6' gutterBottom color='text.secondary'>
                                {carListing.brand} - {carListing.type}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Price:</strong> ${carListing.price}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Gearbox:</strong> {carListing.gearbox}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>State:</strong> {carListing.state}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Kilometers:</strong> {carListing.km} KM
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Production Year:</strong> {carListing.productionYear}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Horsepower:</strong> {carListing.horsepower} hp
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Color:</strong> {carListing.color}
                            </Typography>

                            <Typography variant='body1' sx={{ mb: 2 }}>
                                <strong>Extra Info:</strong> {carListing.extraInfo || "No additional information available."}
                            </Typography>

                            <Box sx={{ mt: 3 }}>
                                <Button
                                variant='contained'
                                color={isFavourite ? 'primary' : 'default'}
                                startIcon={isFavourite ? <Star /> : <StarBorder />}
                                onClick={handleToggleFavourite}
                                >
                                    {isFavourite ? 'Remove from Favourites' : 'Add to Favourites'}
                                </Button>
                            </Box>
                        </CardContent>
                    </Grid2>
                </Grid2>
            </Paper>

            {/* CTA */}
            <Box sx={{
                display: 'flex',
                justifyContent: 'space-between',
                mt: 4,
                px: 2
            }}>
                <Button variant='contained' color='primary' onClick={() => navigate("/carlisting/list")}>
                    Back To Listings
                </Button>
                {user && token && user.userId !== carListing.sellerId &&
                <Button variant='contained' color='secondary' onClick={handleContactOpen}>
                Contact Seller
                </Button>
                }

                {/* Contact Dialog */}
                <Dialog open={contactOpen} onClose={handleContactClose}>
                    <DialogTitle>Contact the Seller</DialogTitle>
                    <DialogContent>
                        <Typography variant='body1' sx={{ mb: 2 }}>
                            For inquiries about this car, you can:
                        </Typography>
                        <Typography variant='body2' sx={{ mb: 1 }}>
                            - <strong>Phone Number</strong>: {"+359893456569"}
                        </Typography>
                        <Button
                            variant='outlined'
                            color='primary'
                            onClick={handleStartChat}
                        >
                            Start Chat
                        </Button>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleContactClose}>Close</Button>
                    </DialogActions>
                </Dialog>
            </Box>
        </Box>
    );
};

export default CarListingDetails;