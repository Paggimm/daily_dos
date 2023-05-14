<template>
    <div class="input-with-headline flex flex-col h-[5vw]">
        <p class="border-[1.5] border-b-0 border-s-black text-center bg-sky-300 m-0 h-[60%] rounded-t-md rounded-b-0">
            {{ title }}
        </p>
        <Calendar
            v-model="value"
            :manual-input="false"
            :show-time="true"
            class="input bg-white h-[60%] p-0"
            date-format="dd.mm.yy"
            selection-mode="single"
        />
    </div>
</template>
<script lang="ts" setup>
import {defineEmits, defineProps, ref, watch} from 'vue';
import Calendar from "primevue/calendar";

const props = defineProps<{
    title: string,
    modelValue: Date,
}>();

const emit = defineEmits([
    'update:modelValue'
]);

const value = ref<Date>(new Date());
watch(value, (value) => {
    emit('update:modelValue', value);
});

</script>

<style scoped>
.input:deep(.p-inputtext) {
  border-radius: 0 0 5px 5px;
}
</style>
