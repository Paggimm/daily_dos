<template>
    <div class="new-plans-container">
        <h3>NEW PLAN</h3>
        <!-- FORM FIELDS -->
        <div class="form-fields-container">
            <div class="form-fields-input-container">
                <ActivitySelection
                      v-model:selected-activity="activity"
                      :activity-list="activityList"
                      class="form-field-activity"
                />
                <div class="form-fields-inputs">
                    <DateWithHeadline
                          v-model="date"
                          title="DATE"/>
                    <NumberInputWithHeadline
                          v-model="duration"
                          class="form-field-duration"
                          title="DURATION"
                    />
                    <RecurringInput
                          v-model="recurringInput"
                          class="form-field-recurring"
                    />
                </div>
            </div>
            <button
                  class="submit-button button is-primary pulsating-on-hover"
                  @click="submit"
            >
                SUBMIT
            </button>
        </div>
    </div>
</template>
<script lang="ts" setup>
import {onMounted, ref} from "vue";
import {Activity, PlanInput} from "@/generated/models";
import {useAuthStore} from "@/store/AuthStore";
import {fetchRequest} from "@/utils";
import ActivitySelection from "@/components/activities/ActivitySelection.vue";
import NumberInputWithHeadline from "@/components/form-components/NumberInputWithHeadline.vue";
import RecurringInput from "@/components/form-components/RecurringInput.vue";
import {IRecurringInput} from "@/types";
import {RecurringType} from "@/enums/RecurringType";
import DateWithHeadline from "@/components/form-components/DateWithHeadline.vue";
import {ValidatePlanInput} from "@/validators/PlanInputValidator";

const authStore = useAuthStore();

const activityList = ref<Activity[]>([]);
const activity = ref<Activity | undefined>(undefined);
const date = ref<Date>(new Date());
const duration = ref<number>(1);
const recurringInput = ref<IRecurringInput>({
    recurringInterval: 1,
    recurringType: RecurringType.NO
});

// load activityList
onMounted(async () => {
    const response = await fetchRequest('activity', undefined, 'GET', authStore.getToken);
    if (response.status === 200) {
        activityList.value = await response.json();
    }
});

async function submit() {
    const planInput: PlanInput = {
        activityId: activity.value!.id,
        date: date.value!,
        duration: duration.value,
        repeatable: recurringInput.value.recurringType.toString()
    }

    if (ValidatePlanInput(planInput)) {
        const response = await fetchRequest('plan', JSON.stringify(planInput), 'POST', authStore.getToken);
    } else {
        // TODO: react to false input
    }

}

</script>
<style lang="less" scoped>
@import "@/css/measures.less";

.submit-button {
    height: 4vw;
    border-radius: @default-border-radius;
    border: 1.5px solid #dbdbdb;
    cursor: pointer;
}

.form-fields-container {
    margin-left: 5vw;
    margin-right: 5vw;
    display: flex;
    gap: 2vw;
    flex-direction: column;

    .form-fields-input-container {
        display: flex;
        gap: 2vw;
        align-items: center;

        .form-field-activity {
            height: 20vw;
            width: 20vw;
        }

        .form-fields-inputs {
            display: flex;
            gap: 2vw;
            width: 40vw;
            flex-direction: column;
        }
    }
}
</style>
