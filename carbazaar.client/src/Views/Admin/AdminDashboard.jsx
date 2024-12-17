import React from "react";
import { Link, Outlet } from "react-router-dom";
import { Box, Typography, Button, Stack } from "@mui/material";

const AdminDashboard = () => {
  return (
    <Box p={4}>
      <Typography variant="h4" gutterBottom>
        Admin Dashboard
      </Typography>
      <Stack direction="row" spacing={2} mb={3}>
        <Button variant="contained" color="primary" component={Link} to="/admin/users">
          Manage Users
        </Button>
        <Button variant="contained" color="primary" component={Link} to="/admin/car-listings">
          Manage Car Listings
        </Button>
      </Stack>
      <Outlet />
    </Box>
  );
};

export default AdminDashboard;