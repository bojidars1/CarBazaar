const { default: axios } = require("axios");
const { useNavigate } = require("react-router-dom");

const navigate = useNavigate();

const api = axios.create({
    baseURL: 'https://localhost:7100/api',
    withCredentials: true
});

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        if (error.response?.status === 401) {
            try {
                const refreshResponse = await api.post('/account/refresh-token');
                const { accessToken } = refreshResponse.data;

                api.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;

                return api.request(error.config);
            } catch (refreshError) {
                console.error('Refresh token failed, redirecting to login.');
                navigate('/login');
            }
        }
        return Promise.reject(error);
    }
);