namespace ECommerce.Domain.Entities;

public enum OrderStatus 
{ 
    Pending,
    Paid,
    Confirmed, 
    Shipped, 
    Delivered, 
    Cancelled 
}