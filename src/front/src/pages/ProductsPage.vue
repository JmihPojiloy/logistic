<template>
  <div class="products-page">
    <h1>Список товаров</h1>
    <div v-for="product in products" :key="product.id" class="product-card">
      <h2>{{ product.name }} (ID: {{ product.id }})</h2>
      <p><strong>Описание:</strong> {{ product.description || '—' }}</p>
      <p><strong>Цена:</strong> {{ product.price?.amount }} {{ product.price?.currency }}</p>
      <p><strong>Вес:</strong> {{ product.weight ?? '—' }} кг</p>
      <p><strong>Размеры (В × Ш × Д):</strong> {{ product.height ?? '—' }} × {{ product.width ?? '—' }} × {{ product.length ?? '—' }}</p>
      <p><strong>Артикул:</strong> {{ product.code }}</p>
      <div v-if="product.inventories?.length">
        <h4>Остатки на складах:</h4>
        <ul>
          <li v-for="inv in product.inventories" :key="inv.id">
            Склад ID: {{ inv.warehouseId }} — Кол-во: {{ inv.quantity }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { axiosInstance } from '@/config';
import { useAuthStore } from '@/store/auth';

const products = ref([]);
const auth = useAuthStore();

onMounted(async () => {
  const res = await axiosInstance.get('/products/getall', {
    headers: { Authorization: `Bearer ${auth.token}` }
  });
  products.value = res.data;
});
</script>

<style scoped>
.products-page {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}
.product-card {
  border: 1px solid #ccc;
  border-radius: 12px;
  padding: 16px;
  margin-bottom: 20px;
  background-color: #fafafa;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}
.product-card ul {
  list-style: none;
  padding-left: 0;
  margin-left: 0;
}

.product-card li {
  margin-bottom: 4px;
}
</style>
