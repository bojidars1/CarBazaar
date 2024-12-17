import { Box, Container, Typography, Grid } from '@mui/material';
import React from 'react';

const WhyUs = () => {
    const whyChooseUs = [
        {
          title: "Wide Selection",
          description: "Choose from hundreds of cars across all categories.",
        },
        {
          title: "Trusted Sellers",
          description: "All sellers are verified to ensure your peace of mind.",
        },
        {
          title: "Easy Process",
          description: "Simple and user-friendly car buying process.",
        },
        {
          title: "Guranteed Clients",
          description: "We will connect you to a world-wide range of car enthuasists.",
        },
      ];

    return (
      <Box sx={{ py: 5 }}>
      <Container>
        <Typography
          variant="h4"
          sx={{
            textAlign: "center",
            mb: 3,
            fontWeight: "bold",
            color: "secondary.main",
          }}
        >
          Why Choose CarBazaar?
        </Typography>
        <Grid container spacing={4} justifyContent="center">
          {whyChooseUs.map((reason, index) => (
            <Grid item xs={12} sm={6} md={3} key={index}>
              <Box
                sx={{
                  textAlign: "center",
                  p: 3,
                  borderRadius: 2,
                  boxShadow: 2,
                  backgroundColor: "#ffffff",
                  transition: "transform 0.3s",
                  "&:hover": { transform: "scale(1.05)" },
                }}
              >
                <Typography variant="h6" sx={{ mb: 1, fontWeight: "bold" }}>
                  {reason.title}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {reason.description}
                </Typography>
              </Box>
            </Grid>
          ))}
        </Grid>
      </Container>
    </Box>
    );
};

export default WhyUs;