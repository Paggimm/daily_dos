<template>
  <div
    class="sidenavi-item-container"
    @click="onItemClicked"
  >
    <div
      class="sidenavi-item-content"
    >
      {{ props.text }}
    </div>
  </div>
</template>
<script lang="ts" setup>
import router from '@/router'
import {defineProps} from 'vue';

interface Props {
    text: string,
    routeTarget: string,
}

const props = defineProps<Props>();

  function onItemClicked() {
    router.push(props.routeTarget);
  }

</script>

<style scoped>
    .sidenavi-item-container {
        aspect-ratio: 3;
        border-bottom: 1px solid #64646494;
        display: flex;
        align-items: center;
    }

    .sidenavi-item-container > .sidenavi-item-content {
        margin-left: auto;
        margin-right: auto;
    }
</style>
