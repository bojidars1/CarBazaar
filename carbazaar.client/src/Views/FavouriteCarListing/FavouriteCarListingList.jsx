import React, { useEffect, useState } from 'react';
import api from '../../api/api';
import {
    Box,
    CircularProgress,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
    Button,
    Pagination,
    useMediaQuery,
    Card,
    CardContent,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';

const FavouriteCarListingList = () => {
    const navigate = useNavigate();
    const isSmallScreen = useMediaQuery((theme) => theme.breakpoints.down('sm'));

    const [favouriteListings, setFavouriteListings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const pageSize = 6;

    const fetchFavourites = async (page) => {
        const params = { page, pageSize };
        try {
            const response = await api.get('/FavouriteCarListing/get-favourites', { params });
            setFavouriteListings(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (err) {
            console.error(err);
            navigate('/error');
        } finally {
            setLoading(false);
        }
    };

    const handlePageChange = (e, value) => setPage(value);

    const handleDetailsClick = (id) => navigate(`/carlisting/details/${id}`);

    const handleRemoveClick = async (id) => {
        try {
            await api.delete(`/FavouriteCarListing/${id}`);
            if (page === totalPages && page > 1) setPage(page - 1);
            await fetchFavourites(page);
        } catch (err) {
            console.error(err);
            navigate('/error');
        }
    };

    useEffect(() => {
        fetchFavourites(page);
    }, [page]);

    return (
        <Box sx={{ p: 2 }}>
            <Typography variant="h4" sx={{ textAlign: 'center', mb: 3 }}>
                Favourites
            </Typography>

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5 }}>
                    <CircularProgress />
                </Box>
            ) : (
                <>
                    {!isSmallScreen ? (
                        <TableContainer component={Paper}>
                            <Table sx={{ minWidth: 650 }} aria-label="favourites table">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Car Listing</TableCell>
                                        <TableCell align="right">Price</TableCell>
                                        <TableCell align="right">Actions</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {favouriteListings.map((car) => (
                                        <TableRow key={car.id}>
                                            <TableCell>{car.name}</TableCell>
                                            <TableCell align="right">${car.price}</TableCell>
                                            <TableCell align="right">
                                                <Box sx={{ display: 'flex', justifyContent: 'flex-end', gap: '0.5em' }}>
                                                    <Button
                                                        variant="contained"
                                                        color="primary"
                                                        size="small"
                                                        onClick={() => handleDetailsClick(car.id)}
                                                    >
                                                        Details
                                                    </Button>
                                                    <Button
                                                        variant="contained"
                                                        color="error"
                                                        size="small"
                                                        onClick={() => handleRemoveClick(car.id)}
                                                    >
                                                        Remove
                                                    </Button>
                                                </Box>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    ) : (
                        // Card View for Small Screens
                        favouriteListings.map((car) => (
                            <Card key={car.id} sx={{ mb: 2, boxShadow: 2 }}>
                                <CardContent>
                                    <Typography variant="h6" fontWeight="bold">
                                        {car.name}
                                    </Typography>
                                    <Typography variant="body2" sx={{ mb: 1 }}>
                                        Price: ${car.price}
                                    </Typography>
                                    <Box
                                        sx={{
                                            display: 'flex',
                                            flexDirection: 'column',
                                            gap: 1,
                                        }}
                                    >
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            size="small"
                                            onClick={() => handleDetailsClick(car.id)}
                                        >
                                            Details
                                        </Button>
                                        <Button
                                            variant="contained"
                                            color="error"
                                            size="small"
                                            onClick={() => handleRemoveClick(car.id)}
                                        >
                                            Remove
                                        </Button>
                                    </Box>
                                </CardContent>
                            </Card>
                        ))
                    )}

                    {/* Pagination */}
                    <Box sx={{ display: 'flex', justifyContent: 'center', mt: 3 }}>
                        <Pagination
                            count={totalPages}
                            page={page}
                            onChange={handlePageChange}
                            color="primary"
                            size="large"
                        />
                    </Box>
                </>
            )}
        </Box>
    );
};

export default FavouriteCarListingList;