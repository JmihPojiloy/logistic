<template>
  <div class="cart-page">
    <h1 style="margin: 14px;">Корзина</h1>

    <PaidOrderSummary
        v-if="paidOrderData"
        :paidOrder="paidOrderData"
        @reset="resetCartAndOrder"
    />

    <template v-else>
      <div v-if="cart.items.length === 0">
        <p style="margin: 14px;">Корзина пуста</p>
      </div>

      <ul v-else>
        <li v-for="item in cart.items" :key="item.productId">
          {{ item.name }} — {{ item.quantity }} шт.
        </li>
      </ul>

      <div style="margin: 14px;" v-if="cart.items.length > 0 && address">
        <h2 style="margin: 14px;">Адрес доставки</h2>
        <p>{{ formatAddress(address) }}</p>
      </div>

      <button
          style="margin: 14px;"
          @click="placeOrder"
          v-if="cart.items.length > 0 && !order"
      >
        Оформить заказ
      </button>
    </template>

    <div style="margin: 14px;" v-if="order && !paidOrderData">
      <p>Статус: {{ getStatusText(order.status) }}</p>
      <p v-if="order.deliveryCost">
        Доставка: {{ order.deliveryCost.sum }} {{ getCurrencyText(order.deliveryCost.currency) }}
      </p>
      <button
          style="margin: 14px;"
          v-if="order.status === 0"
          @click="payOrder"
      >
        Оплатить заказ
      </button>
      <p v-if="order.status === 1">
        Товар успешно оплачен и отправлен на сборку
      </p>
    </div>
  </div>
</template>


<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useCartStore } from '@/store/cart';
import { useAuthStore } from '@/store/auth';
import { axiosInstance } from '@/config';
import PaidOrderSummary from '@/components/PaidOrderSummary.vue';

const cart = useCartStore();
const auth = useAuthStore();

const address = ref<any | null>(null);
const order = ref<any | null>(null);
const userId = auth.userId;

const paidOrderData = ref<any | null>(null);

onMounted(async () => {
  const res = await axiosInstance.get(`/users/getbyid/${userId}`, {
    headers: { Authorization: `Bearer ${auth.token}` }
  });
  address.value = res.data.addresses?.[0];
});

function formatAddress(addr: any) {
  return `${addr.zip ?? ''}, ${addr.city ?? ''}, ${addr.street ?? ''}, д.${addr.houseNumber ?? ''}, кв.${addr.apartmentNumber ?? ''}`;
}

async function placeOrder() {
  const orderData = {
    userId,
    addressId: address.value.id,
    address: address.value,
    orderProducts: cart.items.map(i => ({
      productId: i.productId,
      quantity: i.quantity
    }))
  };

  const res = await axiosInstance.post('/orders/add', orderData, {
    headers: { Authorization: `Bearer ${auth.token}` }
  });

  order.value = res.data;
}

async function payOrder() {
  const res = await axiosInstance.post('/orders/pay', order.value, {
    headers: { Authorization: `Bearer ${auth.token}` }
  });

  paidOrderData.value = res.data;
}

function resetCartAndOrder() {
  cart.clearCart();
  order.value = null;
  paidOrderData.value = null;
}

enum OrderStatus {
  NotPaid = 0,
  Assembly = 1,
  Loading = 2,
  InTransit = 3,
  Arrived = 4,
  Issued = 5
}

function getStatusText(status: OrderStatus): string {
  switch (status) {
    case OrderStatus.NotPaid: return "Не оплачен";
    case OrderStatus.Assembly: return "Сборка";
    case OrderStatus.Loading: return "Погрузка";
    case OrderStatus.InTransit: return "В пути";
    case OrderStatus.Arrived: return "Прибыл";
    case OrderStatus.Issued: return "Выдан";
    default: return `Неизвестен (${status})`;
  }
}

enum Currency {
  RUB = 0,
  USD = 1,
  KZT = 2,
  EUR = 3
}

function getCurrencyText(currency: Currency): string {
  switch (currency) {
    case Currency.RUB: return "₽";
    case Currency.USD: return "$";
    case Currency.KZT: return "₸";
    case Currency.EUR: return "€";
    default: return `Неизвестная валюта (${currency})`;
  }
}

</script>

<style scoped>
button {
  padding: 10px 16px;
  font-size: 1rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  color: white;
  background-color: #2196f3;
}
.cart-page{
  gap: 1rem;
  padding: 1rem;
  margin: 1rem;
}
</style>