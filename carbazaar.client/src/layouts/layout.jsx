import React, { useState } from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
  Container,
  IconButton,
  Drawer,
  List,
  ListItem,
  ListItemText,
  Divider,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../redux/authSlice';
import { clearUser } from '../redux/userSlice';
import NotificationDropdown from '../components/Notification/NotificationDropdown';

const Layout = ({ children }) => {
  const user = useSelector((state) => state.user.user);
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
      localStorage.removeItem('token');
      dispatch(logout());
      dispatch(clearUser());
      navigate('/');
    } catch (err) {
      console.log(err);
      navigate('/error');
    }
  };

  return (
    <Box display="flex" flexDirection="column" minHeight="100vh">
      {/* Header */}
      <AppBar position="sticky" sx={{ bgcolor: 'primary.main', boxShadow: 2 }}>
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          {/* Logo and Title */}
          <Box
            sx={{ display: 'flex', alignItems: 'center', cursor: 'pointer' }}
            onClick={() => navigate('/')}
          >
            <img
              src="/src/assets/logo.png"
              alt="Logo"
              style={{ width: '50px', height: '50px', marginRight: '10px' }}
            />
            <Typography
              variant="h6"
              color="inherit"
              sx={{ fontWeight: 'bold', textDecoration: 'none' }}
              component={Link}
              to="/"
            >
              CarBazaar
            </Typography>
          </Box>

          {/* Desktop Navigation */}
          <Box sx={{ display: { xs: 'none', md: 'flex' }, alignItems: 'center', gap: 2 }}>
            <Button color="inherit" component={Link} to="/">
              Home
            </Button>
            <Button color="inherit" component={Link} to="/carlisting/list">
              Cars
            </Button>

            {user ? (
              <>
                <NotificationDropdown />
                <Button color="inherit" variant='outlined' component={Link} to="/user-carlistings">
                  My Car Listings
                </Button>
                <Button color="inherit" variant='outlined' component={Link} to="/favourites">
                  Favourites
                </Button>
                <Button color="inherit" variant='outlined' component={Link} to="/chats">
                  Chats
                </Button>
                <Button color="inherit" onClick={handleLogout}>
                  Logout
                </Button>
              </>
            ) : (
              <>
                <Button color="inherit" component={Link} to="/login">
                  Login
                </Button>
                <Button color="inherit" component={Link} to="/register">
                  Register
                </Button>
              </>
            )}
          </Box>

          {/* Mobile Navigation */}
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
      <Drawer anchor="right" open={drawerOpen} onClose={toggleDrawer(false)}>
        <Box
          sx={{ width: 250, display: 'flex', flexDirection: 'column', height: '100%' }}
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
            <Divider />
            {user ? (
              <>
                <ListItem component={Link} to="/user-carlistings">
                  <ListItemText primary="My Car Listings" />
                </ListItem>
                <ListItem component={Link} to="/favourites">
                  <ListItemText primary="Favourites" />
                </ListItem>
                <ListItem component={Link} to="/chats">
                  <ListItemText primary="Chats" />
                </ListItem>
                <ListItem component={"button"} onClick={handleLogout}>
                  <ListItemText primary="Logout" />
                </ListItem>
              </>
            ) : (
              <>
                <ListItem component={Link} to="/login">
                  <ListItemText primary="Login" />
                </ListItem>
                <ListItem component={Link} to="/register">
                  <ListItemText primary="Register" />
                </ListItem>
              </>
            )}
          </List>
        </Box>
      </Drawer>

      {/* Main Content */}
      <Container sx={{ flex: 1, py: 3 }}>{children}</Container>

      {/* Footer */}
      <Box
        component="footer"
        sx={{
          textAlign: 'center',
          py: 2,
          backgroundColor: '#f5f5f5',
          mt: 'auto',
          boxShadow: '0 -2px 4px rgba(0, 0, 0, 0.1)',
        }}
      >
        <Typography variant="body2" color="textSecondary">
          &copy; {new Date().getFullYear()} CarBazaar - All rights reserved.
        </Typography>
      </Box>
    </Box>
  );
};

export default Layout;