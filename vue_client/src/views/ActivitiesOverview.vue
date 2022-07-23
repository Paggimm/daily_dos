<template>
  <div class="activities-overview-container">
    <RecentActivityList :activity-list="activityList"/>
    <NewActivityForm />
  </div>
</template>
<script setup lang="ts">
import { Activity } from '@/generated/models'
import { useAuthStore } from '@/store/AuthStore'
import { fetchRequest } from '@/utils'
import { onMounted, ref } from 'vue';
import NewActivityForm from '@/components/activities/NewActivityForm.vue';
import RecentActivityList from "@/components/activities/RecentActivityList.vue";

    const activityList = ref<Activity[]>([]);
    const authStore = useAuthStore();

    onMounted(async () => {
        const response = await fetchRequest('activity', undefined, 'GET', authStore.getToken);
        if(response.status === 200) {
            activityList.value = await response.json();
        }
    });

</script>
<style scoped>

</style>
