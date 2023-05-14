<template>
    <div class="input-with-headline flex flex-col h-[5vw]">
        <p class="title border-[1.5] border-b-0 border-s-[#dbdbdb] rounded-t-md text-center bg-sky-300 m-0 h-[40%]">
            {{ title }}
        </p>
        <input
            :value="modelValue"
            class="input bg-white rounded-b-md h-3/5 p-2 border-s-[#dbdbdb] pl-[1%]"
            type="text"
            @input="updateValue"
        >
    </div>
</template>
<script lang="ts" setup>
import {defineProps, defineEmits} from 'vue';

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
