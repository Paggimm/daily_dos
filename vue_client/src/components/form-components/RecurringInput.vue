<template>
    <div class="recurring-container">
        <div class="recurring-checkbox">
            <input
                  v-model="recurring"
                  type="checkbox"
            >
            <p>recurring Activity?</p>
        </div>
        <div
              v-if="recurring"
              class="recurring-form-container"
        >
            <Dropdown
                  v-model="recurringInput.recurringType"
                  :option-list="availableRecurringTypes"
                  title="Recurring Type"
            />
            <NumberInputWithHeadline
                  v-model="recurringInput.recurringInterval"
                  :max-value="29"
                  :min-value="1"
                  title="RECURRING INTERVAL"
            />
        </div>
    </div>
</template>

<script lang="ts" setup>
import {defineEmits, defineProps, ref, toRef, watch} from "vue";
import {createAvailableRecurringTypes} from "@/enums/RecurringType";
import NumberInputWithHeadline from "@/components/form-components/NumberInputWithHeadline"
import Dropdown from "@/components/form-components/Dropdown"
import {IRecurringInput} from "@/types";

const props = defineProps<{
    modelValue: IRecurringInput
}>();
const emit = defineEmits(['update:modelValue'])

const recurring = ref(false);
const recurringInput = toRef(props, "modelValue");
const availableRecurringTypes = createAvailableRecurringTypes();

watch(recurringInput, () => {
    emit('update:modelValue', recurringInput)
});

</script>

<style lang="less" scoped>
.recurring-form-container {
    display: flex;
    margin-bottom: 1vw;

    * {
        width: 49%;
    }

    & > *:not(:last-child) {
        margin-right: 2%;
    }
}

.recurring-checkbox {
    display: flex;
    justify-content: center;
    align-items: center;

    * {
        margin-right: 0.5vw;
    }
}
</style>
