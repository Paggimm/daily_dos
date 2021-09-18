<template>
  <div class="columns">
    <div class="column is-one-third is-offset-one-third">
      <form @submit.prevent="submit">
        <div class="field">
          <label class="label">Username</label>
          <div class="control has-icons-left">
            <input
              v-model="username"
              :class="invalidLogin ? 'is-danger' : ''"
              class="input block"
              required="required"
              type="text"
            >
            <span class="icon is-small is-left">
              <i class="fas fa-user" />
            </span>
          </div>
        </div>
        <div class="field">
          <label class="label">Password</label>
          <div class="control has-icons-left">
            <input
              v-model="password"
              :class="invalidLogin ? 'is-danger' : ''"
              class="input block"
              required="required"
              type="password"
            >
            <span class="icon is-small is-left">
              <i class="fas fa-lock" />
            </span>
          </div>
          <p
            v-if="invalidLogin"
            class="help is-danger"
          >
            This login is invalid
          </p>
        </div>
        <button class="button is-primary">Submit</button>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { VueWithStore } from "@/store";

interface TokenRequest {
  email: string;
  password: string;
}

interface TokenResponse {
  token: string;
}

export default class LoginForm extends VueWithStore {
  protected username = "";
  protected password = "";

  protected invalidLogin = false;

  protected async submit(): Promise<void> {
    this.invalidLogin = false;
    await this.getToken({ email: this.username, password: this.password });
  }

  private async getToken(request: TokenRequest): Promise<void> {
    try {
      const response = await fetch("http://localhost:8085/token", {
        body: JSON.stringify(request),
        method: "POST",
      });
      switch (response.status) {
        case 200:
          {
            const body: TokenResponse = await response.json();
            this.mutations.setToken(body.token);
          }
          break;
        case 401:
          this.invalidLogin = true;
          break;
      }
    } catch (e) {
      console.log(e);
    }
  }
}
</script>
