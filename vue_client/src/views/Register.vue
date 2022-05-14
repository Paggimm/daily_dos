<template>
  <h1 class="title">Register</h1>
  <div class="register-form">
    <RegisterForm />
  </div>
</template>

<script setup lang="ts">
import RegisterForm from "@/components/auth/RegisterForm.vue";
</script>
<style>
.register-form {
  margin-left: 30%;
  margin-right: 30%;
}
</style>
