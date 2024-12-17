import { Card, CardContent, CardMedia, CardActions, Typography, Grid2, Button, Box } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

const CarListingCard = ({ car }) => {
    const navigate = useNavigate();

    const user = useSelector((state) => state.user.user);
    const token = useSelector((state) => state.auth.token);

    const handleDetailsClick = (id) => {
        navigate(`/carlisting/details/${id}`);
    };

    const handleOnEditClick = (id) => {
        navigate(`/carlisting/edit/${id}`);
    };

    const handleOnDeleteClick = (id) => {
        navigate(`/carlisting/delete/${id}`);
    };

    return (
        <Grid2 xs={12} sm={6} md={4}>
            <Card
                sx={{
                    display: 'flex',
                    flexDirection: { xs: 'column', sm: 'row' },
                    alignItems: 'center',
                    p: 2,
                    boxShadow: 3,
                    borderRadius: 2,
                    gap: 2,
                    height: '100%',
                }}
            >
                {/* Image */}
                <CardMedia
                    component="img"
                    image={car.imageURL || 'placeholder.jpg'}
                    alt={car.name}
                    sx={{
                        width: { xs: '100%', sm: 150 },
                        height: 100,
                        objectFit: 'cover',
                        borderRadius: 1,
                    }}
                />

                {/* Details */}
                <CardContent
                    sx={{
                        flexGrow: 1,
                        width: '100%',
                        textAlign: { xs: 'center', sm: 'left' }, 
                    }}
                >
                    <Typography variant="h6" fontWeight="bold">
                        {car.name}
                    </Typography>
                    <Typography variant="body1" color="text.secondary">
                        Price: ${car.price.toLocaleString()}
                    </Typography>
                </CardContent>

                {/* Actions */}
                <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, gap: 1 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        onClick={() => handleDetailsClick(car.id)}
                        size="small"
                    >
                        View Details
                    </Button>

                    {user && token && user.userId === car.sellerId && (
                        <>
                            <Button
                                variant="contained"
                                color="secondary"
                                onClick={() => handleOnEditClick(car.id)}
                                size="small"
                            >
                                Edit
                            </Button>
                            <Button
                                variant="contained"
                                color="error"
                                onClick={() => handleOnDeleteClick(car.id)}
                                size="small"
                            >
                                Delete
                            </Button>
                        </>
                    )}
                </Box>
            </Card>
        </Grid2>
    );
};

export default CarListingCard;