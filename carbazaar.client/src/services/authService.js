import { jwtDecode } from "jwt-decode";
import { useDispatch } from "react-redux";
import { setAuthenticated, logout } from '../redux/authSlice';
import { setUser, clearUser } from '../redux/userSlice';
import api from '../api/api';
import { useNavigate } from "react-router-dom";

const useAuth = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const checkAccessToken = async () => {
        const token = localStorage.getItem('token');

        if (token) {
            const decodedToken = jwtDecode(token);
            const isExpired = Date.now() >= decodedToken.exp * 1000;

            if (isExpired) {
                await refreshAccessToken();
            } else {
                const userEmail = decodedToken.email;
                dispatch(setAuthenticated(token));
                dispatch(setUser(userEmail));
            }
        }
    };

    const refreshAccessToken = async () => {
        try {
            const response = await api.post('/account/refresh-token');
            const { accessToken } = response.data;

            localStorage.setItem('token', accessToken);

            const decodedToken = jwtDecode(accessToken);
            const userEmail = decodedToken.email;
            dispatch(setAuthenticated(accessToken));
            dispatch(setUser(userEmail));
        } catch (err) {
            console.log('Error refreshing access token: ', err);
            dispatch(logout());
            dispatch(clearUser());
            navigate('/login');
        }
    }

    return { checkAccessToken };
};