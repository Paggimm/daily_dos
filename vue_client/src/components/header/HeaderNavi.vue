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
<style scoped lang="less">
@import "@/css/colors.less";
@import "@/css/measures.less";

.header-navigation {
    height: 5vw;
    background-color: @background-surface-dark;
    display: flex;
    justify-content: center;
}

.header-navigation-item {
    width: 10%;
    background-color: @background-interactable-surface;
    margin: 1vw;
    border-radius: @default-border-radius;
}

.header-navigation-item:hover {
    background-color: @background-interactable-surface-focused;
    cursor: pointer;
}
</style>
