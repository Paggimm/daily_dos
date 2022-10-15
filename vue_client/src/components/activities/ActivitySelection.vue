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
@import "../../css/colors.less";
@import "../../css/measures.less";
.activity-selection-activation {
    aspect-ratio: 1;
    border: 1px solid black;
    border-radius: @default-border-radius;
    height: 10vw;
    background-color: @background-interactable-surface;

    &:hover {
        cursor: pointer;
    }
}

.activity-selection {
    position: absolute;
    bottom: 0;
    width: 90%;
    margin-left: 3%;
    background-color: @background-information-surface;
    padding: 1vw;
    border-radius: @default-border-radius;

    .activity-selection-item {
        aspect-ratio: 1;
        width: 15%;
        border-radius: @default-border-radius;
        border: 1px solid black;
    }
}

</style>
