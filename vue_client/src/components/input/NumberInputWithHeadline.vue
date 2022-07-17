<template>
  <div class="input-with-headline">
    <p> {{ title }}</p>
    <input
      :value="modelValue"
      class="input"
      type="number"
      @input="updateValue"
    >
  </div>
</template>
<script lang="ts" setup>
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
    title: {
        type: String,
        required: true,
    },
    modelValue: {
        type: Number,
        required: true,
    },
    maxValue: {
      type: Number,
      default: 100000000,
    },
    minValue: {
      type: Number,
      default: 0,
    },
});

const emit = defineEmits([
'update:modelValue'
]);

// validates and emits the input
const updateValue = (event: Event) => {
  const event_target = event.target as HTMLInputElement;
  validateInput(event_target);
  const value = Number(event_target.value);
  emit('update:modelValue', value);
};

// checks if the input value is within the allowed bounds and corrects it
function validateInput(input: HTMLInputElement) {
  const value = Number(input.value);
  if(value < props.minValue) {
    input.value = String(props.minValue);
  } else if (value > props.maxValue) {
    input.value = String(props.maxValue);
  }
}

</script>
<style scoped>
.input-with-headline {
  display: flex;
  flex-direction: column;
}

.input-with-headline > p {
  border-top: 1.5px solid #dbdbdb;
  border-left: 1.5px solid #dbdbdb;
  border-right: 1.5px solid #dbdbdb;
  border-radius: 5px 5px 0 0;
  text-align: center;
  font-size: x-small;
  background-color: var(--background-information-surface);
}

.input-with-headline > input {
  background-color: var(--background-interactable-surface);
  border-radius: 0 0 5px 5px;
}
</style>
