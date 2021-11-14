<template>
  <div>
    <header-component />
    <server-status />
    <router-view />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import Header from "./components/header/Header.vue";
import ServerStatus from "./components/server/ServerStatus.vue";

export default defineComponent({
  components: {
    "header-component": Header,
    "server-status": ServerStatus,
  },
});
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
