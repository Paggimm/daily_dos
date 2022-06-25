<template>
  <div class="logout-container">
    <h1>
      Logout
    </h1>
    <button @click="logout">
      logout
    </button>
  </div>
</template>
<script lang="ts" setup>
import router from '@/router'
import { useAuthStore } from '@/store/AuthStore'

const authStore = useAuthStore()

function logout()
{
    authStore.setToken('')
    router.push({name: "Login"});
}

</script>
<style scoped>
</style>
