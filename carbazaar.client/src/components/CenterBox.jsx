import { Box } from '@mui/material';
import React from 'react';

const CenterBox = ( { children } ) => {
    return (
        <Box sx={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
        }}>
            {children}
        </Box>
    );
};

export default CenterBox;