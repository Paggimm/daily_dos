<template>
    <div>
        <div>
            <form
                v-if="!loggedIn"
                @submit.prevent="submit"
                class="flex flex-col items-center mr-auto ml-auto"
            >
                <div>
                    <label class="label">Username</label>
                    <div>
                        <input
                            v-model="loginFeature.username.value"
                            :class="invalidLogin ? 'is-danger' : ''"
                            class="input block"
                            required
                            type="text"
                        >
                        <span class="icon is-small is-left">
                            <i class="fas fa-user" />
                        </span>
                    </div>
                </div>
                <div>
                    <label class="label">Password</label>
                    <div>
                        <input
                            v-model="loginFeature.password.value"
                            :class="invalidLogin ? 'is-danger' : ''"
                            class="input block"
                            required
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
                <button class="button submit w-1/4 h-[4vw]">Submit</button>
            </form>
            <p v-else>You are logged in.</p>
        </div>
    </div>
</template>

<script setup lang="ts">
import {useAuthStore} from "@/store/AuthStore";
import {computed, ref} from "vue";
import router from "@/router"
import {RouterDefinitions} from "@/enums/RouterDefinitions";
import useLoginFeature from "@/composables/LoginHook";

const invalidLogin = ref(false);
const loginFeature = useLoginFeature();

const authStore = useAuthStore();
const loggedIn = computed(() => authStore.isLoggedIn);

async function submit(): Promise<void> {
    invalidLogin.value = false;
    const loginResponse = await loginFeature.login();

    if (loginResponse.token !== "") {
        authStore.setToken(loginResponse.token);
        await router.push({name: RouterDefinitions.HOME});
    } else {
        invalidLogin.value = true;
    }
}
</script>
