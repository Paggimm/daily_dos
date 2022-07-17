<template>
  <div class="new-activity-form-container">
    <h2>CREATE NEW ACTIVITY</h2>
    <div class="new-acitivty-form">
      <!-- name -->
      <TextInputWithHeadline
        v-model="activityName"
        class="block"
        title="NAME"
      />
      <!-- duration -->
      <div
        class="duration-container block"
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
        class="recurring-form-container block"
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
import { ref } from 'vue';
import NumberInputWithHeadline from '../input/NumberInputWithHeadline.vue'
import TextInputWithHeadline from '../input/TextInputWithHeadline.vue'
import WeekdayConstraintInput from '../input/WeekdayConstraintInput.vue'
import { createAvailableRecurringTypes, RecurringType } from '@/enums/RecurringType'
import Dropdown from '../input/Dropdown.vue'
import { fetchRequest } from '@/utils'
import { useAuthStore } from '@/store/AuthStore'

const recurring = ref(true);
const flexibleDuration = ref(false);
const authStore = useAuthStore();

// form values
const durationMin = ref<number>(1);
const durationMax = ref<number>(2);
const activityName = ref('')
const weekdayConstraints = ref<string[]>(['0','0','0','0','0','0','0']);
const recurringType = ref<RecurringType>(RecurringType.DAILY)
const recurringInterval = ref(1);

const availableRecurringTypes = createAvailableRecurringTypes();

function checkDurationInput(){
    if(flexibleDuration.value) {
        // when max lower equals min we change max to min+1
        if(durationMax.value <= durationMin.value) {
            durationMax.value = durationMin.value+1;
        }

        // max is never lower than 2
        if(durationMax.value < 2) {
            durationMax.value = 2;
        }
    }

    if(durationMin.value < 1) {
        durationMin.value = 1;
    }
}

function getDurationInputText(){
    return flexibleDuration.value?'MINIMUM DURATION':'DURATION';
}

async function submit() {
    const weekdays = weekdayConstraints.value.join('');
    const requestBody = {
        name: activityName.value,
        minDuration: durationMin.value,
        maxDuration: durationMax.value,
        recurringType: recurringType.value,
        recurringInterval: recurringInterval.value,
        weekdayConstraint: weekdays,
    }

    await fetchRequest('activity', JSON.stringify(requestBody), 'POST', authStore.getToken);
}

</script>
<style scoped>
.new-activity-form-container {
    margin-left: 2vw;
    margin-right: 2vw;
}

.duration-container {
    display: flex;
    justify-content: flex-start;
    align-items: center;
}

.duration-container > * {
    width: 35.5vw;
}

.duration-container > .duration-flexible-checkbox {
    width: 5vw;
}

.duration-container > *:not(:last-child) {
    margin-right: 2vw;
}

.recurring-form-container {
    display: flex;
}

.recurring-form-container > * {
    width: 48%;
}

.recurring-form-container > *:not(:last-child) {
    margin-right: 2%;
}

.recurring-checkbox {
        display: flex;
        justify-content: center;
        align-items: center;
    }

.recurring-checkbox > * {
    margin-right: 0.5vw;
}

.submit-button {
    width: 100%;
}

</style>
