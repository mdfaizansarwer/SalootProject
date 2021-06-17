import ApiResultStatusCode from "@/common/enums/apiResultStatusCode";

type ApiResult<T = any> = {

    isSuccess: boolean;

    statusCode: ApiResultStatusCode;

    message: string;

    data: T;

}

export default ApiResult;