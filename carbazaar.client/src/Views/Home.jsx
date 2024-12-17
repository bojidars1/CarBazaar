import React from 'react';
import HeroSection from '../components/Home/HeroSection';
import FeaturedCars from '../components/Home/FeaturedCars';
import WhyUs from '../components/Home/WhyUs';

const HomePage = () => {
    return (
        <>
        <HeroSection />
        <FeaturedCars />
        <WhyUs />
        </>
    );
};

export default HomePage;