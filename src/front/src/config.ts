import axios from 'axios';

const API_BASE_URL = 'https://localhost:7268';

const axiosInstance = axios.create({
    baseURL: API_BASE_URL,
});

// ⬇️ Добавляем interceptor для подстановки токена в каждый запрос
axiosInstance.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export { API_BASE_URL, axiosInstance };
