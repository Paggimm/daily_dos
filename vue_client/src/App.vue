<template>
  <div>
    <header-component />
    <server-status />
    <router-view />
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Header from "./components/header/Header.vue";
import ServerStatus from "./components/server/ServerStatus.vue";

@Options({
  components: {
    "header-component": Header,
    "server-status": ServerStatus,
  },
})
export default class App extends Vue {}
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
