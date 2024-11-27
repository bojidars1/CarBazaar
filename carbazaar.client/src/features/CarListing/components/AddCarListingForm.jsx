import React, { useState } from 'react';
import axios from 'axios';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';

const CarListingForm = () => {
    const [formData, setFormData] = useState({
        name: '',
        type: '',
        brand: '',
        price: '',
        gearbox: '',
        state: '',
        km: '',
        productionYear: '',
        horsepower: '',
        color: '',
        extraInfo: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const response = await axios.post('https://localhost:7100/api/CarListing', formData);
        console.log(response);
    };

    return (
        <Box component="form" onSubmit={handleSubmit} 
        sx={{ maxWidth: 500, margin: 'auto', display: 'flex', flexDirection: 'column', gap: 2, mb: '1em' }}>
            <Typography variant="h5" sx={{ textAlign: 'center', mb: 2 }}>
                Add Car Listing
            </Typography>

            <TextField
                label="Name"
                name="name"
                value={formData.name}
                onChange={handleChange}
                fullWidth
                required
            />

            <TextField
                label="Car Type"
                name="type"
                value={formData.type}
                onChange={handleChange}
                fullWidth
                select
                required
            >
                <MenuItem value='SUV'>SUV</MenuItem>
                <MenuItem value='Sedan'>Sedan</MenuItem>
                <MenuItem value='Truck'>Truck</MenuItem>
                <MenuItem value='Convertible'>Convertible</MenuItem>
            </TextField>

            <TextField
                label="Car Brand"
                name="brand"
                value={formData.brand}
                onChange={handleChange}
                fullWidth
                select
                required
            >
                <MenuItem value='BMW'>BMW</MenuItem>
                <MenuItem value='Mercedes'>Mercedes</MenuItem>
                <MenuItem value='Audi'>Audi</MenuItem>
            </TextField>

            <TextField
                label="Price ($)"
                name="price"
                value={formData.price}
                onChange={handleChange}
                type="number"
                fullWidth
                required
            />

            <TextField
                label="Gearbox"
                name="gearbox"
                value={formData.gearbox}
                onChange={handleChange}
                fullWidth
                select
                required
            >
                <MenuItem value="Manual">Manual</MenuItem>
                <MenuItem value="Automatic">Automatic</MenuItem>
            </TextField>

            <TextField
                label="State"
                name="state"
                value={formData.state}
                onChange={handleChange}
                fullWidth
                required
            />

            <TextField
                label="Kilometers (KM)"
                name="km"
                value={formData.km}
                onChange={handleChange}
                type="number"
                fullWidth
                required
            />

            <TextField
                label="Production Year"
                name="productionYear"
                value={formData.productionYear}
                onChange={handleChange}
                type="number"
                fullWidth
                required
            />

            <TextField
                label="Horsepower"
                name="horsepower"
                value={formData.horsepower}
                onChange={handleChange}
                type="number"
                fullWidth
                required
            />

            <TextField
                label="Color"
                name="color"
                value={formData.color}
                onChange={handleChange}
                fullWidth
                required
            />

            <TextField
                label="Extra Information"
                name="extraInfo"
                value={formData.extraInfo}
                onChange={handleChange}
                multiline
                rows={4}
                fullWidth
            />

            <Button type="submit" variant="contained" color="primary">
                Submit
            </Button>
        </Box>
    );
};

export default CarListingForm;