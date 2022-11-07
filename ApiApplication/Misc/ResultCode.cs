namespace ApiApplication {
    public enum ResultCode {
        Ok = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,

        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        NotAcceptable = 406,
        Conflict = 409,
        PreconditionFailed = 412,
        Locked = 423,

        ServerError = 500,
        NotImplemented = 501

    }
}
