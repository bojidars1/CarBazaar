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

    const [errors, setErrors] = useState({});

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
        setErrors((prev) => {
            const capitalFirstLetterName = name.charAt(0).toUpperCase() + name.slice(1);
            const updatedErros = { ...prev };
            delete updatedErros[capitalFirstLetterName];
            return updatedErros;
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrors({});

        try {
            const response = await axios.post('https://localhost:7100/api/CarListing', formData);
        } catch (error) {
            if (error.response && error.response.status === 400) {
                setErrors(error.response.data.errors);
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    };

    return (
        <Box component="form" onSubmit={handleSubmit} 
        sx={{ 
         maxWidth: 500,
         margin: 'auto',
         display: 'flex',
         flexDirection: 'column',
         gap: 2,
         mb: '1em' }}>
            <Typography variant="h5" sx={{ textAlign: 'center', mb: 2 }}>
                Add Car Listing
            </Typography>

            <TextField
                label="Name"
                name="name"
                value={formData.name}
                onChange={handleChange}
                error={!!errors.Name}
                helperText={errors.Name ? errors.Name[0] : ""}
                fullWidth
            />

            <TextField
                label="Car Type"
                name="type"
                value={formData.type}
                onChange={handleChange}
                error={!!errors.Type}
                helperText={errors.Type ? errors.Type[0] : ""}
                fullWidth
                select
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
                error={!!errors.Brand}
                helperText={errors.Brand ? errors.Brand[0] : ""}
                fullWidth
                select
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
                error={!!errors.Price}
                helperText={errors.Price ? errors.Price[0] : ""}
                fullWidth
            />

            <TextField
                label="Gearbox"
                name="gearbox"
                value={formData.gearbox}
                onChange={handleChange}
                error={!!errors.Gearbox}
                helperText={errors.Gearbox ? errors.Gearbox[0] : ""}
                fullWidth
                select
            >
                <MenuItem value="Manual">Manual</MenuItem>
                <MenuItem value="Automatic">Automatic</MenuItem>
            </TextField>

            <TextField
                label="State"
                name="state"
                value={formData.state}
                onChange={handleChange}
                error={!!errors.State}
                helperText={errors.State ? errors.State[0] : ""}
                fullWidth
            />

            <TextField
                label="Kilometers (KM)"
                name="km"
                value={formData.km}
                onChange={handleChange}
                type="number"
                error={!!errors.KM}
                helperText={errors.KM ? errors.KM[0] : ""}
                fullWidth
            />

            <TextField
                label="Production Year"
                name="productionYear"
                value={formData.productionYear}
                onChange={handleChange}
                type="number"
                error={!!errors.ProductionYear}
                helperText={errors.ProductionYear ? errors.ProductionYear[0] : ""}
                fullWidth
            />

            <TextField
                label="Horsepower"
                name="horsepower"
                value={formData.horsepower}
                onChange={handleChange}
                type="number"
                error={!!errors.Horsepower}
                helperText={errors.Horsepower ? errors.Horsepower[0] : ""}
                fullWidth
            />

            <TextField
                label="Color"
                name="color"
                value={formData.color}
                onChange={handleChange}
                error={!!errors.Color}
                helperText={errors.Color ? errors.Color[0] : ""}
                fullWidth
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