import { defineStore } from 'pinia';
import { axiosInstance } from '@/config';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: localStorage.getItem('token') || '',
        userId: null as number | null
    }),
    getters: {
        isAuthenticated: (state) => !!state.token
    },
    actions: {
        async login(phone: number, password: string) {
            const res = await axiosInstance.post('/account/login', { phone, password });
            this.token = res.data.token;
            localStorage.setItem('token', this.token);

            const payload = JSON.parse(atob(this.token.split('.')[1]));
            this.userId = payload.sub;

            return true;
        },
        async register(data: { phone: number; password: string; role: number }) {
            const res = await axiosInstance.post('/account/register', data);
            this.userId = res.data.id;
            return res.data;
        },
        logout() {
            this.token = '';
            localStorage.removeItem('token');
        }
    }
});
