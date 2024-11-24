import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom' 
import { CssBaseline, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';
import Home from './pages/Home';
import AddCarListing from './pages/AddCarListing';

const App = () => {
    return(
      <ThemeProvider theme={lightTheme}>
        <CssBaseline />
        <Router>
          <Layout>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/carlisting/add" element={<AddCarListing />} />
            </Routes>
          </Layout>
        </Router>
        
      </ThemeProvider>
    );
};

export default App;