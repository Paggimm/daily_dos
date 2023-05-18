import {ref} from "vue";
import type {LoginResponse} from "@/generated/models";
import {fetchRequest, fetchWithTimeout} from "@/utils";

export default function useLoginFeature() {
    const username = ref("");
    const password = ref("");

    async function login(): Promise<LoginResponse> {
        const loginViewModel = {name: username.value, password: password.value}
        let loginResponse = {token: ''};

        const reponse = await fetchRequest('login', JSON.stringify(loginViewModel), 'POST');

        if (reponse.status === 200) {
            loginResponse = await reponse.json();
        }
        return loginResponse;
    }

    return {username, password, login};
}
