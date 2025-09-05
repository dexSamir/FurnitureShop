using System.Net;

namespace FurnitureShop.BL.Exceptions.Common;

public class EmptyIdsException : BaseException
{
    public EmptyIdsException(string message, string? errorCode = null, int code = 0)
        : base(message, HttpStatusCode.BadRequest, errorCode, code)
    {
    }
}

public class EmptyIdsException<T> : EmptyIdsException
{
    public EmptyIdsException(string? errorCode = null, int code = 0)
        : base($"{typeof(T).Name}'s ids are not given!", errorCode, code)
    {
    }
}