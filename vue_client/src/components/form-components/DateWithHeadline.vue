<template>
    <div class="input-with-headline">
        <p> {{ title }}</p>
        <Calendar
              v-model="value"
              :manualInput="false"
              :showTime="true"
              class="input"
              dateFormat="dd.mm.yy"
              selectionMode="single"
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

<style lang="less" scoped>
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

    .input {
        background-color: @background-interactable-surface;
        height: 60%;
        padding: 0;

        // to overwrite primevue border-radius
        &:deep(.p-inputtext) {
            border-radius: 0 0 5px 5px;
        }
    }
}
</style>
