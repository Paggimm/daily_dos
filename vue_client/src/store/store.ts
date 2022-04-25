import * as Vue from 'vue'
import { createStore, useStore as baseUseStore, Store as VuexStore } from 'vuex'
import { Mutations, mutations } from './mutations'


// define your typings for the store state
export interface State
{
    token: string | undefined
}

// define injection key
export const key: Vue.InjectionKey<VuexStore<State>> = Symbol()

/**
 * initializes a store with default values
 */
export const store = createStore<State>({
    mutations: mutations,
    state: {
        token: undefined
    },
    strict: process.env.NODE_ENV !== 'production'
})

// define your own `useStore` composition function
function useStore(): VuexStore<State>
{
    return baseUseStore(key)
}

/**
 * allows access to vuex store and mutations
 */
export class VuexHandler
{
    private readonly store = useStore()
    public readonly state = store.state
    public mutations = new Mutations(store)
}
