using System.Net;

namespace FurnitureShop.BL.Exceptions.Common;

public class UnsupportedDeleteTypeException : BaseException 
{
    public UnsupportedDeleteTypeException(string message, string? errorCode = null, int code = 0)
        : base(message, HttpStatusCode.BadRequest, errorCode, code)
    {
    }
}