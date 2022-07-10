<template>
  <div class="sidenavi-container">
    <div class="sidenavi-headline">
      <p>MANAGE</p>
    </div>
    <div class="sidenavi-items">
      <SideNaviItemVue
        text="ACTIVITIES"
        route-target="Activities"
      />
    </div>
  </div>
</template>
<script lang="ts" setup>
import { useAuthStore } from '@/store/AuthStore'
import SideNaviItemVue from './SideNaviItem.vue';

const authStore = useAuthStore()

</script>

<style scoped>
    .sidenavi-container {
        width: 15vw;
        position: absolute;
        display: flex;
        flex-direction: column;
        border-left: 1px solid #64646494;
        border-right: 1px solid #64646494;
    }

    .sidenavi-headline {
        aspect-ratio: 3;
        background-color: #BCCDCD50;
        border-bottom: 1px solid #64646494;
        display: flex;
        align-items: center;
    }

    .sidenavi-headline > p {
        margin-left: auto;
        margin-right: auto;
    }

    .sidenavi-items {
        background-color: #9BF4FF21;
    }
</style>
