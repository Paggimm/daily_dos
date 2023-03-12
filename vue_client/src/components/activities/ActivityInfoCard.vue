<template>
    <div
        class="activity-info-card-container"
    >
        <p>{{ activity.name }}</p>
    </div>
</template>
<script setup lang="ts">

import {defineProps, type PropType} from "vue";
import type {Activity} from "@/generated/models";

defineProps({
    activity: {
        type: Object as PropType<Activity>,
        required: true,
    }
})

</script>
<style scoped lang="less">
@import "@/css/colors.less";
.activity-info-card-container {
    border-radius: 10px;
    margin: 1vw;
    aspect-ratio: 5;
    width: 40%;
    background-color: @background-information-surface;
    justify-content: center;
    display: flex;
    align-items: center;
}
</style>
