<template>
    <div
        class="dropdown-container relative w-full rounded-md h-[5vw]"
        @v-click-outside="closeDropdown"
    >
        <p class="title border-[1.5] border-b-0 border-s-black rounded-t-md text-center bg-sky-300 m-0 h-[40%]">
            {{ title }}
        </p>
        <!-- Selected Value -->
        <div
            :class="{'closed': !showSelectionOptions}"
            class="dropdown-selected-value cursor-pointer bg-white flex pl-[1vw] rounded-t-md h-3/5 items-center place-content-center border-s-black"
            @click="toogleDropdown"
        >
            <p> {{ selectionValue }}</p>
        </div>
        <!-- Selectable Options -->
        <div
            v-if="showSelectionOptions"
            class="dropdown-option-container flex flex-col absolute w-full items-start z-20 cursor-pointer"
        >
            <div
                v-for="(option, index) in optionList"
                :key="index"
                class="dropdown-option bg-white w-full border-s-[#dbdbdb]"
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
<style scoped>

.closed {
  border-radius: 0 0 5px 5px;
}

.dropdown-option:hover {
  background-color: #D9E7FF;
}

</style>
