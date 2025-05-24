<template>
  <div class="user-profile">
    <h2>Личный кабинет</h2>
    <form @submit.prevent="saveChanges">
      <div>
        <label>ID:</label>
        <input type="text" v-model="editableUser.id" disabled />
      </div>
      <div>
        <label>Имя:</label>
        <input type="text" v-model="editableUser.firstName" required />
      </div>
      <div>
        <label>Фамилия:</label>
        <input type="text" v-model="editableUser.lastName" />
      </div>
      <div>
        <label>Отчество:</label>
        <input type="text" v-model="editableUser.middleName" />
      </div>
      <div>
        <label>Email:</label>
        <input type="email" v-model="editableUser.email" />
      </div>
      <div>
        <label>Телефон:</label>
        <input type="number" v-model.number="editableUser.phoneNumber" required />
      </div>
      <div>
        <label>Пол:</label>
        <select v-model="editableUser.gender">
          <option :value="null">Не указан</option>
          <option :value="0">Мужской</option>
          <option :value="1">Женский</option>
        </select>
      </div>

      <h3>Адреса</h3>
      <div v-for="address in editableUser.addresses" :key="address.id" class="address-block">
      <div><strong>ID:</strong> {{ address.id }}</div>
        <div><label>Индекс:</label> <input v-model="address.zip" /></div>
        <div><label>Страна:</label> <input v-model="address.country" /></div>
        <div><label>Город:</label> <input v-model="address.city" /></div>
        <div><label>Улица:</label> <input v-model="address.street" /></div>
        <div><label>Дом:</label> <input v-model="address.houseNumber" /></div>
        <div><label>Квартира:</label> <input v-model="address.apartmentNumber" /></div>
      </div>

      <div class="buttons">
        <button type="submit">Сохранить</button>
        <button type="button" @click="cancelChanges">Отмена</button>
      </div>
    </form>

    <div v-if="successMessage" class="success-message">
      {{ successMessage }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { useAuthStore } from '@/store/auth';
import { axiosInstance } from '@/config';

const auth = useAuthStore();
const userId = auth.userId;
const originalUser = ref<any>(null);
const editableUser = reactive<any>({
  id: 0,
  firstName: '',
  lastName: '',
  middleName: '',
  email: '',
  phoneNumber: 0,
  gender: null,
  addresses: []
});
const successMessage = ref('');

async function loadUser() {
  const res = await axiosInstance.get(`/users/getbyid/${userId}`);
  originalUser.value = JSON.parse(JSON.stringify(res.data));
  Object.assign(editableUser, res.data);

  if(editableUser.addresses.length == 0) {
    editableUser.addresses.push({
      zip: '',
      county: '',
      city: '',
      street: '',
      houseNumber: '',
      apartmentNumber: '',
      latitude: null,
      longitude: null
    });
  }
}

function cancelChanges() {
  if (originalUser.value) {
    Object.assign(editableUser, JSON.parse(JSON.stringify(originalUser.value)));
  }
}

async function saveChanges() {
  try {
    const payload = JSON.parse(JSON.stringify(editableUser));

    payload.addresses = payload.addresses.filter((addr: Record<string, any>) =>
        Object.values(addr).some(val => val !== '' && val !== null)
    )
    
    payload.addresses.forEach((address: any) => {
      if (!address.id) {
        delete address.id;
      }
    });
    
    const res = await axiosInstance.put('/users/update', editableUser);
    successMessage.value = 'Профиль успешно обновлён!';
    setTimeout(() => (successMessage.value = ''), 3000);
    originalUser.value = JSON.parse(JSON.stringify(res.data));
    Object.assign(editableUser, res.data);
  } catch (err) {
    console.error('Ошибка при обновлении профиля:', err);
  }
}

onMounted(() => {
  loadUser();
});
</script>

<style scoped>
.user-profile {
  max-width: 600px;
  margin: auto;
}

form div {
  margin-bottom: 10px;
}

.address-block {
  border: 1px solid #ccc;
  padding: 10px;
  margin-bottom: 15px;
}

.buttons {
  display: flex;
  gap: 10px;
}

.success-message {
  margin-top: 15px;
  color: green;
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
