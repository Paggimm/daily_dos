<template>
    <h2>RECENT PLANS</h2>
    <PlanInfoCard
        v-for="(plan, index) in plans"
        :key="index"
        :plan="plan"
    />
    <NewPlanForm />
</template>

<script lang="ts" setup>
import NewPlanForm from "@/components/plans/NewPlanForm.vue";
import type {Plan} from "@/generated/models";
import {onMounted, ref} from "vue";
import {useAuthStore} from "@/store/AuthStore";
import {fetchRequest} from "@/utils";
import PlanInfoCard from "@/components/plans/PlanInfoCard.vue";

const authStore = useAuthStore();

const plans = ref<Plan[]>([]);

onMounted(async () => {
    plans.value = await fetchRequest("plan", undefined, "GET", authStore.getToken).then(async (response) => {
        const content = (await response.json()) as Plan[];
        const results = content.map((plan) => {
            // we actually only got a string-representation of a date and not a valid date-object
            plan.date = new Date(plan.date);
            return plan;
        });
        return results;
    });
});

</script>
