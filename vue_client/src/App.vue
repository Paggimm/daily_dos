<template>
    <div id="app">
        <Header/>
        <div class="router-view">
            <router-view/>
        </div>
        <!--
        <ServerStatus />
        -->
    </div>
</template>
<script setup lang="ts">
</script>
<style>
@import "css/colors.css";
@import "css/measures.css";

#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    background-color: var(--background-surface-bright);
    padding-bottom: 5vw;
}
</style>
