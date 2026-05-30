namespace ECommerce.Domain.Exceptions;

using System;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string identifier) 
        : base($"El usuario con identificador '{identifier}' no existe.")
    {
    }
}