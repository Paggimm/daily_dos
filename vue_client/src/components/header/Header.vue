<template>
  <header class="header-container">
    <img
      class="banner-img"
      src="../../../../images/test_banner.jpg"
    >
    <h1 class="nav-headline">
      DailyDos
    </h1>
    <nav id="nav">
      <router-link to="/">Home</router-link> |
      <template v-if="!loggedIn">
        <router-link to="/login">
          Login
        </router-link> |
        <router-link to="/register">
          Register
        </router-link> |
      </template>
      <router-link to="/about">About</router-link>
    </nav>
  </header>
</template>

<script setup lang="ts">
import { VuexHandler } from "@/store/store";

const vuexHandler = new VuexHandler();
const loggedIn = vuexHandler.getters.isLoggedIn;
</script>

<style>
.header-container {
  height: 7vw;
  position: relative;
}
.header-container .banner-img {
  object-fit: fill;
  height: 100%;
  width: 100%;
}
.header-container #nav {
  position: absolute;
  right: 2vw;
  top: 4vw;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
  font-size: 1.5vw;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.nav-headline {
  position: absolute;
  top: 0vw;
  left: 2vw;
  font-weight: bold;
  font-size: 5vw;
}
</style>
