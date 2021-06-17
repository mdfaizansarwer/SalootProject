import VueRouter, { NavigationGuardNext, Route } from "vue-router";
import Vue from "vue";
import Routes from "@/router/routes";
import RoutesNames from "@/router/routesNames";
import User from "@/store/modules/user/userModule";


Vue.use(VueRouter)

const router = new VueRouter({
  mode: "history",
  routes: Routes,
});

const requiresAuthGuard = (to: Route, from: Route, next: NavigationGuardNext): boolean => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    const isLoggedIn = User.isLoggedIn;
    if (!isLoggedIn) {
      next({
        name: RoutesNames.signIn,
        query: { redirect: to.path }
      });
    } else {
      next();
    }
    return true;
  }
  return false;
};

const anonymousOnlyGuard = (to: Route, from: Route, next: NavigationGuardNext): boolean => {
  if (to.matched.some(record => record.meta.anonymousOnly)) {
    const isAnonymous = !User.isLoggedIn;
    if (!isAnonymous) {
      next({
        name: RoutesNames.signIn
      });
    } else {
      next();
    }
    return true;
  }
  return false;
};


router.beforeEach(async (to, from, next) => {
  if (requiresAuthGuard(to, from, next)) {
    return;
  }
  if (anonymousOnlyGuard(to, from, next)) {
    return;
  }
  next(); // make sure to always call next()!
});

export default router;
