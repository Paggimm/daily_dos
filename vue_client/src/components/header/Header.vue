<template>
    <header class="header-container bg-sky-300 flex flex-col">
        <div class="header-main relative h-[10vw] flex">
            <h1
                class="nav-headline absolute top-[2vw] left-[2vw] font-bold text-6xl cursor-pointer"
                @click="routeToHome"
            >
                DailyDos
            </h1>
            <!-- router links -->
            <nav
                id="nav"
                class="absolute top-[4vw] right-[2vw]"
            >
                <router-link
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.HOME}"
                >
                    Home
                </router-link>
                <router-link
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.ABOUT}"
                >
                    About
                </router-link>
                <router-link
                    v-if="!loggedIn"
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.LOGIN}"
                >
                    Login
                </router-link>
                <router-link
                    v-if="!loggedIn"
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.REGISTER}"
                >
                    Register
                </router-link>
                <router-link
                    v-if="loggedIn"
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.My}"
                >
                    MeinAccount
                </router-link>
                <router-link
                    v-if="loggedIn"
                    class="font-bold text-black text-2xl mr-[0.6vw]"
                    :to="{name: RouterDefinitions.LOGOUT}"
                >
                    Logout
                </router-link>
            </nav>
        </div>
        <HeaderNavi />
    </header>
</template>

<script setup lang="ts">
import {useAuthStore} from "@/store/AuthStore";
import {computed} from "vue";
import {RouterLink} from "vue-router";
import HeaderNavi from "@/components/header/HeaderNavi.vue";
import {RouterDefinitions} from "@/enums/RouterDefinitions";
import router from "@/router";

const authStore = useAuthStore();
const loggedIn = computed((): boolean => authStore.isLoggedIn);

const routeToHome = () => {
    router.push({name: RouterDefinitions.HOME});
}
</script>

<style scoped>
a.router-link-exact-active {
    color: #D9E7FF;
}
</style>
