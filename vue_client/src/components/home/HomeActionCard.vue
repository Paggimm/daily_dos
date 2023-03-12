<template>
    <div
          class="home-action-card-container"
          @click="toRouterLink"
    >
        <img
              class="home-action-card-image"
              src="../../../images/activity-action2.jpeg"
              alt="activity action"
        >
        <p>{{ actionCardProperties.title }}</p>
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
<style scoped lang="less">
@import "@/css/colors.less";
@import "@/css/measures.less";

.home-action-card-container {
    border-radius: @default-border-radius;
    background-color: @background-interactable-surface;
    display: grid;
    grid-template-areas: "main";
    overflow: hidden;

    &:hover {
        background-color: @background-interactable-surface-focused;
        cursor: pointer;
    }

    .home-action-card-image {
        grid-area: main;
        opacity: 0.6;
        width: 100%;
        height: 100%;
    }
    p {
        grid-area: main;
        z-index: 1;
    }
}
</style>
