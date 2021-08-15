<template>
  <div class="columns">
    <div class="column is-one-third is-offset-one-third">
      <label class="label">Username</label>
      <input
        v-model="username"
        class="input block"
        type="text"
      >
      <label class="label">Password</label>
      <input
        v-model="password"
        class="input block"
        type="password"
      >
      <button
        class="button is-primary"
        @click="submit"
      >
        Submit
      </button>
    </div>
  </div>
</template>

<script lang="ts">
  import { Vue } from 'vue-class-component'

  export default class LoginForm extends Vue {
    protected username = "";
    protected password = "";

    protected submit(): void {
      alert("Not supported yet :(");
    }
  }
</script>
