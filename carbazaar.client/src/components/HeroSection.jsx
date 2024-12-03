import React, { useState } from 'react';
import { Box, Typography, Button, InputLabel, Grid2, FormControl, MenuItem, Select } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

const HeroSection = () => {
    const navigate = useNavigate();

    const [carType, setCarType] = useState('All');
    const [carBrand, setCarBrand] = useState('All');
    const [priceRange, setPriceRange] = useState('All');

    const handleCarTypeChange = (e) => setCarType(e.target.value);
    const handleCarBrandChange = (e) => setCarBrand(e.target.value);
    const handlePriceRangeChange = (e) => setPriceRange(e.target.value);
    
    const handleSearch = () => {
        const params = {
            type: carType,
            brand: carBrand,
            priceRange,
        };

        navigate('/carlisting/list', { state: { params: params } });
    };

    return (
        <Box sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            textAlign: 'center',
            padding: '3em'
        }}>
            {/* Title */}
            <Typography variant='h2' sx={{ fontWeight: 'bold', color: 'primary.main', mb: 2 }}>
                Welcome to CarBazaar
            </Typography>

            {/* Subtitle */}
            <Typography variant='h6' sx={{ color: 'text.secondary', mb: 4 }}>
                Find, Buy, or Sell Your Dream Car with Ease.
            </Typography>

            {/* Search Bar */}
            <Grid2 container spacing={2} justifyContent='center' sx={{ mb: 4, width: '100%' }}>
                {/* Car Type Filter */}
                <Grid2 sx={{display: 'flex', flexGrow: 0.05}}>
                    <FormControl fullWidth>
                        <InputLabel>Car Type</InputLabel>
                        <Select value={carType} onChange={handleCarTypeChange} label='Car Type'>
                            <MenuItem value='All'>All</MenuItem>
                            <MenuItem value='SUV'>SUV</MenuItem>
                            <MenuItem value='Sedan'>Sedan</MenuItem>
                            <MenuItem value='Truck'>Truck</MenuItem>
                            <MenuItem value='Convertible'>Convertible</MenuItem>
                        </Select>
                    </FormControl>
                </Grid2>

                {/* Car Brand Filter */}
                <Grid2 sx={{display: 'flex', flexGrow: 0.05}}>
                    <FormControl fullWidth>
                        <InputLabel>Car Brand</InputLabel>
                        <Select value={carBrand} onChange={handleCarBrandChange} label='Car Brand'>
                            <MenuItem value='All'>All</MenuItem>
                            <MenuItem value='Mercedes'>Mercedes</MenuItem>
                            <MenuItem value='BMW'>BMW</MenuItem>
                            <MenuItem value='Audi'>Audi</MenuItem>
                        </Select>
                    </FormControl>
                </Grid2>

                {/* Price Range Filter */}
                <Grid2 sx={{display: 'flex', flexGrow: 0.05}}>
                    <FormControl fullWidth>
                        <InputLabel>Price Range</InputLabel>
                        <Select value={priceRange} onChange={handlePriceRangeChange} label='Price Range'>
                            <MenuItem value='All'>All</MenuItem>
                            <MenuItem value='0-10000'>0 - 10,000</MenuItem>
                            <MenuItem value='10000-30000'>10,000 - 30,000</MenuItem>
                            <MenuItem value='50000+'>50,000+</MenuItem>
                        </Select>
                    </FormControl>
                </Grid2>

                {/* Search Button */}
                <Grid2>
                    <Button variant='contained' color='primary' size='large' onClick={handleSearch} sx={{ height: '100%', borderRadius: 2 }}>
                        Search
                    </Button>
                </Grid2>
            </Grid2>

            {/* Call-to-Action */}
            <Button
            component={Link} to="/carlisting/add"
            variant='outlined'
            color='secondary'
            size='large'
            sx={{
                textTransform: 'none',
                fontWeight: 'bold'
            }}
            >
                Sell Your Car Now
            </Button>
        </Box>
    );
};

export default HeroSection;