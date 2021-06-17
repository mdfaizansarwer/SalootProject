enum ApiResultStatusCode {

    Success = 1,

    ServerError = 2,

    BadRequest = 4,

    NotFound = 8,

    UnAuthorized = 16,

    ExpiredSecurityToken = 32,

    Forbidden = 64,

    LogicError = 128,

}

export default ApiResultStatusCode;