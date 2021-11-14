<template>
  <h1 class="title">Login</h1>
  <login-form />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import LoginForm from "../components/auth/LoginForm.vue";

export default defineComponent({
  components: {
    "login-form": LoginForm,
  },
});
</script>
