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
import { useAuthStore } from '@/store/AuthStore'

const authStore = useAuthStore()

function logout()
{
    authStore.setToken('')
}

</script>
<style scoped lang="less">
</style>
