<template>
    <div class="new-plans-container">
        <h3>NEW PLAN</h3>
        <!-- FORM FIELDS -->
        <ActivitySelection
              v-model:selected-activity="activity"
              :activity-list="activityList"
        />
        <Calendar
              v-model="date"
              :manualInput="false"
              :showTime="true"
              dateFormat="dd.mm.yy"
              selectionMode="single"
        />
        <NumberInputWithHeadline
              v-model="duration"
              title="DURATION"
        />
        <RecurringInput
              v-model="recurringInput"
        />
        <button
              class="submit-button button is-primary pulsating-on-hover"
              @click="submit"
        >
            SUBMIT
        </button>
    </div>
</template>
<script lang="ts" setup>
import {onMounted, ref} from "vue";
import {Activity, PlanInput} from "@/generated/models";
import {useAuthStore} from "@/store/AuthStore";
import {fetchRequest} from "@/utils";
import ActivitySelection from "@/components/activities/ActivitySelection.vue";
import Calendar from "primevue/calendar";
import NumberInputWithHeadline from "@/components/form-components/NumberInputWithHeadline.vue";
import RecurringInput from "@/components/form-components/RecurringInput.vue";
import {IRecurringInput} from "@/types";
import {RecurringType} from "@/enums/RecurringType";

const authStore = useAuthStore();

const activityList = ref<Activity[]>([]);
const activity = ref<Activity | undefined>(undefined);
const date = ref<Date>();
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
    console.log("clicked submit")
    const planInput: PlanInput = {
        activityId: activity.value!.id,
        date: date.value!,
        duration: duration.value,
        repeatable: recurringInput.value.recurringType.toString()
    }

    const response = await fetchRequest('plan', JSON.stringify(planInput), 'POST', authStore.getToken);
}

</script>
<style lang="less" scoped>
@import "@/css/measures.less";

.submit-button {
    width: 100%;
    height: 4vw;
    border-radius: @default-border-radius;
    border: 1.5px solid #dbdbdb;
    cursor: pointer;
}
</style>
