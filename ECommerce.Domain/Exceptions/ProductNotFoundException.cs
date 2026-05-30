namespace ECommerce.Domain.Exceptions;

using System;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId) 
        : base($"El producto con ID {productId} no fue encontrado en el sistema.")
    {
    }
}