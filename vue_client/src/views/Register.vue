<template>
  <h1 class="title">Register</h1>
  <div class="register-form">
    <register-form />
  </div>
</template>

<script lang="ts">
import RegisterForm from "@/components/auth/RegisterForm.vue";
import { Vue, Options } from "vue-class-component";

@Options({
  components: {
    "register-form": RegisterForm,
  },
})
export default class Register extends Vue {}
</script>
<style>
.register-form {
  margin-left: 30%;
  margin-right: 30%;
}
</style>
