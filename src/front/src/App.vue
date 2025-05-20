<template>
  <div id="app">
    <AppHeader v-if="auth.isAuthenticated" />
    <router-view />
  </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/store/auth';
import AppHeader from "@/components/AppHeader.vue";

const auth = useAuthStore();
</script>

<style>
body {
  margin: 0;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}
</style>
