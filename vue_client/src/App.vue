<template>
    <div id="app">
        <Header/>
        <div class="router-view">
            <router-view/>
        </div>
    </div>
</template>
<script setup lang="ts">
import Header from "@/components/header/Header.vue";
</script>
<style lang="less">
@import "css/colors.less";
@import "css/measures.less";

#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    background-color: @background-surface-bright;
    padding-bottom: 5vw;
}
</style>
