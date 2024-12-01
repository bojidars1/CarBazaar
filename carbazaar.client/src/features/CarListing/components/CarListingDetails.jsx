import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const CarListingDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [carListing, setCarListing] = useState(null);
    const [contactOpen, setContactOpen] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchCarDetailsAsync = async () => {
            try {
                const respone = await axios.get(`https://localhost:7100/api/CarListing/${id}`);
            }
        };
    }, []);
};

export default CarListingDetails;