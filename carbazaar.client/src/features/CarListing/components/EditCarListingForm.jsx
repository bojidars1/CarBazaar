import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';
import { useNavigate } from "react-router-dom";

const CarListingForm = () => {
    const [formData, setFormData] = useState({
        id: '',
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
        extraInfo: '',
        imageURL: ''
    });

    const [loading, setLoading] = useState(true);
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

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
            await axios.put('https://localhost:7100/api/CarListing', formData);
            navigate(`/carlisting/details/${id}`);
        } catch (error) {
            if (error.response && error.response.status === 400) {
                setErrors(error.response.data.errors);
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    };

    useEffect(() => {
        const fetchCarListingAsync = async () => {
            try {
                const respone = await axios.get(`https://localhost:7100/api/CarListing/${id}`);
                setFormData(respone.data);
            } catch (err) {
                setErrors('Failed to fetch car listing.');
                console.error(err);
            } finally {
                setLoading(false);
            }
        };

        fetchCarListingAsync();
    }, [formData.id]);

    if (loading) {
        return (
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh'
            }}>
                <Typography variant='h6'>Loading...</Typography>
            </Box>
        );
    }

    if (!formData) {
        return (
            <Box sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh'
            }}>
                <Typography variant='h6'>{'Car listing not found.'}</Typography>
            </Box>
        );
    }

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
                required
                fullWidth
            />

            <TextField
                label="Car Type"
                name="type"
                value={formData.type}
                onChange={handleChange}
                error={!!errors.Type}
                helperText={errors.Type ? errors.Type[0] : ""}
                required
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
                required
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
                required
                fullWidth
            />

            <TextField
                label="Gearbox"
                name="gearbox"
                value={formData.gearbox}
                onChange={handleChange}
                error={!!errors.Gearbox}
                helperText={errors.Gearbox ? errors.Gearbox[0] : ""}
                required
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
                required
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
                required
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
                required
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
                required
                fullWidth
            />

            <TextField
                label="Color"
                name="color"
                value={formData.color}
                onChange={handleChange}
                error={!!errors.Color}
                helperText={errors.Color ? errors.Color[0] : ""}
                required
                fullWidth
            />

            <TextField
                label="Extra Information"
                name="extraInfo"
                value={formData.extraInfo}
                onChange={handleChange}
                error={!!errors.extraInfo}
                helperText={errors.ExtraInfo ? errors.ExtraInfo[0] : ""}
                multiline
                rows={4}
                fullWidth
            />

            <TextField
                label="Image URL"
                name="imageURL"
                value={formData.imageURL}
                onChange={handleChange}
                error={!!errors.imageURL}
                helperText={errors.ImageURL ? errors.ImageURL[0] : ""}
                fullWidth
            />

            <Button type="submit" variant="contained" color="primary">
                Submit
            </Button>
        </Box>
    );
};

export default CarListingForm;