<template>
  <h1 class="title">Login</h1>
  <login-form />
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import LoginForm from "../components/auth/LoginForm.vue";

@Options({
  components: {
    "login-form": LoginForm,
  },
})
export default class Login extends Vue {}
</script>
