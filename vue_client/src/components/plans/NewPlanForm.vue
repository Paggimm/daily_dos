<template>
    <div class="new-plans-container">
        <h3>NEW PLAN</h3>
        <!-- FORM FIELDS -->
        <div class="form-fields-container ml-[5vw] mr-[5vw] grid justify-center gap-[2vw]">
            <div class="form-fields-input-container flex gap-[2vw] items-center">
                <ActivitySelection
                    v-model:selected-activity="activity"
                    :activity-list="activityList"
                    class="form-field-activity h-[20vw] w-[20vw] col-start-1 col-end-1 row-span-full"
                />
                <div class="form-fields-inputs col-start-2 col-end-2 flex gap-[2vw] w-[40vw] flex-col row-span-full">
                    <DateWithHeadline
                        v-model="date"
                        title="DATE"
                    />
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
                class="submit-button pulsating-on-hover h-[4vw] w-[62vw] rounded-md cursor-pointer bg-sky-200"
                @click="submit"
            >
                SUBMIT
            </button>
        </div>
    </div>
</template>
<script lang="ts" setup>
import {onMounted, ref} from "vue";
import type {Activity, PlanInput} from "@/generated/models";
import {useAuthStore} from "@/store/AuthStore";
import {fetchRequest} from "@/utils";
import ActivitySelection from "@/components/activities/ActivitySelection.vue";
import NumberInputWithHeadline from "@/components/form-components/NumberInputWithHeadline.vue";
import RecurringInput from "@/components/form-components/RecurringInput.vue";
import type {IRecurringInput} from "@/types";
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
