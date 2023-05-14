<template>
    <div id="app" class="bg-sky-50">
        <Header />
        <div class="router-view">
            <router-view />
        </div>
    </div>
</template>
<script setup>
import Header from "@/components/header/Header.vue";</script>
<style>
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    padding-bottom: 5vw;
}
</style>
