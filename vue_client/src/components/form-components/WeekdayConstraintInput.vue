<template>
    <div
          class="weekday-constraint-input-container block"
          @click="updateWeekdayConstraints"
    >
        <div
              v-for="(weekday, index) in weekday_list"
              :key="index"
              class="weekday-constraint-input-item"
              :class="{'selected':isWeekdaySelected(index)}"
              @click="toggleWeekday(index)"
        >
            <p>{{ weekday }}</p>
        </div>
    </div>
</template>
<script lang="ts" setup>
import {ref, defineProps, defineEmits} from 'vue';

defineProps({
    weekdayConstraints: {
        type: Array,
        required: true,
    }
});
const emits = defineEmits(['update:weekdayConstraints']);

const weekday_list = ref(['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']);
const weekday_constraints = ref<string[]>(['0', '0', '0', '0', '0', '0', '0']);

const updateWeekdayConstraints = () => {
    emits('update:weekdayConstraints', weekday_constraints.value)
}

function isWeekdaySelected(index: number): boolean {
    return weekday_constraints.value[index] !== '0';
}

function toggleWeekday(index: number) {
    console.log('toggle');
    if (weekday_constraints.value[index] === '0') {
        weekday_constraints.value[index] = '1';
    } else {
        weekday_constraints.value[index] = '0'
    }
}

</script>
<style scoped lang="less">
@import "@/css/colors.less";

.weekday-constraint-input-container {
    display: flex;
}

.weekday-constraint-input-item {
    display: flex;
    aspect-ratio: 1;
    width: 16%;
    border-radius: 10px;
    background-color: @background-interactable-surface;
    justify-content: center;
    align-items: center;
    border: 1px solid black;
}

.weekday-constraint-input-item:hover {
    cursor: pointer;
    background-color: @background-interactable-surface-focused;

    animation: pulse 1.5s infinite ease-out;
}

.weekday-constraint-input-item:not(:last-child) {
    margin-right: 2%;
}

.selected {
    background-color: @background-interactable-surface-focused;
}

@keyframes pulse {
    from {
        box-shadow: 0 0 0 0 rgba(0, 0, 0, 0.7)
    }
    70% {
        box-shadow: 0 0 0 5px rgba(0, 0, 0, 0)
    }
    to {
        box-shadow: 0 0 0 0 rgba(0, 0, 0, 0)
    }
}

</style>
