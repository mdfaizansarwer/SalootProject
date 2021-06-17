export interface RoutesNames {

  signIn: string;

  adminPanel: string;

  userPanel: string;

}

const routesNames: Readonly<RoutesNames> = {

  signIn: "signIn",

  adminPanel: "adminPanel",

  userPanel: "userPanel",

};

declare module "vue/types/vue" {
  interface Vue {
    $routesNames: RoutesNames;
  }
}

export default routesNames;