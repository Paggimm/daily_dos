<template>
    <div class="plan-info-card">
        <h3>{{ plan.activity.name }}</h3>
        <p>DATE: {{ dateText }}</p>
    </div>
</template>

<script lang="ts" setup>
import type {Plan} from "@/generated/models";
import {computed, defineProps} from "vue";

const props = defineProps<{
    plan: Plan
}>();

const dateText = computed(() => {
    const day = props.plan.date.getDate();
    const month = props.plan.date.getMonth();
    const year = props.plan.date.getFullYear();
    const hour = props.plan.date.getHours();
    const minute = props.plan.date.getMinutes();

    return `${day}.${month}.${year} - ${hour}:${minute}`;
});

</script>

<style lang="less" scoped>
@import "@/css/colors.less";
@import "@/css/measures.less";

.plan-info-card {
    background-color: @background-information-surface;
    border-radius: @default-border-radius;
}

</style>
