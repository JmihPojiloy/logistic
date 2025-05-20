import axios from 'axios';

const API_BASE_URL = 'https://localhost:7268';

const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
});

export { API_BASE_URL, axiosInstance };
