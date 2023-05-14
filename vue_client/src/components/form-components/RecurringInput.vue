<template>
    <div
        class="recurring-form-container flex mb-1"
    >
        <Dropdown
            v-model="recurringInput.recurringType"
            class="w-1/2"
            :option-list="availableRecurringTypes"
            title="Recurring Type"
        />
        <NumberInputWithHeadline
            v-show="recurring"
            v-model="recurringInput.recurringInterval"
            class="interval-input"
            :max-value="29"
            :min-value="1"
            title="RECURRING INTERVAL"
        />
    </div>
</template>

<script lang="ts" setup>
import {computed, defineEmits, defineProps, toRef, watch} from "vue";
import {createAvailableRecurringTypes, RecurringType} from "@/enums/RecurringType";
import NumberInputWithHeadline from "@/components/form-components/NumberInputWithHeadline.vue"
import Dropdown from "@/components/form-components/Dropdown.vue"
import type {IRecurringInput} from "@/types";

const props = defineProps<{
    modelValue: IRecurringInput
}>();
const emit = defineEmits(['update:modelValue'])

const recurring = computed(() => {
    return recurringInput.value.recurringType !== RecurringType.NO
});
const recurringInput = toRef(props, "modelValue");
const availableRecurringTypes = createAvailableRecurringTypes();

watch(recurringInput, () => {
    emit('update:modelValue', recurringInput)
});

</script>

<style scoped>
.recurring-form-container > *:not(:last-child) {
    margin-right: 2%;
}
</style>
