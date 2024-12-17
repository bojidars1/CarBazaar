import React, { useEffect, useState } from 'react';
import api from '../../api/api';
import { Box, CircularProgress, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Button, Pagination } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const FavouriteCarListingList = () => {
    const navigate = useNavigate();

    const [favouriteListings, setFavouriteListings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    const pageSize = 1;

    const fetchFavourites = async (page) => {
        const params = {
            page,
            pageSize
        };

        try {
            const response = await api.get('/FavouriteCarListing/get-favourites', { params });
            setFavouriteListings(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (err) {
            setError('Failed to get car listing favourites. Please try again');
        } finally {
            setLoading(false);
        }
    };

    const handlePageChange = (e, value) => {
        setPage(value);
    };

    const handleDetailsClick = (id) => {
        navigate(`/carlisting/details/${id}`);
    };

    const handleRemoveClick = async (id) => {
        try {
            await api.delete(`/FavouriteCarListing/${id}`);
            if (page === totalPages && page > 1) {
                setPage(page - 1);
            }
            await fetchFavourites(page);
        } catch (err) {
            console.log(err);
            setError('Failed to remove from favourites. Try again');
            setTimeout(() => {
                navigate('/favourites');
                window.location.reload();
            }, 1000);
        }
    };

    useEffect(() => {
        fetchFavourites(page);
    }, [page]);

    return (
        <Box sx={{ p: 2 }}>
            <Typography variant='h4' sx={{ textAlign: 'center', mb: 3}}>
                Favourites
            </Typography>

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5}}>
                    <CircularProgress />
                </Box>
            ) : error ? (
                <Typography color='error' variant='h6' sx={{ textAlign: 'center' }}>
                    {error}
                </Typography>
            ) : (
                <>
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label='simple table'>
                        <TableHead>
                            <TableRow>
                                <TableCell>Car Listing</TableCell>
                                <TableCell align='right'>Price</TableCell>
                                <TableCell align='right'>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {favouriteListings.map((car) => (
                                <TableRow key={car.id}>
                                    <TableCell>
                                        {car.name}
                                    </TableCell>
                                    <TableCell align='right'>
                                        {car.price}
                                    </TableCell>
                                    <TableCell align='right'>
                                        <Box sx={{ display: 'flex', justifyContent: 'flex-end', gap: '0.5em'}}>
                                          <Button 
                                          variant='contained' 
                                          color='primary' 
                                          size='small' 
                                          onClick={() => handleDetailsClick(car.id)}
                                          >
                                            Details
                                          </Button>
                                          <Button 
                                          variant='contained' 
                                          color='error' 
                                          size='small'
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

                <Box sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    mt: 3
                }}>
                    <Pagination count={totalPages} page={page} onChange={handlePageChange} color='primary' size='large' />
                </Box>
                </>
            )}
        </Box>
    );
};

export default FavouriteCarListingList;