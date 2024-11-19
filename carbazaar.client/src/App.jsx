import React from 'react';
import { Button, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';

const App = () => {
    return(
      <ThemeProvider theme={lightTheme}>
        <Layout>
          <div>
            <h1>Welcome to CarBazaar</h1>
            <Button variant='contained' color='primary'>MUI Installed</Button>
          </div>
        </Layout>
      </ThemeProvider>
    );
};

export default App;