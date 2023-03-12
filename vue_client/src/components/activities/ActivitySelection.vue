<template>
    <div
        v-click-outside="disableSelection"
        class="activity-selection-container"
    >
        <div
            class="activity-selection-activation pulsating-on-hover"
            @click="toggleSelectionEnabled"
        >
            <p v-if="selectedActivity === undefined">select activity</p>
            <p v-else>{{ selectedActivity.name }}</p>
        </div>
        <transition name="selection-container">
            <div
                v-if="selectionEnabled"
                class="activity-selection"
            >
                <div
                    v-for="(activity, index) in activityList"
                    :key="index"
                    :class="{selected: isActivitySelected(activity)}"
                    class="activity-selection-item pulsating-on-hover"
                    @click="activityClicked(index)"
                >
                    <p>{{ activity.name }}</p>
                </div>
            </div>
        </transition>
    </div>
</template>

<script lang="ts" setup>
import type {Activity} from "@/generated/models";
import {defineEmits, defineProps, ref} from "vue";

const props = defineProps<{
    activityList: Activity[],
    selectedActivity: Activity | undefined
}>();

const emit = defineEmits([
    'update:selectedActivity'
]);

const selectionEnabled = ref(false);
const disableSelection = () => selectionEnabled.value = false;
const enableSelection = () => selectionEnabled.value = true;
const toggleSelectionEnabled = () => selectionEnabled.value = !selectionEnabled.value

const activityClicked = (index: number) => {
    if (props.activityList.length >= index) {
        const activity = props.activityList[index];
        emit('update:selectedActivity', activity);
        disableSelection();
    }
};

const isActivitySelected = (activity: Activity) => {
    if (props.selectedActivity === undefined) {
        return false;
    }
    return activity.id === props.selectedActivity.id
}
</script>

<style lang="less" scoped>
@import "@/css/colors.less";
@import "@/css/measures.less";

.activity-selection-activation {
    border: 1px solid black;
    border-radius: @default-border-radius;
    background-color: @background-interactable-surface;
    height: 100%;
    width: 100%;

    &:hover {
        cursor: pointer;
    }
}

.activity-selection {
    position: absolute;
    bottom: 0;
    width: 90%;
    height: 30vw;
    margin-left: 3%;
    background-color: @background-information-surface;
    padding: 1vw;
    border-top-left-radius: @default-border-radius;
    border-top-right-radius: @default-border-radius;
    display: flex;
    //animation: fly-from-bottom 0.5s ease-out;

    .activity-selection-item {
        aspect-ratio: 1;
        width: 8vw;
        height: 8vw;
        border-radius: @default-border-radius;
        background-color: @background-interactable-surface;
        border: 1px solid black;

        &.selected {
            background-color: @background-interactable-surface-focused;
        }

        &:hover {
            cursor: pointer;
        }
    }
}

.selection-container-enter-active {
    animation: fly-from-bottom 0.5s ease-out;
}

.selection-container-leave-active {
    animation: fly-from-bottom reverse 0.5s ease-out;
}

@keyframes fly-from-bottom {
    from {
        height: 0;
    }
    to {
        height: 30vw;
    }
}

</style>
