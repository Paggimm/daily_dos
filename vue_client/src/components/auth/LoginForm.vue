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
            />
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
            />
            <span class="icon is-small is-left">
              <i class="fas fa-lock" />
            </span>
          </div>
          <p v-if="invalidLogin" class="help is-danger">
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
import { LoginResponse, LoginViewModel } from "@/generated/models";
import { defineComponent } from "vue";

export default defineComponent({
  name: "LoginForm",
  data() {
    return {
      username: "",
      password: "",
      invalidLogin: false,
      vueStore: new VueWithStore(),
    };
  },
  methods: {
    async submit(): Promise<void> {
      this.invalidLogin = false;
      await this.getToken({ name: this.username, password: this.password });
    },
    async getToken(request: LoginViewModel): Promise<void> {
      try {
        const response = await fetch("http://localhost:8085/token", {
          body: JSON.stringify(request),
          method: "POST",
        });
        switch (response.status) {
          case 200:
            {
              const body: LoginResponse = await response.json();
              this.vueStore.mutations.setToken(body.token);
            }
            break;
          case 401:
            this.invalidLogin = true;
            break;
        }
      } catch (e) {
        console.log(e);
      }
    },
  },
});
</script>
