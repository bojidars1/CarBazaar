import { Container, Box, Typography, TextField, Button, Alert } from '@mui/material';
import api from '../api/api';
import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useLocation, useNavigate } from 'react-router-dom';
import { setUser } from '../redux/userSlice';
import { setAuthenticated } from '../redux/authSlice';
import { jwtDecode } from 'jwt-decode';

const Login = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const location = useLocation();

    const from = location.state?.from?.pathname || '/';

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
        setErrorMessage('');

        try {
            const response = await api.post('/account/login', { email, password });
            const { accessToken } = response.data;

            const decodedToken = jwtDecode(accessToken);
            const userId = decodedToken.sub;
            const userEmail = decodedToken.email;
            const carListings = decodedToken.CarListings;

            const user = {
                userId: userId,
                userEmail: userEmail,
                carListings: carListings
            };

            localStorage.setItem('token', accessToken);

            dispatch(setAuthenticated(true));
            dispatch(setUser(user));
            navigate(from, { replace: true });
        } catch (err) {
            console.error(err);

            if (err.response && err.response.data) {
                setErrorMessage(err.response.data.message || "Invalid email or password.");
            } else {
                setErrorMessage("Something went wrong. Please try again later.");
            }
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

                {errorMessage && (
                    <Alert severity="error" sx={{ mb: 2, width: '100%' }}>
                        {errorMessage}
                    </Alert>
                )}

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