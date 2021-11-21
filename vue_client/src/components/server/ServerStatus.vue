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

<script lang="ts">
import { VueWithStore } from "./../../store";
import { fetchWithTimeout } from "./../../utils";
import { defineComponent } from "vue";

// TODO: generate? Move into own file?
interface PingResponse {
  online?: boolean | unknown;
}

export default defineComponent({
  name: "ServerStatus",
  data() {
    return {
      online: false,
      authenticated: false,
      intervalPid: undefined as number | undefined,
      vueStore: new VueWithStore(),
    };
  },
  mounted() {
    // For Hotreload: Delete old Interval before we add a new one
    clearInterval(this.intervalPid);
    this.intervalPid = setInterval(async () => {
      console.log("Trigger ping");
      await this.ping(false);
      await this.ping(true);
    }, 10000);
  },
  methods: {
    async created(): Promise<void> {
      await this.ping(false);
      await this.ping(true);
    },
    async ping(check_auth: boolean): Promise<void> {
      const headers = new Headers();
      headers.append("pragma", "no-cache");
      headers.append("cache-control", "no-cache");
      headers.append("Authorization", `Bearer ${this.vueStore.state.token}`);

      const myInit = {
        method: "GET",
        headers: headers,
      };

      let result: boolean;
      try {
        const response = await fetchWithTimeout(
          "http://localhost:8085/" + (check_auth ? "authping" : "ping"),
          myInit,
          1000
        );
        if (response.status === 200) {
          const body: PingResponse = await response.json();
          result = body.online === true;
          console.log("Update!");
        } else {
          result = false;
        }
      } catch (e) {
        console.log("Timeout!");
        result = false;
      }

      if (check_auth) {
        this.authenticated = result;
      } else {
        this.online = result;
      }
    },
  },
});
</script>
