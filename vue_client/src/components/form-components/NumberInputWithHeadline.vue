<template>
    <div class="input-with-headline flex flex-col h-[5vw]">
        <p class="title border-[1.5] border-b-0 border-s-[#dbdbdb] rounded-t-md text-center bg-sky-300 m-0 h-[40%]">
            {{ title }}
        </p>
        <input
            :value="modelValue"
            class="input bg-white rounded-b-md h-3/5 p-2 border-s-[#dbdbdb] pl-[1%]"
            type="number"
            @input="updateValue"
        >
    </div>
</template>
<script lang="ts" setup>
import {defineProps, defineEmits} from 'vue';

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
    if (value < props.minValue) {
        input.value = String(props.minValue);
    } else if (value > props.maxValue) {
        input.value = String(props.maxValue);
    }
}

</script>
