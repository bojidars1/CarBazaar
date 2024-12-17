import React, { useState } from 'react';
import { Box, Typography, Button, InputLabel, Grid, FormControl, MenuItem, Select, Paper, Divider } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';

const HeroSection = () => {
    const navigate = useNavigate();

    const [carType, setCarType] = useState('All');
    const [carBrand, setCarBrand] = useState('All');
    const [priceRange, setPriceRange] = useState('All');

    const handleCarTypeChange = (e) => setCarType(e.target.value);
    const handleCarBrandChange = (e) => setCarBrand(e.target.value);
    const handlePriceRangeChange = (e) => setPriceRange(e.target.value);

    const handleSearch = () => {
        const params = { type: carType, brand: carBrand, priceRange };
        navigate('/carlisting/list', { state: { params: params } });
    };

    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                textAlign: 'center',
                p: 4,
                gap: 4,
            }}
        >
            {/* Title and Subtitle */}
            <Box>
                <Typography
                    variant="h2"
                    sx={{
                        fontWeight: 'bold',
                        color: 'primary.main',
                        fontSize: { xs: '2.5rem', md: '3.5rem' },
                        mb: 2,
                    }}
                >
                    Welcome to CarBazaar
                </Typography>
                <Typography
                    variant="h6"
                    sx={{
                        color: 'text.secondary',
                        fontSize: { xs: '1rem', md: '1.2rem' },
                    }}
                >
                    Find, Buy, or Sell Your Dream Car with Ease.
                </Typography>
            </Box>

            {/* Search Filters */}
            <Paper
                elevation={3}
                sx={{
                    width: '100%',
                    maxWidth: '900px',
                    p: 4,
                    borderRadius: 3,
                    boxShadow: 5,
                    backgroundColor: '#ffffff',
                }}
            >
                <Grid container spacing={2} alignItems="center" justifyContent="center">
                    {/* Car Type Filter */}
                    <Grid item xs={12} sm={6} md={3}>
                        <FormControl fullWidth variant="outlined">
                            <InputLabel>Car Type</InputLabel>
                            <Select value={carType} onChange={handleCarTypeChange} label="Car Type">
                                <MenuItem value="All">All</MenuItem>
                                <MenuItem value="SUV">SUV</MenuItem>
                                <MenuItem value="Sedan">Sedan</MenuItem>
                                <MenuItem value="Truck">Truck</MenuItem>
                                <MenuItem value="Convertible">Convertible</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Car Brand Filter */}
                    <Grid item xs={12} sm={6} md={3}>
                        <FormControl fullWidth variant="outlined">
                            <InputLabel>Car Brand</InputLabel>
                            <Select value={carBrand} onChange={handleCarBrandChange} label="Car Brand">
                                <MenuItem value="All">All</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Price Range Filter */}
                    <Grid item xs={12} sm={6} md={3}>
                        <FormControl fullWidth variant="outlined">
                            <InputLabel>Price Range</InputLabel>
                            <Select value={priceRange} onChange={handlePriceRangeChange} label="Price Range">
                                <MenuItem value="All">All</MenuItem>
                                <MenuItem value="0-10000">0 - 10,000</MenuItem>
                                <MenuItem value="10000-30000">10,000 - 30,000</MenuItem>
                                <MenuItem value="50000+">50,000+</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>

                    {/* Search Button */}
                    <Grid item xs={12} sm={6} md={3}>
                        <Button
                            variant="contained"
                            color="primary"
                            size="large"
                            onClick={handleSearch}
                            fullWidth
                            sx={{
                                borderRadius: 2,
                                height: '100%',
                                '&:hover': { backgroundColor: 'primary.dark' },
                            }}
                        >
                            Search
                        </Button>
                    </Grid>
                </Grid>
            </Paper>

            {/* Call-to-Action */}
            <Divider sx={{ width: '80%', my: 0.5 }} />
            <Button
                component={Link}
                to="/carlisting/add"
                variant="outlined"
                color="secondary"
                size="large"
                sx={{
                    textTransform: 'none',
                    fontWeight: 'bold',
                    borderRadius: 2
                }}
            >
                Sell Your Car Now
            </Button>
        </Box>
    );
};

export default HeroSection;