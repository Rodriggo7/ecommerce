namespace ECommerce.Application.Interfaces;

using ECommerce.Domain.Entities;

public interface IJwtProvider
{
    string Generate(User user);
}