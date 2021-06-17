import router from "@/router";
import routesNames from "@/router/routesNames";
import { notifyError } from "@/common/notificationUtils";
import localStorageUtils from "@/common/localStorageUtils";
import ApiResult from "@/services/api/apiResult";
import Token from "@/models/authentication/token";
import axios, { AxiosRequestConfig, AxiosResponse, AxiosInstance } from "axios";
import ApiResultStatusCode from "@/common/Enums/apiResultStatusCode";
import HttpStatusCodes from "@/common/Enums/httpStatusCodes";
import authenticationService from "@/services/authentication/authenticationService";
import API_URLS from "@/services/api/apiUrls";

const AuthInterceptor = (config: AxiosRequestConfig): AxiosRequestConfig => {

    const token: Token = localStorageUtils.getItem("Token");
    if (config.url == API_URLS.LOGIN.toString() || config.url == API_URLS.REFRESH_TOKEN.toString()) {
        config.headers.Authorization = null;
    }
    else {
        config.headers.Authorization = `Bearer ${token.accessToken}`;
    }

    return config;
};


const OnResponseSuccess = (response: AxiosResponse<any>): AxiosResponse<any> =>
    response;

const OnResponseFailure = (error: any): Promise<any> => {
    const httpStatusCode = error?.response?.status;
    const serverResponse = error?.response?.data as ApiResult;
    const requestUrl = error?.config?.url;

    switch (httpStatusCode) {

        case HttpStatusCodes.UNAUTHORIZED:
            if (serverResponse.statusCode == ApiResultStatusCode.NotFound) {
                notifyError("Authentication error", serverResponse.message);
            }

            if (serverResponse.statusCode == ApiResultStatusCode.ExpiredSecurityToken) {
                if (requestUrl == API_URLS.REFRESH_TOKEN.toString()) {
                    router.push({ name: routesNames.signIn });
                }
                else {
                    authenticationService.setTokenWithRefreshToken();
                }
            }
            break;

        case HttpStatusCodes.INTERNAL_SERVER_ERROR:
            if (serverResponse.statusCode == ApiResultStatusCode.NotFound) {
                notifyError("Authentication error", serverResponse.message);
                router.push({ name: routesNames.signIn });
            }
            notifyError("Server error", serverResponse.message);
            break;

        case HttpStatusCodes.NOT_FOUND:
            notifyError("", "Requested resource was not found.");
            break;

        case HttpStatusCodes.FORBIDDEN:
            notifyError("", "Access to this resource is forbidden");
            break;

        case HttpStatusCodes.UNPROCESSABLE_ENTITY:
            // This case should be handled at the forms
            break;

        default:
            notifyError("", "Unknown error occurred, please try again later.");
            break;

    }

    return Promise.reject("error");
};

const instance: Readonly<AxiosInstance> = axios.create({
    baseURL: process.env.VUE_APP_API_BASE_URL,
    timeout: 5000
});

instance.defaults.headers.get.Accepts = "application/json";
instance.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

instance.interceptors.request.use(AuthInterceptor);
instance.interceptors.response.use(OnResponseSuccess, OnResponseFailure);

export default instance;