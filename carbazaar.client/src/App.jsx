import React from 'react';
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

const App = () => {
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
            </Routes>
          </Layout>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;