import { Card, CardContent, CardMedia, CardActions, Typography, Grid2, Button } from '@mui/material';
import React from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';

const CarListingCard = ({ car }) => {
    const navigate = useNavigate();

    const user = useSelector((state) => state.user.user);
    const token = useSelector((state) => state.auth.token);

    const handleDetailsClick = (id) => {
        navigate(`/carlisting/details/${id}`)
    };

    const handleOnEditClick = (id) => {
        navigate(`/carlisting/edit/${id}`);
    };

    const handleOnDeleteClick = (id) => {
        navigate(`/carlisting/delete/${id}`);
    };

    return (
        <Grid2 key={car.id} xs={12}>
            <Card sx={{ display: 'flex', alignItems: 'center', p: 2, boxShadow: 3 }}>
                {/* Image */}
                <CardMedia
                    component="img"
                    image={car.imageURL || 'placeholder.jpg'} // Placeholder image if no URL
                    alt={car.name}
                    sx={{
                        width: 150,
                        height: 100,
                        objectFit: 'cover',
                        borderRadius: 1
                        }}
                />

                {/* Details */}
                <CardContent sx={{ flexGrow: 1, ml: 2 }}>
                    <Typography variant="h6" fontWeight="bold">
                        {car.name}
                    </Typography>
                    <Typography variant="body1" color="text.secondary">
                        Price: ${car.price.toLocaleString()}
                    </Typography>
                </CardContent>

                {/* Actions */}
                <CardActions>
                <Button
                    variant="contained"
                    color="primary"
                    onClick={() => handleDetailsClick(car.id)}
                >
                    View Details
                </Button>
                {user && token && user.carListings.includes(car.id) &&
                <>
                <Button
                    variant="contained"
                    color="secondary"
                    onClick={() => handleOnEditClick(car.id)}
                >
                    Edit
                </Button>
                <Button
                    variant="contained"
                    color="error"
                    onClick={() => handleOnDeleteClick(car.id)}
                >
                    Delete
                </Button>
                </>
                }
                </CardActions>
            </Card>
        </Grid2>
    );
};

export default CarListingCard;