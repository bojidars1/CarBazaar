import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Container } from '@mui/material';
import { Link } from 'react-router-dom';

const Layout = ({ children }) => {
    return (
        <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            {/* Header Section with Logo and Navigation */}
            <AppBar position='sticky' sx={{ margin: '0.5em auto', width: '97%' }}>
                <Toolbar>
                    {/* Logo and App Name */}
                    <Typography variant='h6' component='div' sx={{
                        flexGrow: 1,
                        display: 'flex',
                        alignItems: 'center',
                    }}>
                        <img src='/src/assets/logo.png' style={{ width: '60px', height: '60px', marginRight: '10px' }} />
                        CarBazaar
                    </Typography>

                    {/* Navigation Buttons */}
                    <Button color='inherit' component={Link} to="/">Home</Button>
                    <Button color='inherit'>Play</Button>
                    <Button color='inherit'>About</Button>
                    <Button color='inherit'>Login</Button>
                    <Button color='inherit'>Register</Button>
                </Toolbar>
            </AppBar>

            {/* Main Content */}
            <Container sx={{ mt: 2, flexGrow: 1 }}>
                {children}
            </Container>

            {/* Footer */}
            <Box component="footer" sx={{
                textAlign: 'center',
                padding: '10px 0',
                backgroundColor: '#f1f1f1',
                marginTop: 'auto',
                width: '100%',
            }}>
                <Typography variant="body2" color="textSecondary">
                    &copy; 2024 CarBazaar - All rights reserved.
                </Typography>
            </Box>
        </Box>
    );
};

export default Layout;