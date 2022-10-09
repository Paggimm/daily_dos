<template>
    <div class="header-navigation">
        <div
              v-for="(item, index) in headerItems"
              :key="index"
              class="header-navigation-item"
              @click="toActivity"
        >
            {{ item }}
        </div>
    </div>
</template>

<script setup lang="ts">
import router from "@/router";
import {RouterDefinitions} from "@/enums/RouterDefinitions";

const headerItems = ['ACTIVITY', 'PLANS', 'BLABLA']

function toActivity() {
    router.push(RouterDefinitions.ACTIVITIES);
}

</script>
<style scoped>

.header-navigation {
    height: 5vw;
    background-color: var(--background-surface-dark);
    display: flex;
    justify-content: center;
}

.header-navigation-item {
    width: 10%;
    background-color: var(--background-interactable-surface);
    margin: 1vw;
    border-radius: var(--default-border-radius);
}

.header-navigation-item:hover {
    background-color: var(--background-interactable-surface-focused);
    cursor: pointer;
}
</style>
