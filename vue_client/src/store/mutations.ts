import { MutationTree, Store } from 'vuex'
import { State, store } from './store'

enum MutationTypes
{
    SET_TOKEN = 'SET_TOKEN'
}

/**
 * sontains every store-mutation definition
 */
export const mutations: MutationTree<State> = {
    [MutationTypes.SET_TOKEN](state, token: string)
    {
        state.token = token
        return state
    }
}

/**
 * wrapperclass for smoother Access to vuex mutations
 */
export class Mutations
{
    protected store

    public constructor(store: Store<State>)
    {
        this.store = store
    }

    public setToken(token: string): void
    {
        store.commit(MutationTypes.SET_TOKEN, token)
    }
}
