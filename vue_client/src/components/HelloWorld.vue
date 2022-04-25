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

<script setup lang="ts">
import { defineProps, ref } from "vue";

interface Props {
  initmsg?: string;
}
const props = defineProps<Props>();

const msg = ref(props.initmsg);
const msg1 = "und wenn nicht ist auch nicht so schlimm";
const msg2 = "ist es das wirklich?";

function changeText() {
  if (msg.value === msg1) {
    msg.value = msg2;
  } else {
    msg.value = msg1;
  }
}
</script>

<style></style>
