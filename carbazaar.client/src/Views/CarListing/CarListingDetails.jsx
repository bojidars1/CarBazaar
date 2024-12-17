import {
    Box,
    Button,
    CardContent,
    CardMedia,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid2,
    Paper,
    Typography,
  } from "@mui/material";
  import api from "../../api/api";
  import React, { useEffect, useState } from "react";
  import { useNavigate, useParams } from "react-router-dom";
  import { Star, StarBorder } from "@mui/icons-material";
  import { useSelector } from "react-redux";
  
  const CarListingDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
  
    const user = useSelector((state) => state.user.user);
    const token = useSelector((state) => state.auth.token);
  
    const [carListing, setCarListing] = useState(null);
    const [contactOpen, setContactOpen] = useState(false);
    const [isFavourite, setIsFavourite] = useState(false);
    const [loading, setLoading] = useState(true);
  
    const handleContactOpen = () => setContactOpen(true);
    const handleContactClose = () => setContactOpen(false);
  
    const handleToggleFavourite = async () => {
      try {
        if (isFavourite) {
          await api.delete(`/FavouriteCarListing/${id}`);
        } else {
          await api.post(`/FavouriteCarListing/${id}`);
        }
        setIsFavourite(!isFavourite);
      } catch (err) {
        console.error(err);
        navigate("/error");
      }
    };
  
    const handleStartChat = () => {
      if (carListing && carListing.sellerId) {
        navigate(`/chat/${carListing.id}/${carListing.sellerId}`);
        handleContactClose();
      }
    };
  
    useEffect(() => {
      const fetchCarDetailsAsync = async () => {
        try {
          const response = await api.get(`/CarListing/${id}`);
          setCarListing(response.data);
        } catch (err) {
          console.error(err);
          navigate("/error");
        } finally {
          setLoading(false);
        }
      };
  
      const fetchCarFavouriteStatusAsync = async () => {
        try {
          const response = await api.get(`/FavouriteCarListing/get-favourites`);
          setIsFavourite(response.data.items.some((item) => item.id === id));
        } catch (err) {
          console.error(err);
          navigate("/error");
        }
      };
  
      fetchCarDetailsAsync();
      if (user && token) {
        fetchCarFavouriteStatusAsync();
      }
    }, [id, user, token]);
  
    if (loading) {
      return (
        <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
          <Typography variant="h6">Loading...</Typography>
        </Box>
      );
    }
  
    if (!carListing) {
      return (
        <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
          <Typography variant="h6">Car listing not found.</Typography>
        </Box>
      );
    }
  
    return (
      <Box
        sx={{
          maxWidth: "1200px",
          margin: "auto",
          mt: 5,
          p: 3,
          backgroundColor: "#ffffff",
          borderRadius: 3,
          boxShadow: 3,
        }}
      >
        {/* Main Card */}
        <Paper elevation={3} sx={{ overflow: "hidden", borderRadius: 3 }}>
          <Grid2 container spacing={0}>
            {/* Image Section */}
            <Grid2 item xs={12} md={6}>
              <CardMedia
                component="img"
                image={
                  carListing.imageURL ||
                  "https://www.ilusso.com/wp-content/uploads/3-3-1024x683.jpg"
                }
                alt={carListing.name}
                sx={{
                  width: "100%",
                  height: { xs: "250px", md: "400px" },
                  objectFit: "cover",
                  backgroundColor: "#f5f5f5",
                }}
              />
            </Grid2>
  
            {/* Details Section */}
            <Grid2 item xs={12} md={6}>
              <CardContent sx={{ p: { xs: 2, md: 4 } }}>
                <Typography variant="h4" gutterBottom>
                  {carListing.name}
                </Typography>
                <Typography variant="h6" color="text.secondary" gutterBottom>
                  {carListing.brand} - {carListing.type}
                </Typography>
  
                <Box sx={{ my: 2 }}>
                  <Typography variant="body1">
                    <strong>Price:</strong> ${carListing.price}
                  </Typography>
                  <Typography variant="body1">
                    <strong>Gearbox:</strong> {carListing.gearbox}
                  </Typography>
                  <Typography variant="body1">
                    <strong>State:</strong> {carListing.state}
                  </Typography>
                  <Typography variant="body1">
                    <strong>Kilometers:</strong> {carListing.km} KM
                  </Typography>
                  <Typography variant="body1">
                    <strong>Production Year:</strong> {carListing.productionYear}
                  </Typography>
                  <Typography variant="body1">
                    <strong>Horsepower:</strong> {carListing.horsepower} hp
                  </Typography>
                  <Typography variant="body1">
                    <strong>Color:</strong> {carListing.color}
                  </Typography>
                  <Typography variant="body1" sx={{ mt: 2 }}>
                    <strong>Extra Info:</strong>{" "}
                    {carListing.extraInfo || "No additional information available."}
                  </Typography>
                </Box>
  
                {/* Favourite Button */}
                {user && token && (
                  <Button
                    variant="contained"
                    color={isFavourite ? "primary" : "default"}
                    startIcon={isFavourite ? <Star /> : <StarBorder />}
                    onClick={handleToggleFavourite}
                    sx={{ mt: 2 }}
                  >
                    {isFavourite ? "Remove from Favourites" : "Add to Favourites"}
                  </Button>
                )}
              </CardContent>
            </Grid2>
          </Grid2>
        </Paper>
  
        {/* CTA Section */}
        <Box
          sx={{
            display: "flex",
            flexDirection: { xs: "column", sm: "row" },
            justifyContent: "space-between",
            alignItems: "center",
            mt: 4,
            gap: 2,
          }}
        >
          <Button variant="outlined" color="primary" onClick={() => navigate("/carlisting/list")}>
            Back To Listings
          </Button>
          {user && token && user.userId !== carListing.sellerId && (
            <Button variant="contained" color="secondary" onClick={handleContactOpen}>
              Contact Seller
            </Button>
          )}
        </Box>
  
        {/* Contact Dialog */}
        <Dialog open={contactOpen} onClose={handleContactClose}>
          <DialogTitle>Contact the Seller</DialogTitle>
          <DialogContent>
            <Typography>For inquiries about this car, you can:</Typography>
            <Button
              variant="outlined"
              color="primary"
              onClick={handleStartChat}
              sx={{ mt: 2 }}
            >
              Start Chat
            </Button>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleContactClose}>Close</Button>
          </DialogActions>
        </Dialog>
      </Box>
    );
};
  
export default CarListingDetails;  