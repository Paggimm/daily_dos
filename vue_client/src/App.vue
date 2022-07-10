<template>
  <div>
    <Header />
    <SideNavi
      v-if="authStore.isLoggedIn"
    />

    <div class="router-view">
      <router-view />
    </div>

    <ServerStatus />
  </div>
</template>

<script setup lang="ts">
import Header from "@/components/header/Header.vue"
import ServerStatus from "@/components/server/ServerStatus.vue"
import SideNavi from "@/components/sidenavi/SideNavi.vue";
import { useAuthStore } from "./store/AuthStore";

const authStore = useAuthStore();
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.router-view {
  margin-left: 15vw;
}
</style>
