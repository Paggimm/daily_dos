<template>
  <div class="activities-overview-container">
    <h2>
      RECENT ACTIVITIES
    </h2>

    <!-- last Activities -->
    <div class="existing-activity-container">
      <div
        v-for="(activity, index) in activity_list"
        :key="index"
        class="existing-activity"
      >
        <p>{{ activity.name }}</p>
      </div>
    </div>
  </div>
  <NewActivityForm />
</template>

<script setup lang="ts">
import { Activity } from '@/generated/models'
import { useAuthStore } from '@/store/AuthStore'
import { fetchRequest } from '@/utils'
import { onMounted, ref } from 'vue';
import NewActivityForm from '@/components/activities/NewActivityForm.vue';

    const activity_list = ref<Activity[]>([]);
    const authStore = useAuthStore();

    onMounted(async () => {
        const response = await fetchRequest('activity', undefined, 'GET', authStore.getToken);
        if(response.status === 200) {
            activity_list.value = await response.json();
        }
    });

</script>

<style scoped>
    .existing-activity-container {
        display: flex;
        margin-left: auto;
        margin-right: auto;
        flex-wrap: wrap;
        justify-content: flex-start;
        padding-left: 12vw;

    }

    .existing-activity-container > .existing-activity {
        border-radius: 10px;
        margin: 1vw;
        aspect-ratio: 5;
        width: 40%;
        background-color: aquamarine;
        justify-content: center;
        display: flex;
        align-items: center;
    }
</style>
