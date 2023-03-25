<template>
    <header class="header-container">
        <div class="header-main">
            <h1 class="nav-headline"
            @click="routeToHome"
            >
                DailyDos
            </h1>
            <!-- router links -->
            <nav id="nav">
                <router-link :to="{name: RouterDefinitions.HOME}">
                    Home
                </router-link>
                <router-link :to="{name: RouterDefinitions.ABOUT}">
                    About
                </router-link>
                <router-link
                    v-if="!loggedIn"
                    :to="{name: RouterDefinitions.LOGIN}"
                >
                    Login
                </router-link>
                <router-link
                    v-if="!loggedIn"
                    :to="{name: RouterDefinitions.REGISTER}"
                >
                    Register
                </router-link>
                <router-link
                    v-if="loggedIn"
                    :to="{name: RouterDefinitions.My}"
                >
                    MeinAccount
                </router-link>
                <router-link
                    v-if="loggedIn"
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

<style scoped lang="less">
@import "@/css/colors.less";
.header-container {
    background-color: @background-surface;
    display: flex;
    flex-direction: column;
}

.header-main {
    position: relative;
    height: 10vw;
    display: flex;
}

.header-main #nav {
    position: absolute;
    right: 2vw;
    top: 4vw;
}

#nav a {
    font-weight: bold;
    color: #2c3e50;
    font-size: 1.5vw;
    margin-right: 0.6vw;
}

#nav a.router-link-exact-active {
    color: var(--background-interactable-surface-focused);
}

.header-main .nav-headline {
    position: absolute;
    top: 0;
    left: 2vw;
    font-weight: bold;
    font-size: 5vw;

    &:hover {
        cursor: pointer;
    }
}
</style>
