import Vue from "vue";
import App from "./App.vue";
import router from "@/router";
import store from "@/store";
import Notifications from "vue-notification";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "jquery/src/jquery.js";
import "bootstrap/dist/js/bootstrap.min.js";

Vue.use(Notifications);

Vue.config.productionTip = false;
Vue.config.errorHandler = (err, vm, info) => {
  // err: error trace
  // vm: component in which error occured
  // info: Vue specific error information such as lifecycle hooks, events etc.

  // TODO: Perform any custom logic or log to server
  console.log(err.message);
};

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app")