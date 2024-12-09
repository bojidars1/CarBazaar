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
import Login from './pages/Login';
import Register from './pages/Register';
import { setAuthenticated } from './redux/authSlice';
import { setUser } from './redux/userSlice';
import { useDispatch } from 'react-redux';
import { jwtDecode } from 'jwt-decode';

const App = () => {
   const dispatch = useDispatch();
   const token = localStorage.getItem('token');
   if (savedToken) {
    const decodedToken = jwtDecode(token);
    const userEmail = decodedToken.email;

    dispatch(setAuthenticated(token));
    dispatch(setUser(userEmail));
   }

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
              <Route path='/login' element={<Login />} />
              <Route path='/register' element={<Register />} />
            </Routes>
          </Layout>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;