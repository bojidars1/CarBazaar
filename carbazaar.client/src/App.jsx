import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom' 
import { CssBaseline, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';
import Home from './Views/Home';
import AddCarListing from './Views/CarListing/AddCarListing';
import CarListings from './Views/CarListing/CarListingsList';
import CarListingDetails from './Views/CarListing/CarListingDetails';
import EditCarListingForm from './Views/CarListing/EditCarListing';
import DeleteCarListing from './Views/CarListing/DeleteCarListing';
import Login from './Views/Login';
import Register from './Views/Register';
import useAuth from './services/authService';
import UserCarListingList from './Views/UserCarListing/UserCarListingList';
import FavouriteCarListingList from './Views/FavouriteCarListing/FavouriteCarListingList';
import ChatList from './Views/Chat/ChatList';
import ChatMessages from './Views/Chat/ChatMessages';
import AdminDashboard from './Views/Admin/AdminDashboard';
import UsersDashboard from './Views/Admin/UsersDashboard';
import CarListingsDashboard from './Views/Admin/CarListingsDashboard';
import NotificationsList from './Views/Notification/NotificationsList';
import InternalError from './Views/Error/InternalError';
import NotFound from './Views/Error/NotFound';
import ErrorBoundary from './components/Error/ErrorBoundary.jsx';

const App = () => {
   const { checkAccessToken } = useAuth();

   useEffect(() => {
    checkAccessToken();
   }, [checkAccessToken]);

    return(
      <ThemeProvider theme={lightTheme}>
        <CssBaseline />
        <Router>
          <ErrorBoundary>
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

              <Route path='/notifications' element={<NotificationsList /> } />

              <Route path='/error' element={<InternalError />} />
              <Route path='*' element={<NotFound />} />

              <Route path='/admin' element={<AdminDashboard /> }>
                <Route path='users' element={<UsersDashboard /> } />
                <Route path='car-listings' element={<CarListingsDashboard />} />
              </Route>
            </Routes>
          </Layout>
          </ErrorBoundary>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;