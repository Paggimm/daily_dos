<template>
  <div class="input-with-headline">
    <p> {{ title }}</p>
    <input
      :value="modelValue"
      class="input"
      type="text"
      @input="updateValue"
    >
  </div>
</template>
<script lang="ts" setup>
import { defineProps, defineEmits } from 'vue';

defineProps({
    title: {
        type: String,
        required: true,
    },
    modelValue: {
        type: String,
        required: true,
    },
});

const emit = defineEmits([
'update:modelValue'
]);

const updateValue = (event: Event) => {
  const event_target = event.target as HTMLInputElement;
  const value = event_target.value;
  emit('update:modelValue', value);
};

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
