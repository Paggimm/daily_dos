<template>
  <div class="server-status-container">
    Server:
    <span
      :class="online ? 'has-text-success' : 'has-text-danger'"
      v-text="online ? 'online' : 'offline'"
    />
    | Authenticated:
    <span
      :class="authenticated ? 'has-text-success' : 'has-text-danger'"
      v-text="authenticated ? 'yes' : 'no'"
    />
  </div>
</template>

<script setup lang="ts">
import { VuexHandler } from "../../store/store";
import { fetchRequest } from "./../../utils";
import { onMounted, ref } from "vue";

// TODO: generate? Move into own file?
interface PingResponse {
  online?: boolean | unknown;
}

const intervalPid = ref<number>();
const online = ref(false);
const authenticated = ref(false);
const vuexHandler = new VuexHandler();

async function ping(check_auth: boolean): Promise<void> {
  let result: boolean;
  try {
    const response = await fetchRequest(
      check_auth ? "authping" : "ping",
      undefined,
      "GET",
      vuexHandler.state.token
    );
    if (response.status === 200) {
      const body: PingResponse = await response.json();
      result = body.online === true;
    } else {
      result = false;
    }
  } catch (e) {
    console.warn(e);
    result = false;
  }

  if (check_auth) {
    authenticated.value = result;
  } else {
    online.value = result;
  }
}

void ping(false);
void ping(true);

onMounted(() => {
  // For Hotreload: Delete old Interval before we add a new one
  clearInterval(intervalPid.value);
  intervalPid.value = setInterval(async () => {
    void ping(false);
    void ping(true);
  }, 10000);
})
</script>
