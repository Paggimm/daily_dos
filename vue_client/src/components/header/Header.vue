<template>
  <header class="header-container">
    <div class="header-main">
      <h1 class="nav-headline">
        DailyDos
      </h1>
      <!-- router links -->
      <nav id="nav">
        <router-link :to="{name: RouterDefinitions.HOME}">
          Home
        </router-link>
        <router-link :to="{name: RouterDefinitions.ABOUT}">
          About
        </router-link>
        <router-link
          v-if="!loggedIn"
          :to="{name: RouterDefinitions.LOGIN}"
        >
          Login
        </router-link>
        <router-link
          v-if="!loggedIn"
          :to="{name: RouterDefinitions.REGISTER}"
        >
          Register
        </router-link>
        <router-link
          v-if="loggedIn"
          :to="{name: RouterDefinitions.My}"
        >
          MeinAccount
        </router-link>
        <router-link
          v-if="loggedIn"
          :to="{name: RouterDefinitions.LOGOUT}"
        >
          Logout
        </router-link>
      </nav>
    </div>
    <HeaderNavi />
  </header>
</template>

<script setup lang="ts">
import { useAuthStore } from "@/store/AuthStore";
import { computed } from "vue";
import {RouterLink} from "vue-router";
import HeaderNavi from "@/components/header/HeaderNavi.vue";
import {RouterDefinitions} from "@/enums/RouterDefinitions";

const authStore = useAuthStore();
const loggedIn = computed((): boolean => authStore.isLoggedIn);
</script>

<style>
.header-container {
  background-color: var(--background-surface);
  display: flex;
  flex-direction: column;
}
.header-main {
  position: relative;
  height: 7vw;
  display: flex;
}

.header-main #nav {
  position: absolute;
  right: 2vw;
  top: 4vw;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
  font-size: 1.5vw;
  margin-right: 0.6vw;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.header-main .nav-headline {
  position: absolute;
  top: 0vw;
  left: 2vw;
  font-weight: bold;
  font-size: 5vw;
}
</style>
