import { Container, Box, Typography, TextField, Button } from '@mui/material';
import React, { useState } from 'react';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = (e) => {
        e.preventDefault();
        console.log(`Successful login: ${email} .. ${password}`);
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