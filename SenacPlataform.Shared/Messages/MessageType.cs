using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenacPlataform.Shared.Messages;
internal class MessageType
{
}

public enum ErrorType
{
    NotFound = 1,
    AlreadyExists = 2,
    InvalidData = 3,
    InvalidOperation = 4,
    Unauthorized = 5,
    Forbidden = 6,
    BadRequest = 7,
    InternalServerError = 8
}
