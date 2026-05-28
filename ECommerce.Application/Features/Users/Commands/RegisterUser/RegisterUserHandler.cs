namespace ECommerce.Application.Features.Users.Commands.RegisterUser;

using MediatR;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    // Inyectamos el repositorio y nuestro servicio de hashing abstracto
    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar si el email ya existe
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new InvalidOperationException("Ya existe un usuario con este correo electrónico.");

        // 2. Encriptar la contraseña delegando a la infraestructura
        var hashedPassword = _passwordHasher.Hash(request.Password);

        // 3. Crear entidad de dominio
        var user = new User(request.Email, request.Name, hashedPassword);

        // 4. Persistir
        await _userRepository.AddAsync(user, cancellationToken);

        // Si el email es el maestro, lo ascendemos a Admin automáticamente
        if (request.Email.ToLower() == "admin@ecommerce.com")
        {
            user.AssignRole("Admin");
        }

        // 5. Retornar DTO seguro
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
    }
}