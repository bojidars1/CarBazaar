import { Container, Box, Typography, TextField, Button } from '@mui/material';
import api from '../api/api';
import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { setUser } from '../redux/userSlice';
import { setAuthenticated } from '../redux/authSlice';
import { jwtDecode } from 'jwt-decode';

const Login = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await api.post('/account/login', { email, password });
            const { accessToken } = response.data;

            const decodedToken = jwtDecode(accessToken);
            const userEmail = decodedToken.email;
            const carListings = decodedToken.CarListings;
            const user = {
              userEmail: userEmail,
              carListings: carListings
            };

            localStorage.setItem('token', accessToken);
            
            dispatch(setAuthenticated(accessToken));
            dispatch(setUser(user));
            navigate('/');
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <Container maxWidth='xs'>
            <Box sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                marginTop: 8,
                padding: 3,
                borderRadius: 2,
                boxShadow: 2,
                bgcolor: 'background.paper'
            }}>
                <Typography variant='h4' sx={{ mb: 2 }}>
                    Sign In
                </Typography>
                <form onSubmit={handleLogin} style={{ width: '100%' }}>
                    <TextField
                    label='Email'
                    type='email'
                    fullWidth
                    required
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    sx={{ mb: 2 }}
                    />

                    <TextField
                    label='Password'
                    type='password'
                    fullWidth
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    sx={{ mb: 2 }}
                    />

                    <Button type='submit' variant='contained' color='primary' fullWidth>
                        Sign In
                    </Button>
                </form>
                <Typography variant='body2' sx={{ mt: 2 }}>
                    Don't have an account?
                    <Button variant='text' color='primary'>
                        Register
                    </Button>
                </Typography>
            </Box>
        </Container>
    );
};

export default Login;