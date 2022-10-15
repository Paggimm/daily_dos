<template>
    <div
          @v-click-outside="closeDropdown"
          class="dropdown-container"
    >
        <p class="title"> {{ title }}</p>
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
    },
    title: {
        type: String,
        default: "",
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
@import "@/css/colors.less";

.dropdown-container {
    position: relative;
    width: 100%;
    border-radius: 10px;

    .title {
        border-top: 1.5px solid #dbdbdb;
        border-left: 1.5px solid #dbdbdb;
        border-right: 1.5px solid #dbdbdb;
        border-radius: 5px 5px 0 0;
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
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
        height: 60%;
        justify-content: center;
        align-items: center;
        border: 1px solid #dbdbdb;

        &:hover {
            cursor: pointer;
        }
    }

    .dropdown-selected-value.closed {
        border-radius: 0 0 10px 10px;
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
