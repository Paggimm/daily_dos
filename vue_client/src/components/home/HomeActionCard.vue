<template>
    <div
        class="home-action-card-container rounded-md bg-white hover:bg-sky-200 grid overflow-hidden cursor-pointer"
        @click="toRouterLink"
    >
        <img
            class="home-action-card-image opacity-60 w-full h-full row-span-full col-span-full"
            src="../../../images/activity-action2.jpeg"
            alt="activity action"
        >
        <p class="z-10 row-span-full col-span-full">{{ actionCardProperties.title }}</p>
    </div>
</template>
<script setup lang="ts">
import router from "@/router";
import {defineProps, type PropType} from "vue";
import type {IHomeActionCardConfiguration} from "@/actions/HomeActionCardPropBuilder";

const props = defineProps({
    actionCardProperties: {
        type: Object as PropType<IHomeActionCardConfiguration>,
        required: true
    }
});

function toRouterLink() {
    router.push(props.actionCardProperties.routerLink)
}

</script>
