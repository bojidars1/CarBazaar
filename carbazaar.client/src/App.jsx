import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom' 
import { CssBaseline, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';
import Home from './pages/Home';
import AddCarListing from './pages/AddCarListing';
import CarListings from './pages/CarListings';
import CarListingDetails from './features/CarListing/components/CarListingDetails';
import EditCarListingForm from './features/CarListing/components/EditCarListingForm';
import DeleteCarListing from './features/CarListing/components/DeleteCarListing';
import Login from './pages/Login';
import Register from './pages/Register';
import useAuth from './services/authService';
import UserCarListingList from './pages/UserCarListing/UserCarListingList';

const App = () => {
   const { checkAccessToken } = useAuth();

   useEffect(() => {
    checkAccessToken();
   }, [checkAccessToken]);

    return(
      <ThemeProvider theme={lightTheme}>
        <CssBaseline />
        <Router>
          <Layout>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/carlisting/add" element={<AddCarListing />} />
              <Route path="/carlisting/list" element={<CarListings />} />
              <Route path="/carlisting/details/:id" element={<CarListingDetails /> } />
              <Route path="/carlisting/edit/:id" element={<EditCarListingForm /> } />
              <Route path='/carlisting/delete/:id' element={<DeleteCarListing /> } />
              <Route path='/usercarlistings' element={<UserCarListingList />} />
              <Route path='/login' element={<Login />} />
              <Route path='/register' element={<Register />} />
            </Routes>
          </Layout>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;