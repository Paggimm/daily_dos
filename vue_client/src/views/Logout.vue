<template>
    <div class="logout-container">
        <h1>
            Logout
        </h1>
        <button
                class="button submit w-1/4 h-[4vw]"
                @click="logout"
        >
            logout
        </button>
    </div>
</template>
<script lang="ts" setup>
import router from '@/router'
import {useAuthStore} from '@/store/AuthStore'
import {RouterDefinitions} from "@/enums/RouterDefinitions";

const authStore = useAuthStore()

function logout() {
    authStore.setToken('')
    router.push({name: RouterDefinitions.LOGIN});
}

</script>
