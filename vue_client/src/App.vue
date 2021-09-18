<template>
  <div>
    <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/login">Login</router-link> |
      <router-link to="/about">About</router-link>
    </div>
    <div class="block">
      Server:
      <span
        :class="online ? 'has-text-success' : 'has-text-danger'"
        v-text="online ? 'online' : 'offline'"
      />
    </div>
    <router-view @got_token="gotToken" />
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
import { fetchWithTimeout } from "./utils";

// TODO: generate? Move into own file?
interface PingResponse {
  online?: boolean | unknown;
}

export default class App extends Vue {
  protected online = false;
  protected token: string | undefined = undefined;
  protected intervalPid: number | undefined = undefined;

  public async created(): Promise<void> {
    await this.ping();
  }

  public async mounted(): Promise<void> {
    // For Hotreload: Delete old Interval before we add a new one
    clearInterval(this.intervalPid);
    this.intervalPid = setInterval(async () => {
      console.log("Trigger ping");
      await this.ping();
    }, 10000);
  }

  protected gotToken(token: string): void {
    this.token = token;
  }

  protected async ping(): Promise<void> {
    const headers = new Headers();
    headers.append("pragma", "no-cache");
    headers.append("cache-control", "no-cache");
    headers.append("Authorization", `Bearer ${this.token}`);

    const myInit = {
      method: "GET",
      headers: headers,
    };

    let result: boolean;
    try {
      const response = await fetchWithTimeout(
        "http://localhost:8085/ping",
        myInit,
        1000
      );
      const body: PingResponse = await response.json();
      result = body.online === true;
      console.log("Update!");
    } catch (e) {
      console.log("Timeout!");
      result = false;
    }
    this.online = result;
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
