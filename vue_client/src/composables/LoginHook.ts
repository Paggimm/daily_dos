import {ref} from "vue";
import {LoginResponse} from "@/generated/models";

export default function useLoginFeature() {
    const username = ref("");
    const password = ref("");

    async function login(): Promise<LoginResponse> {
        const loginViewModel = { name: username.value, password: password.value }
        let loginResponse = { token: ''};

        const reponse = await fetch("http://localhost:8085/login", {
                body: JSON.stringify(loginViewModel),
                method: "POST",
            })

        if(reponse.status === 200) {
            loginResponse = await reponse.json();
        }
        return loginResponse;
    }

    return {username, password, login};
}
