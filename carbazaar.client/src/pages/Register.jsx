import { Container, Box, Typography, TextField, Button } from "@mui/material";
import React, { useState } from "react";

const Register = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleRegister = (e) => {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert('Passwords do not match!');
            return;
        }
        console.log(`Registered: ${email}, ${password}`);
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