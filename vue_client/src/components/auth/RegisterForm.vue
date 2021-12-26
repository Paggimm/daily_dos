<template>
  <div class="register-container">
    <image-upload />
    <form @submit.prevent="submit">
      <div class="field">
        <label class="label">Username</label>
        <div class="control">
          <input
            v-model="register_data.username"
            class="input block"
            required="required"
            type="text"
          >
        </div>
      </div>
      <div class="field">
        <label class="label">Email</label>
        <div class="control">
          <input
            v-model="register_data.email"
            class="input block"
            required="required"
            type="text"
          >
        </div>
      </div>
      <div class="field">
        <label class="label">Password</label>
        <div class="control">
          <input
            v-model="register_data.password"
            class="input block"
            required="required"
            type="password"
          >
        </div>
      </div>
      <button class="button is-primary">Submit</button>
    </form>
  </div>
</template>

<script lang="ts">
import { RegisterData } from "@/generated/models";
import { defineComponent } from "vue";
import ImageUpload from "../input/ImageUpload.vue";

export default defineComponent({
  components: {
    ImageUpload,
  },

  data() {
    return {
      register_data: {
        email: "",
        password: "",
        name: "",
      } as RegisterData,
    };
  },

  methods: {
    async submit() {
      this.register(this.register_data);
    },
    async register(register_data: RegisterData): Promise<void> {
      console.log("tried to register with the following data:" + register_data);
    },
  },
});
</script>

<style>
.filepond--item {
  width: calc(50% - 0.5em);
}
</style>
