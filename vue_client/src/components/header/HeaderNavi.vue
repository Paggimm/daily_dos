<template>
    <div class="header-navigation">
        <div
            v-for="(item, index) in headerItems"
            :key="index"
            class="header-navigation-item"
            @click="toActivity(item.routerTarget)"
        >
            {{ item.name }}
        </div>
    </div>
</template>

<script lang="ts" setup>
import router from "@/router";
import {RouterDefinitions} from "@/enums/RouterDefinitions";

interface IHeaderItem {
    name: string,
    routerTarget: RouterDefinitions
}

const headerItems: IHeaderItem[] = [
    {name: 'ACTIVITY', routerTarget: RouterDefinitions.ACTIVITIES},
    {name: 'PLANS', routerTarget: RouterDefinitions.PLANS},
    {name: 'BLABLA', routerTarget: RouterDefinitions.ACTIVITIES}
]

function toActivity(routerTarget: RouterDefinitions) {
    router.push(routerTarget);
}

</script>
<style lang="less" scoped>
@import "@/css/colors.less";
@import "@/css/measures.less";

.header-navigation {
    height: 5vw;
    background-color: @background-surface-dark;
    display: flex;
    justify-content: center;
    min-height: 50px;
}

.header-navigation-item {
    width: 10%;
    background-color: @background-interactable-surface;
    margin: 1vw;
    border-radius: @default-border-radius;
    min-height: 30px;
    min-width: 100px;
}

.header-navigation-item:hover {
    background-color: @background-interactable-surface-focused;
    cursor: pointer;
}
</style>
