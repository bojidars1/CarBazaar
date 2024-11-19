import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Container } from '@mui/material';

const Layout = ({ children }) => {
    return (
        <div>
            {/* Header Section with Logo and Navigation */}
           <AppBar position='sticky'>
            <Toolbar>
                {/* Logo and App Name */}
                <Typography variant='h6' component='div' sx={{ 
                    flexGrow: 1,
                    display: 'flex',
                    alignItems: 'center',
                    }}>
                    <img src='/src/assets/logo.png' style={{ width: '60px', height: '60px', marginRight: '10px' }}></img>
                    CarBazaar
                </Typography>

                {/* Navigation Buttons */}
                <Button color='inherit'>Home</Button>
                <Button color='inherit'>Play</Button>
                <Button color='inherit'>About</Button>
                <Button color='inherit'>Login</Button>
                <Button color='inherit'>Register</Button>
            </Toolbar>
           </AppBar>

           {/* Main Content */}
           <Container maxWidth='lg' 
           sx={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            mt: 2
           }}>
            {children}
           </Container>

           {/* Footer*/}
           <Box position='fixed' component='footer'
           sx=
           {{ textAlign: 'center', padding: '10px 0', backgroundColor: '#f1f1f1', marginTop: '20px', bottom: 5, width: '100%' }}>
           <Typography variant="body2" color="textSecondary">
             &copy; 2024 CarBazaar - All rights reserved.
           </Typography>
           </Box>
        </div>
    );
};

export default Layout;