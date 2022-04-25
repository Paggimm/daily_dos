import { computed, ComputedRef } from 'vue'
import { GetterTree, Store } from 'vuex'
import { State } from './store'

enum GetterTypes
{
    LOGGED_IN = 'LOGGED_IN'
}

export const getters: GetterTree<State, unknown> = {
    [GetterTypes.LOGGED_IN](state)
    {
        return state.token !== undefined
    }
}

/**
 * wrapperclass for smoother Access to vuex mutations
 */
export class Getters
{
    protected readonly store

    public constructor(store: Store<State>)
    {
        this.store = store
    }

    public get isLoggedIn(): ComputedRef<boolean>
    {
        return computed(() => this.store.getters[GetterTypes.LOGGED_IN])
    }
}
