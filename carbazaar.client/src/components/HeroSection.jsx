import React from 'react';
import { Box, Typography, Button, TextField, Grid2 } from '@mui/material';

const HeroSection = () => {
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
                <Grid2 sx={{ display: 'flex', flexGrow: 0.3}}>
                    <TextField
                    fullWidth
                    placeholder='Search for cars...'
                    variant='outlined'
                    size='medium'
                    sx={{ backgroundColor: 'background.paper' }}
                    />
                </Grid2>
                <Grid2>
                    <Button
                    variant='contained'
                    color='primary'
                    size='large'
                    sx={{ height: '100%' }}
                    >
                        Search
                    </Button>
                </Grid2>
            </Grid2>

            {/* Call-to-Action */}
            <Button
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