<template>
  <header class="app-header">
    <nav>
      <button @click="goToProducts">Все товары</button>
      <button @click="logout">Выйти</button>
    </nav>
  </header>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';

const router = useRouter();
const auth = useAuthStore();

function goToProducts() {
  router.push('/products');
}

function logout() {
  auth.logout();
  router.push('/login');
}
</script>

<style scoped>
.app-header {
  background-color: #1976d2;
  color: white;
  padding: 1rem;
  display: flex;
  justify-content: center;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

nav {
  display: flex;
  gap: 1rem;
}

button {
  background-color: white;
  color: #1976d2;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  font-weight: bold;
  cursor: pointer;
  transition: 0.2s ease-in-out;
}

button:hover {
  background-color: #e3f2fd;
}
</style>
