<!-- src/components/PaidOrderSummary.vue -->
<template>
  <div class="paid-order">
    <h2>Заказ успешно оформлен</h2>
    <p><strong>Номер заказа:</strong> {{ paidOrder.orderId }}</p>
    <p><strong>Водитель:</strong> {{ paidOrder.driverName }}</p>
    <p><strong>Расстояние:</strong> {{ paidOrder.distance }} км</p>
    <p><strong>Стоимость:</strong> {{ paidOrder.totalCost?.sum }} {{ getCurrencyText(paidOrder.totalCost?.currency) }}</p>
    <p><strong>Время доставки:</strong> {{ formatTimeSpan(paidOrder.leadTime) }}</p>
    <p><strong>Статус:</strong> {{ getStatusText(paidOrder.status) }}</p>

    <button @click="$emit('reset')">Сбросить</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'PaidOrderSummary',
  props: {
    paidOrder: {
      type: Object,
      required: true
    }
  },
  emits: ['reset'],
  setup(_, { emit }) {
    enum OrderStatus {
      NotPaid = 0,
      Assembly = 1,
      Loading = 2,
      InTransit = 3,
      Arrived = 4,
      Issued = 5
    }

    enum Currency {
      RUB = 0,
      USD = 1,
      KZT = 2,
      EUR = 3
    }

    function getStatusText(status?: OrderStatus): string {
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

    function getCurrencyText(currency?: Currency): string {
      switch (currency) {
        case Currency.RUB: return "₽";
        case Currency.USD: return "$";
        case Currency.KZT: return "₸";
        case Currency.EUR: return "€";
        default: return `?`;
      }
    }

    function formatTimeSpan(time?: string): string {
      if (!time) return "";
      const [hours, minutes] = time.split(':');
      return `${parseInt(hours)} ч ${parseInt(minutes)} мин`;
    }

    return {
      getStatusText,
      getCurrencyText,
      formatTimeSpan,
      emit
    };
  }
});
</script>

<style scoped>
.paid-order {
  border: 1px solid #ccc;
  padding: 16px;
  margin: 16px;
  border-radius: 12px;
  background-color: #e6f4ea;
}
.paid-order h2 {
  color: #2e7d32;
}
.paid-order button {
  margin-top: 12px;
  padding: 8px 14px;
  font-size: 1rem;
  border: none;
  border-radius: 8px;
  background-color: #d32f2f;
  color: white;
  cursor: pointer;
}
</style>
