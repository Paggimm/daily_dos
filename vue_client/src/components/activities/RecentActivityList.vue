<template>
    <div class="recent-activities-container">
        <h2>
            RECENT ACTIVITIES
        </h2>

        <!-- last Activities -->
        <div class="existing-activity-container">
            <activity-info-card
                  v-for="(activity, index) in activityList"
                  :key="index"
                  :activity="activity"
                  class="existing-activity"
            >
                <p>{{ activity.name }}</p>
            </activity-info-card>
        </div>
    </div>
</template>
<script setup lang="ts">

import {Activity} from "@/generated/models";
import {PropType, defineProps} from "vue";
import ActivityInfoCard from "@/components/activities/ActivityInfoCard.vue";

defineProps({
    activityList: {
        type: Array as PropType<Array<Activity>>,
        required: true
    }
})

</script>
<style scoped lang="css">
.existing-activity-container {
    display: flex;
    margin-left: auto;
    margin-right: auto;
    flex-wrap: wrap;
    justify-content: flex-start;
    padding-left: 12vw;
}
</style>
