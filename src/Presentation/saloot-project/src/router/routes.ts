import RoutesNames from "@/router/routesNames";
import { RouteConfig } from "vue-router";


const routes: RouteConfig[] = [
    {
        path: "/",
        name: RoutesNames.signIn,
        component: () => import("@/views/SignIn.vue")
    },
    {
        path: "/adminPanel",
        name: RoutesNames.adminPanel,
        component: () => import("@/views/AdminPanel.vue"),
        meta: {
            requiresAuth: true,
        }
    },
    {
        path: "/userPanel",
        name: RoutesNames.userPanel,
        component: () => import("@/views/UserPanel.vue"),
        meta: {
            requiresAuth: true,
        }
    },

]

export default routes;