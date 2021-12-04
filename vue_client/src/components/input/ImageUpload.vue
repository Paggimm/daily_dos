<template>
  <div>
    <file-pond
      ref="pond"
      name="test"
      label-idle="Drop files here..."
      :allow-multiple="true"
      accepted-file-types="image/jpeg, image/png"
      server="/api"
      instant-upload="false"
      @init="handleFilePondInit"
    />
  </div>
</template>

<script lang="ts">
import { Component, defineComponent } from "vue";

// Import Vue FilePond + Plugins and Styles
import vueFilePond from "vue-filepond";
import "filepond/dist/filepond.min.css";
import "filepond-plugin-image-preview/dist/filepond-plugin-image-preview.min.css";
import FilePondPluginFileValidateType from "filepond-plugin-file-validate-type";
import FilePondPluginImagePreview from "filepond-plugin-image-preview";

//create FilePond Component
const FilePond = vueFilePond(
  FilePondPluginFileValidateType,
  FilePondPluginImagePreview
) as Component;

export default defineComponent({
  name: "ImageUpload",

  components: {
    FilePond,
  },

  methods: {
    handleFilePondInit(): void {
      console.log("FilePond has initialized");

      // FilePond instance methods are available on `this.$refs.pond`
    },
  },
});
</script>
