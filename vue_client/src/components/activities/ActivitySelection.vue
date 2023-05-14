<template>
    <div
        v-click-outside="disableSelection"
        class="activity-selection-container"
    >
        <div
            class="pulsating-on-hover border-s-black rounded-sm bg-white h-full cursor-pointer"
            @click="toggleSelectionEnabled"
        >
            <p v-if="selectedActivity === undefined">select activity</p>
            <p v-else>{{ selectedActivity.name }}</p>
        </div>
        <transition name="selection-container">
            <div
                v-if="selectionEnabled"
                class="absolute bottom-0 w-[62vw] h-[30vw] rounded-t-sm bg-sky-100 flex gap-2 p-2"
            >
                <div
                    v-for="(activity, index) in activityList"
                    :key="index"
                    :class="{selected: isActivitySelected(activity)}"
                    class="pulsating-on-hover aspect-square w-[8vw] h-[8vw] rounded-sm border-s-black bg-sky-200 cursor-pointer hover:bg-white"
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

<style scoped>
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
