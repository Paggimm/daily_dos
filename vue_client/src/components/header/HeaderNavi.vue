<template>
    <div class="header-navigation h-[5vw] bg-sky-500 flex place-content-center">
        <div
            v-for="(item, index) in headerItems"
            :key="index"
            class="header-navigation-item w-[10%] bg-white m-[1vw] rounded-md min cursor-pointer"
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
<style scoped>
.header-navigation-item:hover {
    background-color: #bae6fd;
}
</style>
