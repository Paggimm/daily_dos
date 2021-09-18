<template>
  <div class="columns">
    <div class="column is-one-third is-offset-one-third">
      <form @submit.prevent="submit">
        <label class="label">Username</label>
        <input
          v-model="username"
          class="input block"
          required="required"
          type="text"
        />
        <label class="label">Password</label>
        <input
          v-model="password"
          class="input block"
          required="required"
          type="password"
        />
        <button class="button is-primary">Submit</button>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";

interface TokenRequest {
  email: string;
  password: string;
}

interface TokenResponse {
  token: string;
}

export default class LoginForm extends Vue {
  protected username = "";
  protected password = "";

  protected async submit(): Promise<void> {
    await this.getToken({ email: this.username, password: this.password });
  }

  private async getToken(request: TokenRequest): Promise<void> {
    try {
      const response = await fetch("http://localhost:8085/token", {
        body: JSON.stringify(request),
        method: "POST",
      });
      const body: TokenResponse = await response.json();
      this.$emit("got_token", body.token);
      console.log("Got token!");
    } catch (e) {
      console.log("Error while fetching token :(");
    }
  }
}
</script>
