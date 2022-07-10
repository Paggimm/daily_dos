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
import { ref, defineProps, defineEmits } from 'vue';

defineProps({
    weekdayConstraints: {
        type: Array,
        required: true,
    }
});
const emits = defineEmits(['update:weekdayConstraints']);

const weekday_list = ref(['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']);
const weekday_constraints = ref<string[]>(['0','0','0','0','0','0','0']);

const updateWeekdayConstraints = () => {
    emits('update:weekdayConstraints', weekday_constraints.value)
}

function isWeekdaySelected(index: number): boolean {
    return weekday_constraints.value[index] !== '0';
}

function toggleWeekday(index: number) {
    console.log('toggle');
    if(weekday_constraints.value[index] === '0') {
        weekday_constraints.value[index] = '1';
    }
    else {
        weekday_constraints.value[index] = '0'
    }
}

</script>
<style scoped>

.weekday-constraint-input-container {
    display: flex;
}

.weekday-constraint-input-item{
    display: flex;
    aspect-ratio: 1;
    width: 16%;
    border-radius: 10px;
    background-color: bisque;
    justify-content: center;
    align-items: center;
}

.weekday-constraint-input-item:not(:last-child) {
    margin-right: 2%;
}

.selected {
    background-color: blueviolet;
}

</style>
