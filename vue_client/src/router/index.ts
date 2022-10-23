import {createRouter, createWebHashHistory, RouteRecordRaw} from 'vue-router'
import {RouterDefinitions} from "@/enums/RouterDefinitions";
import {useAuthStore} from "@/store/AuthStore";

// checks if the current user is logged in and returns a redirect to the login-page
// TODO: take to-route as parameter and redirect from login-page after successful login to to-route
const checkLoggedIn = () => {
        const authStore = useAuthStore();
        if(!authStore.isLoggedIn) {
            return { name: RouterDefinitions.LOGIN }
        }
}

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: RouterDefinitions.HOME,
        component: () => import('@/views/Home.vue')
    },
    {
        path: '/login',
        name: RouterDefinitions.LOGIN,
        component: () => import('@/views/Login.vue')
    },
    {
        path: '/register',
        name: RouterDefinitions.REGISTER,
        component: () => import('@/views/Register.vue')
    },
    {
        path: '/about',
        name: RouterDefinitions.ABOUT,
        component: () => import('@/views/About.vue')
    },
    {
        path: '/logout',
        name: RouterDefinitions.LOGOUT,
        component: () => import('@/views/Logout.vue')
    },
    {
        path: '/my',
        name: RouterDefinitions.My,
        component: () => import('@/views/My.vue')
    },
    {
        path: '/activities',
        name: RouterDefinitions.ACTIVITIES,
        component: () => import('@/views/ActivitiesOverview.vue'),
        beforeEnter: checkLoggedIn
    },
    {
        path: '/plans',
        name: RouterDefinitions.PLANS,
        component: () => import('@/views/PlansOverview.vue'),
        beforeEnter: checkLoggedIn
    },
]

const router = createRouter({
    history: createWebHashHistory(),
    routes
})

export default router
