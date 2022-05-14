<template>
  <div>
    <Header />
    <ServerStatus />
    <router-view />
  </div>
</template>

<script setup lang="ts">
import Header from "@/components/header/Header.vue";
import ServerStatus from "@/components/server/ServerStatus.vue";
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
