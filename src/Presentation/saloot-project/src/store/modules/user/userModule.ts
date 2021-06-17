import {
    Action,
    getModule,
    Module,
    Mutation,
    VuexModule
} from "vuex-module-decorators";
import modulesNames from "@/store/moduleNames";
import store from "@/store/index";
import localStorageUtils from "@/common/localStorageUtils";
import { User, UserCreate } from "@/models/user/user";
import Token from "@/models/authentication/token";
import authenticationService from "@/services/authentication/authenticationService";
import userService from "@/services/user/userService";
import Login from "@/models/user/login";
import { isEmptyObject } from "@/common/objectUtils";
import AccountSettingStoreModel from "@/models/user/accountSettingStoreModel";

@Module({ dynamic: true, namespaced: true, store, name: modulesNames.user })
class UserModule extends VuexModule {

    private _currentUser?: User = localStorageUtils.getItem("User");
    private _authToken?: Token = localStorageUtils.getItem("Token");

    get currentUser(): User | undefined | null {
        return this._currentUser;
    }

    get authToken(): Token | undefined | null {
        return this._authToken;
    }

    get isLoggedIn(): boolean {
        return !isEmptyObject(this.currentUser);
    }

    @Mutation
    private SET_CURRENT_USER(currentUser?: User): void {
        if (currentUser) {
            this._currentUser = currentUser;
            localStorageUtils.setItem("User", currentUser);
        } else {
            this._authToken = undefined;
            localStorageUtils.removeItem("User");
        }
    }

    @Mutation
    private SET_AUTH_TOKEN(authToken?: Token): void {
        if (authToken) {
            this._authToken = authToken;
            localStorageUtils.setItem("Token", authToken);
        } else {
            this._authToken = undefined;
            localStorageUtils.removeItem("Token");
        }
    }

    @Action({ rawError: true })
    public async login(login: Login): Promise<void> {
        const response = await authenticationService.login(login);
        this.SET_CURRENT_USER(response);
    }

    @Action({ rawError: true })
    public async register(userCreate: UserCreate): Promise<void> {
        const response = await authenticationService.register(userCreate);
        this.SET_CURRENT_USER(response);
    }

    @Action({ rawError: true })
    public async update(accountSettingStoreModel: AccountSettingStoreModel): Promise<void> {
        const response = await userService.updateUser(accountSettingStoreModel.userId, accountSettingStoreModel.userUpdate, accountSettingStoreModel.profilePicture);
        this.SET_CURRENT_USER(response);
    }

    @Action({ rawError: true })
    public async logout(): Promise<void> {
        await authenticationService.logout();
        this.SET_AUTH_TOKEN(undefined);
        this.SET_CURRENT_USER(undefined);
    }

}

export default getModule(UserModule);