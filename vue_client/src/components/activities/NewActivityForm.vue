<template>
    <div class="new-activity-form-container">
        <h2>CREATE NEW ACTIVITY</h2>
        <div class="new-activity-form">
            <!-- name -->
            <TextInputWithHeadline
                  v-model="activityName"
                  class="activity-name"
                  title="NAME"
            />
            <!-- duration -->
            <div
                  class="duration-container"
                  @change="checkDurationInput"
            >
                <NumberInputWithHeadline
                      v-model="durationMin"
                      :title="getDurationInputText()"
                />
                <div class="duration-flexible-checkbox">
                    <input
                          v-model="flexibleDuration"
                          type="checkbox"
                    >
                    <p>flexible?</p>
                </div>

                <NumberInputWithHeadline
                      v-if="flexibleDuration"
                      v-model="durationMax"
                      title="MAXIMUM DURATION"
                />
            </div>
            <!-- weekday constraint -->
            <WeekdayConstraintInput
                  v-model:weekdayConstraints="weekdayConstraints"
            />
            <RecurringInput
                  v-model="recurringInput"
                  class="recurring-input"
            />
            <button
                  class="submit-button button is-primary"
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
<style lang="less" scoped>
@import "@/css/measures.less";

.new-activity-form-container {
    margin-left: 2vw;
    margin-right: 2vw;

    .activity-name {
        margin-bottom: 1vw;
    }

    .duration-container {
        display: flex;
        justify-content: flex-start;
        align-items: center;
        margin-bottom: 1vw;
        width: 100%;

        * {
            width: 45.5%;
        }

        *:not(:last-child) {
            margin-right: 2vw;
        }

        .duration-flexible-checkbox {
            width: 5vw;

            p, input {
                width: 5vw;
            }
        }
    }

    .recurring-input {
        margin-top: 2vw;
    }

    .submit-button {
        width: 100%;
        height: 4vw;
        border-radius: @default-border-radius;
        border: 1.5px solid #dbdbdb;
        cursor: pointer;
    }
}
</style>
