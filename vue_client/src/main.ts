import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)

// directive to react on clicks outside of a specific element
// stolen from https://stackoverflow.com/a/64698630
const clickOutside = {
    beforeMount: (el: any, binding: any) => {
      el.clickOutsideEvent = (event: Event) => {
        // here I check that click was outside the el and his children
        if (!(el == event.target || el.contains(event.target))) {
          // and if it did, call method provided in attribute value
          binding.value();
        }
      };
      document.addEventListener("click", el.clickOutsideEvent);
    },
    unmounted: (el: any) => {
      document.removeEventListener("click", el.clickOutsideEvent);
    },
};


const app = createApp(App)
app.use(router)
app.use(pinia)
app.directive("click-outside", clickOutside)
app.mount('#app')
