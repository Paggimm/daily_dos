<template>
    <div
        v-click-outside="disableSelection"
        class="activity-selection-container"
    >
        <div
            class="activity-selection-activation"
            @click="toggleSelectionEnabled"
        >
            select Activity
        </div>
        <div
            v-if="selectionEnabled"
            class="activity-selection"
        >
            <div
                v-for="(activity, index) in activityList"
                :key="index"
                class="activity-selection-item"
            >
                <p>{{ activity.name }}</p>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import {Activity} from "@/generated/models";
import {PropType, ref, defineProps} from "vue";

const props = defineProps({
    activityList: {
        type: Array as PropType<Array<Activity>>,
        required: true,
    }
});

const selectionEnabled = ref(false);
const disableSelection = () => selectionEnabled.value = false;
const enableSelection = () => selectionEnabled.value = true;
const toggleSelectionEnabled = () => selectionEnabled.value = !selectionEnabled.value

</script>
<style scoped lang="less">
.activity-selection-activation {
    aspect-ratio: 1;
    border: 1px solid black;
    height: 10vw;

    &:hover {
        cursor: pointer;
    }
}

.activity-selection {
    display: flex;

    .activity-selection-item {
        aspect-ratio: 1;
        width: 5vw;
        border: 1px solid black;
    }
}

</style>
