import { Container, Box, Typography, TextField, Button, Alert } from "@mui/material";
import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from 'react-redux';
import { setUser } from '../redux/userSlice';
import { setAuthenticated } from '../redux/authSlice';
import { jwtDecode } from "jwt-decode";

const Register = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');

    const handleRegister = async (e) => {
        e.preventDefault();
        setError('');

        if (password !== confirmPassword) {
            setError('Passwords does not match!');
            return;
        }

        try {
            const response = await api.post('/account/register', { email, password });
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
            
            dispatch(setAuthenticated(accessToken));
            dispatch(setUser(user));
            navigate('/');
        } catch (err) {
            console.log(err);
        }
    }

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
                <Typography variant='h4' sx={{ mb: 4 }}>
                    Register
                </Typography>

                {error && <Alert severity='error' sx={{ mb: 2 }}>{error}</Alert>}

                <form onSubmit={handleRegister} style={{ width: '100%' }}>
                    <TextField
                    label='Email'
                    type='email'
                    value={email}
                    fullWidth
                    required
                    onChange={(e) => setEmail(e.target.value)}
                    sx={{ mb: 2 }}
                    />

                    <TextField
                    label='Password'
                    type='password'
                    value={password}
                    fullWidth
                    required
                    onChange={(e) => setPassword(e.target.value)}
                    sx={{ mb: 2 }}
                    />

                    <TextField
                    label='Confirm Password'
                    type='password'
                    value={confirmPassword}
                    fullWidth
                    required
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    sx={{ mb: 2 }}
                    />

                    <Button type='submit' variant='contained' color='primary' fullWidth>
                        Register
                    </Button>
                </form>
                <Typography variant='body2' sx={{ mt: 2 }}>
                    Already have an account?
                    <Button variant='text' color='primary'>
                        Sign In
                    </Button>
                </Typography>
            </Box>
        </Container>
    );
};

export default Register;