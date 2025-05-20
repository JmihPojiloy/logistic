<template>
  <form @submit.prevent="submit" class="auth-form">
    <h2>Вход</h2>
    <input v-model="phone" type="text" placeholder="Телефон" required />
    <input v-model="password" type="password" placeholder="Пароль" required />
    <div class="button-group">
      <button type="submit">Войти</button>
      <button type="button" @click="goToRegister">Зарегистрироваться</button>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';

const phone = ref('');
const password = ref('');
const router = useRouter();
const auth = useAuthStore();

async function submit() {
  try {
    await auth.login(Number(phone.value), password.value);
    router.push('/products');
  } catch {
    alert('Ошибка авторизации');
  }
}

function goToRegister() {
  router.push('/register');
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

input {
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
  gap: 14px;
}

button {
  padding: 10px 16px;
  font-size: 1rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

button[type="submit"] {
  background-color:  #2196f3;
  color: white;
}

button[type="button"] {
  background-color: #4caf50;
  color: white;
}
</style>
