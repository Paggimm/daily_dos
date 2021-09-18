import { MutationTree, Store } from 'vuex'
import { MutationTypes } from './mutation-types'
import { State, store } from '../store'

export const mutations: MutationTree<State> = {
    [MutationTypes.SET_TOKEN](state, token: string)
    {
        state.token = token
        return state
    }
}

export class Mutations
{
    private store

    public constructor(store: Store<State>)
    {
        this.store = store
    }

    public setToken(token: string): void
    {
        store.commit(MutationTypes.SET_TOKEN, token)
    }
}
