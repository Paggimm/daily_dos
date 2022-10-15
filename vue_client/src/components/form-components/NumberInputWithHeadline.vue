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
<style scoped lang="less">
@import "@/css/colors.less";

.input-with-headline {
    display: flex;
    flex-direction: column;
    height: 5vw;

    p {
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

    input {
        background-color: @background-interactable-surface;
        border-radius: 0 0 5px 5px;
        height: 60%;
        padding: 0;
        border: 1.5px solid #dbdbdb;
        padding-left: 1%;
    }
}
</style>
