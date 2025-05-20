<template>
  <form @submit.prevent="submit" class="auth-form">
    <h2>Регистрация</h2>
    <input v-model="phone" type="text" placeholder="Телефон" required />
    <input v-model="password" type="password" placeholder="Пароль" required />
    <select v-model="role">
      <option :value="0">Admin</option>
      <option :value="1">User</option>
      <option :value="2">Manager</option>
    </select>
    <div class="button-group">
      <button type="submit">Зарегистрироваться</button>
      <button type="button" @click="goToLogin">Войти</button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';

const phone = ref('');
const password = ref('');
const role = ref(1);
const router = useRouter();
const auth = useAuthStore();

async function submit() {
  try {
    await auth.register({ phone: Number(phone.value), password: password.value, role: role.value });
    router.push('/login');
  } catch {
    alert('Ошибка регистрации');
  }
}

function goToLogin() {
  router.push('/login');
}
</script>

<style scoped>
.auth-form {
  max-width: 400px;
  margin: 80px auto;
  padding: 30px;
  border-radius: 12px;
  background-color: #f8f8f8;
  box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

input, select {
  margin: 10px 0;
  padding: 10px;
  font-size: 1rem;
  border-radius: 8px;
  border: 1px solid #ccc;
}

.button-group {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}

button {
  padding: 10px 16px;
  font-size: 1rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

button[type="submit"] {
  background-color: #2196f3;
  color: white;
}

button[type="button"] {
  background-color: #4caf50;
  color: white;
}
</style>
