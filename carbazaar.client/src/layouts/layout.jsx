import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Container, IconButton, Drawer, List, ListItem, ListItemText } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../redux/authSlice';
import { clearUser } from '../redux/userSlice';
import api from '../api/api';

const Layout = ({ children }) => {
    const user = useSelector((state) => state.user.user);
    const token = useSelector((state) => state.auth.token);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [drawerOpen, setDrawerOpen] = useState(false);

    const toggleDrawer = (open) => (event) => {
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
            return;
        }
        setDrawerOpen(open);
    };

    const handleLogout = async () => {
        try {
            await api.post('/account/logout');
            localStorage.removeItem('token');
            dispatch(logout());
            dispatch(clearUser());
            navigate('/');
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            <AppBar position="sticky" sx={{ margin: '0.5em auto', width: '97%' }}>
                <Toolbar sx={{ justifyContent: 'space-between' }}>
                    {/* Logo and Name */}
                    <Typography variant="h6" component="div" sx={{ display: 'flex', alignItems: 'center' }}>
                        <img
                            src="/src/assets/logo.png"
                            alt="Logo"
                            style={{ width: '50px', height: '50px', marginRight: '10px' }}
                        />
                        CarBazaar
                        {/* User Greeting */}
                        {user && (
                            <Box sx={{ display: 'flex', alignItems: 'center', gap: '1em' }}>
                            <Typography variant="body1" sx={{ display: 'inline', color: 'white', ml: '1em' }}>
                            Hello, {user}
                            </Typography>
                            <Button color="inherit" variant='outlined' size='small' component={Link} to="/user-carlistings">My Car Listings</Button>
                            </Box>
                        )}
                    </Typography>

                    {/* Nav Buttons */}
                    <Box sx={{ display: { xs: 'none', md: 'block' } }}>
                        <Button color="inherit" component={Link} to="/">Home</Button>
                        <Button color="inherit" component={Link} to="/carlisting/list">Cars</Button>
                        <Button color="inherit" onClick={handleLogout}>About</Button>
                        {user ? (
                            <>
                            <Button color="inherit" onClick={handleLogout}>Logout</Button>
                            </>
                        ) : (
                            <>
                            <Button color="inherit" component={Link} to="/login">Login</Button>
                            <Button color="inherit" component={Link} to="/register">Register</Button>
                            </>
                        )}
                        
                    </Box>

                    {/* Mobile Menu Button */}
                    <IconButton
                        color="inherit"
                        edge="start"
                        sx={{ display: { xs: 'block', md: 'none' } }}
                        onClick={toggleDrawer(true)}
                    >
                        <MenuIcon />
                    </IconButton>
                </Toolbar>
            </AppBar>

            {/* Mobile Drawer */}
            <Drawer
                anchor="right"
                open={drawerOpen}
                onClose={toggleDrawer(false)}
                sx={{ display: { xs: 'block', md: 'none' } }}
            >
                <Box
                    sx={{ width: 250 }}
                    role="presentation"
                    onClick={toggleDrawer(false)}
                    onKeyDown={toggleDrawer(false)}
                >
                    <List>
                        <ListItem component={Link} to="/">
                            <ListItemText primary="Home" />
                        </ListItem>
                        <ListItem component={Link} to="/carlisting/list">
                            <ListItemText primary="Cars" />
                        </ListItem>
                        <ListItem component={Link} to="/">
                            <ListItemText primary="About" />
                        </ListItem>
                        <ListItem component={Link} to="/">
                            <ListItemText primary="Login" />
                        </ListItem>
                        <ListItem component={Link} to="/">
                            <ListItemText primary="Register" />
                        </ListItem>
                    </List>
                </Box>
            </Drawer>

            {/* Main Content */}
            <Container sx={{ mt: 2, flexGrow: 1 }}>
                {children}
            </Container>

            {/* Footer */}
            <Box
                component="footer"
                sx={{
                    textAlign: 'center',
                    padding: '10px 0',
                    backgroundColor: '#f1f1f1',
                    marginTop: 'auto',
                    width: '100%',
                }}
            >
                <Typography variant="body2" color="textSecondary">
                    &copy; 2024 CarBazaar - All rights reserved.
                </Typography>
            </Box>
        </Box>
    );
};

export default Layout;