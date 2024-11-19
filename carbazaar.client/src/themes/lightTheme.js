import { createTheme } from '@mui/material/styles'

const theme = createTheme({
    palette: {
        mode: 'light',
        primary: {
            main: '#1E88E5'
        },
        secondary: {
            main: '#FFC107'
        },
        background: {
            default: '#F9F9F9',
            paper: '#FFFFFF',
        },
        text: {
            primary: '#212121',
            secondary: '#757575'
        },
        error: {
            main: '#D32F2F'
        }
    }
});

export default theme;