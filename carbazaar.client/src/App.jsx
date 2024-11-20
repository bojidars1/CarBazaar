import React from 'react';
import { CssBaseline, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';
import Home from './pages/Home';

const App = () => {
    return(
      <ThemeProvider theme={lightTheme}>
        <CssBaseline />
        <Layout>
          <Home />
        </Layout>
      </ThemeProvider>
    );
};

export default App;