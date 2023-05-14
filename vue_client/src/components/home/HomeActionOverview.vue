<template>
    <div class="flex flex-wrap justify-center m-5 gap-4">
        <HomeActionCard
            v-for="(action, index) in homeActions"
            :key="index"
            class="w-1/4"
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
</style>
