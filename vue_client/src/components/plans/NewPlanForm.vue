<template>
    <div class="new-plans-container">
        <h3>NEW PLAN</h3>
        <!-- FORM FIELDS -->
        <ActivitySelection
              :activity-list="activityList"
        />
        <!--
            activity dropdown
            date
            duration
            repeatable
        -->
    </div>
</template>
<script setup lang="ts">
import {onMounted, ref} from "vue";
import {Activity} from "@/generated/models";
import {useAuthStore} from "@/store/AuthStore";
import {fetchRequest} from "@/utils";
import ActivitySelection from "@/components/activities/ActivitySelection.vue";

const authStore = useAuthStore();

// activityList and computed OptionList for Dropdown
const activityList = ref<Activity[]>([]);
// load activityList
onMounted(async () => {
    const response = await fetchRequest('activity', undefined, 'GET', authStore.getToken);
    if (response.status === 200) {
        activityList.value = await response.json();
    }
});

</script>
<style scoped lang="less">

</style>
