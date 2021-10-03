<template>
  <div class="header-container">
    <img
      class="banner-img"
      src="../../../../images/test_banner.jpg"
    >
    <h1 class="nav-headline">
      DailyDos
    </h1>
    <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/login">Login</router-link> |
      <router-link to="/register">Register</router-link> |
      <router-link to="/about">About</router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";

export default class Header extends Vue {}
</script>

<style>
.header-container {
  height: 18vw;
  position: relative;
}
.header-container .banner-img {
  object-fit: fill;
  height: 100%;
  width: 100%;
}
.header-container #nav {
  position: absolute;
  right: 10%;
  top: 10%;
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
  top: 20%;
  left: 40%;
  font-weight: bold;
  font-size: 6vw;
}
</style>
