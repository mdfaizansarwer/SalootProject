import Vue from "vue";

export const notifyError = (title: string, message: string): void => {
  Vue.notify({
    type: "error",
    title: title,
    text: message
  });
};

export const notifySuccess = (message: string): void => {
  Vue.notify({
    type: "success",
    text: message
  });
};

export const notifyWarn = (message: string): void => {
  Vue.notify({
    type: "warn",
    title: "Warning",
    text: message
  });
};
