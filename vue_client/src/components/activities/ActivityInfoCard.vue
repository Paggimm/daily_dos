<template>
    <div
        class="rounded-sm m-1 aspect-[5/1] w-2/5 bg-sky-200 flex justify-items-center justify-center"
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
