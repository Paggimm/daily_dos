<template>
    <div
        class="weekday-constraint-input-container flex"
        @click="updateWeekdayConstraints"
    >
        <div
            v-for="(weekday, index) in weekday_list"
            :key="index"
            :class="{'selected':isWeekdaySelected(index)}"
            class="weekday-constraint-input-item pulsating-on-hover flex aspect-square w-[16%] rounded-lg bg-white items-center place-content-center border-s-black border-1 cursor-pointer"
            @click="toggleWeekday(index)"
        >
            <p>{{ weekday }}</p>
        </div>
    </div>
</template>
<script lang="ts" setup>
import {defineEmits, defineProps, ref} from 'vue';

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
<style scoped>
.weekday-constraint-input-item:hover {
    background-color: #bae6fd;
}

.weekday-constraint-input-item:not(:last-child) {
    margin-right: 2%;
}

.selected {
    background-color: #bae6fd;
}
</style>
