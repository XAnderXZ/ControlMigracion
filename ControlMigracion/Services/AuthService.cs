using ControlMigracion.Data;
using ControlMigracion.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ControlMigracion.Services
{
    public class AuthService
    {
        private readonly ControlMigracionContext _context;

        public AuthService(ControlMigracionContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Authenticate(string email, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        public async Task<Usuario> Register(Usuario newUser)
        {
            // Verificar si el usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == newUser.Email))
            {
                return null;
            }

            // En una aplicación real, aquí se debería hacer hash de la contraseña
            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // En una aplicación real, deberías usar hashing y salting.
            // Por simplicidad, aquí comparamos directamente.
            return inputPassword == storedPassword;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var identity = new ClaimsIdentity(claims, "login");
            return new ClaimsPrincipal(identity);
        }
    }
}

