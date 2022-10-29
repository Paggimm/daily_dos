<template>
    <div
          class="dropdown-container"
          @v-click-outside="closeDropdown"
    >
        <p class="title"> {{ title }}</p>
        <!-- Selected Value -->
        <div
              :class="{'closed': !showSelectionOptions}"
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
import {defineEmits, defineProps, ref} from 'vue';

const props = defineProps<{
    optionList: string[],
    modelValue: string,
    title: string,
}>()

const emit = defineEmits(['update:modelValue'])

const showSelectionOptions = ref(false);
const selectionValue = ref<string>(props.modelValue);

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
    if (props.optionList.length >= index) {
        selectionValue.value = props.optionList[index];
        closeDropdown();
        updateModelValue();
    }
}

function updateModelValue() {
    emit('update:modelValue', selectionValue)
}

</script>
<style lang="less" scoped>
@import "@/css/colors.less";
@import "@/css/measures.less";

.dropdown-container {
    position: relative;
    width: 100%;
    border-radius: @default-border-radius;
    height: 5vw;

    .title {
        border-top: 1.5px solid #dbdbdb;
        border-left: 1.5px solid #dbdbdb;
        border-right: 1.5px solid #dbdbdb;
        border-radius: @default-border-radius @default-border-radius 0 0;
        text-align: center;
        font-size: x-small;
        background-color: @background-information-surface;
        margin: 0;
        height: 40%;
    }

    .dropdown-selected-value {
        background-color: @background-interactable-surface;
        display: flex;
        padding-left: 1vw;
        border-top-left-radius: @default-border-radius;
        border-top-right-radius: @default-border-radius;
        height: 60%;
        justify-content: center;
        align-items: center;
        border: 1px solid #dbdbdb;

        &:hover {
            cursor: pointer;
        }
    }

    .dropdown-selected-value.closed {
        border-radius: 0 0 @default-border-radius @default-border-radius;
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
            background-color: @background-interactable-surface;
            width: 100%;
            border: 1px solid #dbdbdb;

            &:hover {
                background-color: @background-interactable-surface-focused;
            }
        }
    }
}

</style>
