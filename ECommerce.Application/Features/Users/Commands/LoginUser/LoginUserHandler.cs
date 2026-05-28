namespace ECommerce.Application.Features.Users.Commands.LoginUser;

using MediatR;
using ECommerce.Application.Interfaces;
using System.Security.Authentication;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar al usuario por correo
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        // 2. Si no existe, o si la contraseña no coincide, lanzamos excepción de autenticación
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new InvalidCredentialException("Credenciales inválidas.");
        }

        // 3. Si todo es correcto, generamos y retornamos el token JWT
        return _jwtProvider.Generate(user);
    }
}