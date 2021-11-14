<template>
  <div>
    <h1>schicke neue App</h1>
    <p>ich hoffe sie gefällt dir</p>
    <button @click="changeText">
      OK
    </button>
    <p class="temp_txt1">
      {{ msg }}
    </p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "HelloWorld",
  props: {
    initmsg: { type: String, required: true, default: "" },
  },
  // in Data kommen verwendete Attribute rein
  data() {
    return {
      msg: this.initmsg,
      msg1: "und wenn nicht ist auch nicht so schlimm",
      msg2: "ist es das wirklich?",
    };
  },

  // in Methods werden Methoden definiert
  methods: {
    changeText: function () {
      if (this.msg === this.msg1) {
        this.msg = this.msg2;
      } else {
        this.msg = this.msg1;
      }
    },
  },
});
</script>

<style></style>
