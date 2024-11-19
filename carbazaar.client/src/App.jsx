import React from 'react';
import { Button, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';
import Layout from './layouts/layout';
import CenterBox from './components/CenterBox';

const App = () => {
    return(
      <ThemeProvider theme={lightTheme}>
        <Layout>
          <div>
            <h1>Welcome to CarBazaar</h1>
            <CenterBox>
              <Button variant='contained' color='primary'>MUI Installed</Button>
            </CenterBox>
          </div>
        </Layout>
      </ThemeProvider>
    );
};

export default App;