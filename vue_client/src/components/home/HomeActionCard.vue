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
import {defineProps, PropType} from "vue";
import {IHomeActionCardConfiguration} from "@/actions/HomeActionCardPropBuilder";

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
.home-action-card-container {
  aspect-ratio: 2;
  border-radius: var(--default-border-radius);
  background-color: var(--background-interactable-surface);
  display: grid;
  grid-template-areas: "main";
  overflow: hidden;

  &:hover {
    background-color: var(--background-interactable-surface-focused);
    cursor: pointer;
  }

  .home-action-card-image {
    grid-area: main;
    opacity: 0.65;
    width: 100%;
    height: 100%;

    p {
      grid-area: main;
    }
  }
}
</style>
