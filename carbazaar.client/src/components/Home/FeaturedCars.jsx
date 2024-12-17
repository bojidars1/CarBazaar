import { Box, Typography, Container, Grid, Card, CardMedia, CardContent, Button } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';

const FeaturedCars = () => {
    // not actual listings
    const featuredCars = [
        {
          name: "BMW X5",
          description: "Luxury SUV with premium features.",
          image:
            "https://hips.hearstapps.com/hmg-prod/images/2024-bmw-x5-xdrive-50e-796-659d5427d6862.jpg?crop=0.627xw:0.528xh;0.103xw,0.400xh&resize=1200:*",
        },
        {
          name: "Mercedes-Benz C-Class",
          description: "Sleek sedan with outstanding performance.",
          image:
            "https://m.netinfo.bg/media/images/50258/50258374/1180-663-mercedes-amg-c63-s-e-f1-edition.jpg",
        },
        {
          name: "Audi Q7",
          description: "Spacious and powerful SUV for families.",
          image:
            "https://static.dir.bg/uploads/images/2024/01/31/2641271/1366x768.jpg?_=1706710986",
        },
      ];

    return (
        <Box sx={{ py: 5, backgroundColor: "#f9f9f9" }}>
       <Container>
        <Typography
          variant="h4"
          sx={{
            textAlign: "center",
            mb: 3,
            fontWeight: "bold",
            color: "primary.main",
          }}
        >
          Featured Cars
        </Typography>
        <Grid container spacing={3} justifyContent="center">
          {/* Car Cards */}
          {featuredCars.map((car, index) => (
            <Grid item xs={12} sm={6} md={4} key={index}>
              <Card sx={{ boxShadow: 3, borderRadius: 2 }}>
                <CardMedia
                  component="img"
                  height="200"
                  image={car.image}
                  alt={car.name}
                />
                <CardContent>
                  <Typography variant="h6" fontWeight="bold">
                    {car.name}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {car.description}
                  </Typography>
                </CardContent>
                <Box sx={{ p: 2, textAlign: "center" }}>
                  <Button
                    component={Link}
                    to="/carlisting/list"
                    variant="contained"
                    color="primary"
                  >
                    View Listings
                  </Button>
                </Box>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </Box>
    );
};

export default FeaturedCars;