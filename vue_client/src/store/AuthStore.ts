import { defineStore } from "pinia"

export const useAuthStore = defineStore('authStore', {
    state: () => ({
        token: "",
    }),
    getters: {
        getToken: (state) => { return state.token },
        isLoggedIn: (state) => { return state.token !== "" }
    },
    actions: {
        setToken(token: string)
        {
            this.token = token
        }
    },
    persist: true,
})
