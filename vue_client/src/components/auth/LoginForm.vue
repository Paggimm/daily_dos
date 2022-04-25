<template>
  <div class="columns">
    <div class="column is-one-third is-offset-one-third">
      <form
        v-if="!loggedIn"
        @submit.prevent="submit"
      >
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
      <p v-else>You are logged in.</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { VuexHandler } from "@/store/store";
import { LoginResponse, LoginViewModel } from "@/generated/models";
import { ref } from "vue";

const username = ref("");
const password = ref("");
const invalidLogin = ref(false);
const vuexHandler = new VuexHandler();

const loggedIn = vuexHandler.getters.isLoggedIn;

async function getToken(request: LoginViewModel): Promise<void> {
  try {
    const response = await fetch("http://localhost:8085/login", {
      body: JSON.stringify(request),
      method: "POST",
    });
    switch (response.status) {
      case 200:
        {
          const body: LoginResponse = await response.json();
          vuexHandler.mutations.setToken(body.token);
        }
        break;
      case 401:
        invalidLogin.value = true;
        break;
    }
  } catch (e) {
    console.log(e);
  }
}

async function submit(): Promise<void> {
  invalidLogin.value = false;
  await getToken({ name: username.value, password: password.value });
}
</script>
