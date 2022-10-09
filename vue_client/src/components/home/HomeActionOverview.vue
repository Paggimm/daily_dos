<template>
    <div class="home-action-overview-container">
        <HomeActionCard
              v-for="(action, index) in homeActions"
              :key="index"
              class="home-action-card"
              :action-card-properties="action"
        />
    </div>
</template>
<script setup lang="ts">
import HomeActionCard from "@/components/home/HomeActionCard.vue";
import {buildHomeActionCardPropList} from "@/actions/HomeActionCardPropBuilder";

const homeActions = buildHomeActionCardPropList();

</script>
<style scoped>
.home-action-overview-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    margin: 5vw;
    gap: 4vw;
}

.home-action-card {
    width: 30%;
}
</style>
