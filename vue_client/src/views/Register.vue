<template>
  <h1 class="title">Register</h1>
  <div class="register-form">
    <register-form />
  </div>
</template>

<script lang="ts">
import RegisterForm from "@/components/auth/RegisterForm.vue";
import { defineComponent } from "vue";

export default defineComponent({
  components: {
    "register-form": RegisterForm,
  },
});
</script>
<style>
.register-form {
  margin-left: 30%;
  margin-right: 30%;
}
</style>
