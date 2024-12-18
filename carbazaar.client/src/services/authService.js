import { jwtDecode } from "jwt-decode";
import { useDispatch } from "react-redux";
import { setAuthenticated, logout } from '../redux/authSlice';
import { setUser, clearUser } from '../redux/userSlice';
import api from '../api/api';

const useAuth = () => {
    const dispatch = useDispatch();

    const checkAccessToken = async () => {
        const token = localStorage.getItem('token');

        if (token) {
            const decodedToken = jwtDecode(token);
            const isExpired = Date.now() >= decodedToken.exp * 1000;

            if (isExpired) {
                await refreshAccessToken();
            } else {
                const userId = decodedToken.sub;
                const userEmail = decodedToken.email;
                const carListings = decodedToken.CarListings;
                const user = {
                  userId: userId,
                  userEmail: userEmail,
                  carListings: carListings
                };
                dispatch(setAuthenticated(true));
                dispatch(setUser(user));
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
            dispatch(setAuthenticated(true));
            dispatch(setUser(userEmail));
        } catch (err) {
            console.log('Error refreshing access token: ', err);
            localStorage.removeItem('token');
            dispatch(logout());
            dispatch(clearUser());
        }
    }

    return { checkAccessToken };
};

export default useAuth;