import singleIdApiResult from "@/common/singleIdApiResult";
import ApiResult from "@/services/api/apiResult";
import apiInstance from "@/services/api/axiosConfig";
import { AxiosResponse } from "axios";

class HttpService {

    public async get<T>(url: string): Promise<AxiosResponse<ApiResult<T>>> {

        return await apiInstance.get<ApiResult<T>>(url);
    }

    public async post<T>(url: string, value: any): Promise<AxiosResponse<ApiResult<T>>> {

        return await apiInstance.post<ApiResult<T>>(url, value);
    }

    public async postFile(url: string, file: any): Promise<AxiosResponse<ApiResult<singleIdApiResult>>> {
        const formData = new FormData();
        formData.append("file", file);
        const config: any = {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        };

        formData.set("formFile", file);
        return await apiInstance.post<ApiResult<singleIdApiResult>>(url, formData, config);
    }

    public async put<T>(url: string, value: any): Promise<AxiosResponse<ApiResult<T>>> {

        return await apiInstance.put<ApiResult<T>>(url, value);
    }

    public async delete(url: string): Promise<AxiosResponse<ApiResult>> {

        return await apiInstance.delete<ApiResult>(url);
    }
}

export default new HttpService();