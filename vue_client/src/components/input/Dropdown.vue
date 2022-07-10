<template>
  <div
    v-click-outside="closeDropdown"
    class="dropdown-container"
  >
    <!-- Selected Value -->
    <div
      class="dropdown-selected-value"
      @click="toogleDropdown"
    >
      <p> {{ selectionValue }}</p>
    </div>
    <!-- Selectable Options -->
    <div
      v-if="showSelectionOptions"
      class="dropdown-option-container"
    >
      <div
        v-for="(option, index) in optionList"
        :key="index"
        class="dropdown-option"
        @click="optionClicked(index)"
      >
        <p>{{ option }}</p>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { ref, defineProps, defineEmits, PropType } from 'vue';

const props = defineProps({
    optionList: {
        type: Array as PropType<Array<string>>,
        required: true,
    },
    modelValue: {
        type: String,
        required: true,
    }
})

const emit = defineEmits(['update:modelValue'])

const showSelectionOptions = ref(false);
const selectionValue = ref<string>('...');

function openDropdown() {
    showSelectionOptions.value = true;
}

function closeDropdown() {
    showSelectionOptions.value = false;
}

function toogleDropdown() {
    if(showSelectionOptions.value === true) {
        closeDropdown();
    } else {
        openDropdown();
    }
}

function optionClicked(index: number) {
    selectionValue.value = props.optionList[index];
    closeDropdown();
    updateModelValue();
}

function updateModelValue() {
    emit('update:modelValue', selectionValue)
}

</script>
<style scoped>

.dropdown-container {
    position: relative;
    width: 100%;
    border-radius: 10px;
}

.dropdown-selected-value {
    background-color: brown;
    display: flex;
    align-items: flex-start;
    padding-left: 1vw;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
}
.dropdown-option-container {
    display: flex;
    flex-direction: column;
    position: absolute;
    width: 100%;
    align-items: flex-start;
}

.dropdown-option {
    background-color: rgb(170, 170, 170);
    width: 100%;
}

</style>
