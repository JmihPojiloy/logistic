import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '@/pages/LoginPage.vue';
import RegisterPage from '@/pages/RegisterPage.vue';
import ProductsPage from '@/pages/ProductsPage.vue';
import UserProfile from "@/components/UserProfile.vue";
import CartPage from "@/pages/CartPage.vue";

const routes = [
    { path: '/login', component: LoginPage },
    { path: '/register', component: RegisterPage },
    { path: '/products', component: ProductsPage, meta: { requiresAuth: true } },
    { path: '/cabinet', component: UserProfile, meta: { requiresAuth: true } },
    { path: '/cart', component: CartPage, meta: { requiresAuth: true } },
    { path: '/', redirect: '/login' }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, _, next) => {
    const isAuthenticated = !!localStorage.getItem('token');
    if (to.meta.requiresAuth && !isAuthenticated) {
        next('/login');
    } else {
        next();
    }
});

export default router;
