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
      <router-link
        v-if="!loggedIn"
        to="/login"
      >
        Login
      </router-link> |
      <router-link
        v-if="!loggedIn"
        to="/register"
      >
        Register
      </router-link> |
      <router-link to="/about">About</router-link>
    </nav>
  </header>
</template>

<script lang="ts">
import { defineComponent, onMounted } from "vue";
import { VuexHandler } from "@/store/store";

export default defineComponent({
  setup() {
    const vuexHandler = new VuexHandler();
    let loggedIn = false;

    function isLoggedIn() {
      const token = vuexHandler.state.token;
      if (token !== undefined) {
        return true;
      }
      return false;
    }

    onMounted(() => {
      loggedIn = isLoggedIn();
    });

    return { loggedIn };
  },
});
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
