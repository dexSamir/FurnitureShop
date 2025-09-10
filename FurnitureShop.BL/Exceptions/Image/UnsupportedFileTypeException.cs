using System.Net;

namespace FurnitureShop.BL.Exceptions.Image;

public class UnsupportedFileTypeException : BaseException
{
    public UnsupportedFileTypeException(string message, string? errorCode = null, int code = 0)
        : base(message, HttpStatusCode.BadRequest, errorCode, code)
    {
    }
}