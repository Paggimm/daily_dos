<template>
    <div class="new-activity-form-container ml-[2vw] mr-[2vw]">
        <h2>CREATE NEW ACTIVITY</h2>
        <div class="new-activity-form">
            <!-- name -->
            <TextInputWithHeadline
                v-model="activityName"
                class="mb-[1vw]"
                title="NAME"
            />
            <!-- duration -->
            <div
                class="duration-container flex justify-start items-center mb-[1vw] w-full"
                @change="checkDurationInput"
            >
                <NumberInputWithHeadline
                    v-model="durationMin"
                    :title="getDurationInputText()"
                    class="w-[45.5%] mr-[2vw]"
                />
                <div class="duration-flexible-checkbox mr-[2vw] w-[5vw]">
                    <input
                        class="w-[5vw]"
                        v-model="flexibleDuration"
                        type="checkbox"
                    >
                    <p class="w-[5vw]">flexible?</p>
                </div>

                <NumberInputWithHeadline
                    v-if="flexibleDuration"
                    v-model="durationMax"
                    title="MAXIMUM DURATION"
                    class="w-[45.5%]"
                />
            </div>
            <!-- weekday constraint -->
            <WeekdayConstraintInput
                v-model:weekdayConstraints="weekdayConstraints"
            />
            <RecurringInput
                v-model="recurringInput"
                class="recurring-input mt-[2vw]"
            />
            <button
                class="submit cursor-pointer border-s-black border-[1.5] w-full h-[4vw] rounded-sm bg-sky-200"
                @click="submit"
            >
                SUBMIT
            </button>
        </div>
    </div>
</template>

<script lang="ts" setup>
import {ref} from 'vue';
import NumberInputWithHeadline from '@/components/form-components/NumberInputWithHeadline.vue'
import TextInputWithHeadline from '@/components/form-components/TextInputWithHeadline.vue'
import WeekdayConstraintInput from '@/components/form-components/WeekdayConstraintInput.vue'
import {RecurringType} from '@/enums/RecurringType'
import {fetchRequest} from '@/utils'
import {useAuthStore} from '@/store/AuthStore'
import type {ActivityInput} from "@/generated/models";
import {validateActivityInput} from "@/validators/ActivityInputValidator";
import type {IRecurringInput} from "@/types";
import RecurringInput from "@/components/form-components/RecurringInput.vue";

const authStore = useAuthStore();

const activityName = ref('')
const weekdayConstraints = ref<string[]>(['0', '0', '0', '0', '0', '0', '0']);

const recurringInput = ref<IRecurringInput>({
    recurringType: RecurringType.NO,
    recurringInterval: 1
});

const flexibleDuration = ref(false);
const durationMin = ref<number>(1);
const durationMax = ref<number>(2);

function checkDurationInput() {
    if (flexibleDuration.value) {
        // when max lower equals min we change max to min+1
        if (durationMax.value <= durationMin.value) {
            durationMax.value = durationMin.value + 1;
        }

        // max is never lower than 2
        if (durationMax.value < 2) {
            durationMax.value = 2;
        }
    }

    if (durationMin.value < 1) {
        durationMin.value = 1;
    }
}

function getDurationInputText() {
    return flexibleDuration.value ? 'MINIMUM DURATION' : 'DURATION';
}

async function submit() {
    const weekdays = weekdayConstraints.value.join('');

    const activityInput: ActivityInput = {
        name: activityName.value,
        minDuration: durationMin.value,
        maxDuration: durationMax.value,
        recurringType: recurringInput.value.recurringType,
        recurringInterval: recurringInput.value.recurringInterval,
        weekdayConstraint: weekdays,
    }

    if (validateActivityInput(activityInput)) {
        await fetchRequest('activity', JSON.stringify(activityInput), 'POST', authStore.getToken);
    } else {
        throw new Error("invalid input")
    }
}

</script>
