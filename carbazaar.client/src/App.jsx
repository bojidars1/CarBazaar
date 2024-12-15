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
import FavouriteCarListingList from './pages/FavouriteCarListing/FavouriteCarListingList';
import ChatList from './pages/Chat/ChatList';
import ChatMessages from './pages/Chat/ChatMessages';
import AdminDashboard from './pages/Admin/AdminDashboard';
import UsersDashboard from './pages/Admin/UsersDashboard';
import CarListingsDashboard from './pages/Admin/CarListingsDashboard';

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
              <Route path='/user-carlistings' element={<UserCarListingList /> } />
              <Route path='/favourites' element={<FavouriteCarListingList /> } />
              <Route path='/chats' element={<ChatList /> } />
              <Route path='/chat/:carListingId/:participantId' element={<ChatMessages /> } />
              <Route path='/login' element={<Login />} />
              <Route path='/register' element={<Register />} />

              <Route path='/admin' element={<AdminDashboard /> }>
                <Route path='users' element={<UsersDashboard /> } />
                <Route path='car-listings' element={<CarListingsDashboard />} />
              </Route>
            </Routes>
          </Layout>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;