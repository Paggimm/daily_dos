<template>
    <div class="register-container">
        <ImageUpload />
        <form @submit.prevent="submit">
            <div class="field">
                <label class="label">Username</label>
                <div class="control">
                    <input
                        v-model="register_data.name"
                        class="input block"
                        required
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
                        required
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
                        required
                        type="password"
                    >
                </div>
            </div>
            <button class="button is-primary">Submit</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import type {RegisterData} from "@/generated/models";
import ImageUpload from "@/components/form-components/ImageUpload.vue";

const register_data: RegisterData = {
    email: "",
    password: "",
    name: "",
};

async function register(register_data: RegisterData): Promise<void> {
    console.log("tried to register with the following data:", register_data);
}

async function submit() {
    register(register_data);
}
</script>

<style>
.filepond--item {
    width: calc(50% - 0.5em);
}
</style>
