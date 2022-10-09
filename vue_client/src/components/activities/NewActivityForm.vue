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
            <!-- recurring -->
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
                      v-model="recurringType"
                      :option-list="availableRecurringTypes"
                />
                <NumberInputWithHeadline
                      :title="'RECURRING INTERVAL'"
                      :model-value="recurringInterval"
                      :max-value="29"
                      :min-value="1"
                />
            </div>
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
import NumberInputWithHeadline from '../input/NumberInputWithHeadline.vue'
import TextInputWithHeadline from '../input/TextInputWithHeadline.vue'
import WeekdayConstraintInput from '../input/WeekdayConstraintInput.vue'
import {createAvailableRecurringTypes, RecurringType} from '@/enums/RecurringType'
import Dropdown from '../input/Dropdown.vue'
import {fetchRequest} from '@/utils'
import {useAuthStore} from '@/store/AuthStore'
import {ActivityInput} from "@/generated/models";
import {validateActivityInput} from "@/validators/ActivityInputValidator";

const recurring = ref(false);
const flexibleDuration = ref(false);
const authStore = useAuthStore();

// form values
const durationMin = ref<number>(1);
const durationMax = ref<number>(2);
const activityName = ref('')
const weekdayConstraints = ref<string[]>(['0', '0', '0', '0', '0', '0', '0']);
const recurringType = ref<RecurringType>(RecurringType.DAILY)
const recurringInterval = ref(1);

const availableRecurringTypes = createAvailableRecurringTypes();

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
        recurringType: recurring.value ? recurringType.value : RecurringType.NO,
        recurringInterval: recurringInterval.value,
        weekdayConstraint: weekdays,
    }

    if (validateActivityInput(activityInput)) {
        await fetchRequest('activity', JSON.stringify(activityInput), 'POST', authStore.getToken);
    } else {
        throw new Error("invalid input")
    }
}

</script>
<style scoped lang="less">
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

    .submit-button {
        width: 100%;
        height: 4vw;
        border-radius: var(--default-border-radius);
        border: 1.5px solid #dbdbdb;
        cursor: pointer;
    }
}
</style>
