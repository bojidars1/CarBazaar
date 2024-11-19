import React from 'react';
import { Button, ThemeProvider } from '@mui/material';
import lightTheme from './themes/lightTheme';

const App = () => {
    return(
      <ThemeProvider theme={lightTheme}>
        <div>
          <h1>Welcome to CarBazaar</h1>
          <Button variant='contained' color='primary'>MUI Installed</Button>
        </div>
      </ThemeProvider>
    );
};

export default App;