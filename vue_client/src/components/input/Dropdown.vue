<template>
    <div
          v-click-outside="closeDropdown"
          class="dropdown-container"
    >
        <!-- Selected Value -->
        <div
              class="dropdown-selected-value"
              :class="{'closed': !showSelectionOptions}"
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
import {ref, defineProps, defineEmits, PropType} from 'vue';

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
    if (showSelectionOptions.value === true) {
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
<style scoped lang="less">

.dropdown-container {
    position: relative;
    width: 100%;
    border-radius: 10px;

    .dropdown-selected-value {
        background-color: var(--background-interactable-surface);
        display: flex;
        padding-left: 1vw;
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
        height: 100%;
        justify-content: center;
        align-items: center;
        border: 1px solid #dbdbdb;

        &:hover {
            cursor: pointer;
        }
    }

    .dropdown-selected-value.closed {
        border-radius: 10px;
    }

    .dropdown-option-container {
        display: flex;
        flex-direction: column;
        position: absolute;
        width: 100%;
        align-items: flex-start;
        z-index: 20;

        &:hover {
            cursor: pointer;
        }

        .dropdown-option {
            background-color: var(--background-interactable-surface);
            width: 100%;
            border: 1px solid #dbdbdb;

            &:hover {
                background-color: var(--background-interactable-surface-focused);
            }
        }
    }
}

</style>
