<template>
  <h1 class="title">Login</h1>
  <login-form @got_token="gotToken" />
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import LoginForm from "../components/login/LoginForm.vue";

@Options({
  components: {
    "login-form": LoginForm,
  },
})
export default class Login extends Vue {
  protected gotToken(token: string): void {
    this.$emit("got_token", token);
  }
}
</script>
