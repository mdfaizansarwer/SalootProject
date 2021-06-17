import ApiResult from "@/services/api/apiResult";
import httpService from "@/services/api/httpService";
import API_URLS from "@/services/api/apiUrls";
import Login from "@/models/user/login";
import { User, UserCreate } from "@/models/user/user";
import TokenRequest from "@/models/authentication/tokenRequest";
import localStorageUtils from "@/common/localStorageUtils";
import { AxiosResponse } from "axios";
import UserSignInViewModel from "@/models/user/userSignInViewModel";
import Token from "@/models/authentication/token";

class AuthenticationService {

    public async login(login: Login): Promise<User> {
        const tokenRequest: TokenRequest = {
            grantType: "password",
            username: login.username,
            password: login.password,
            accessToken: "",
            refreshToken: "",
        };

        const response = await httpService.post<UserSignInViewModel>(API_URLS.LOGIN, tokenRequest);
        if (response?.data?.isSuccess == true) {
            localStorageUtils.setItem("Token", response.data.data.token);
        }
        else {
            localStorageUtils.removeItem("Token");
        }
        return response.data.data.userViewModel;
    }

    public async register(register: UserCreate): Promise<User> {
        const response = await httpService.post<UserSignInViewModel>(API_URLS.REGISTER, register);
        if (response?.data?.isSuccess == true) {
            localStorageUtils.setItem("Token", response.data.data.token);
        }
        else {
            localStorageUtils.removeItem("Token");
        }
        return response.data.data.userViewModel;
    }

    public async logout(): Promise<AxiosResponse<ApiResult>> {
        return await httpService.post(API_URLS.LOGOUT, null);
    }

    // Get new token and set that in local storage
    public async setTokenWithRefreshToken() {
        const token = localStorageUtils.getItem("Token") as Token;
        const tokenRequest: TokenRequest =
        {
            grantType: "refresh_token",
            username: "",
            password: "",
            accessToken: token.accessToken,
            refreshToken: token.refreshToken
        };

        const response = await httpService.post<ApiResult>(API_URLS.REFRESH_TOKEN, tokenRequest);
        if (response?.data?.isSuccess == true) {
            localStorageUtils.setItem("Token", response.data.data);
        }
        else {
            localStorageUtils.removeItem("Token");
        }
    }
}

export default new AuthenticationService();