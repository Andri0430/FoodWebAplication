using FoodAplication.Context;
using FoodAplication.Dtos;
using FoodAplication.Interface;
using FoodAplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FoodAplication.Repository
{
    public class AccountRepository : IAccount
    {
        private readonly FoodContext _context;
        private readonly IConfiguration _configuration;

        public AccountRepository(FoodContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public Task<Account> GetAccountByEmail(string email)
        {
            var account = _context.Accounts.Where(a => a.Email == email).FirstOrDefaultAsync();
            return account!;
        }

        public Task<Account> GetAccountByPhoneNumber(string phone)
        {
            var account = _context.Accounts.Where(a => a.PhoneNumber == phone).FirstOrDefaultAsync();
            return account!;
        }

        public async Task<object> RegisterAccount(Account account)
        {
            var emailAccountChecked = await GetAccountByEmail(account.Email);
            var phoneAccountChecked = await GetAccountByPhoneNumber(account.PhoneNumber);
            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password, passwordSalt);
            account.Password = passwordHash;
            account.Role = "User";

            if (emailAccountChecked != null) return new { success = false, message = "Email sudah pernah terdaftar!" };
            if (phoneAccountChecked != null) return new { success = false, message = "Nomor telepon sudah pernah terdaftar!" };

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return new { success = true, message = "Akun berhasil dibuat" };
        }

        public async Task<object> LoginAccount(LoginDto login)
        {
            var emailAccountChecked = await GetAccountByEmail(login.Email);

            if (emailAccountChecked == null) return new { success = false, message = "Akun belum terdaftar!" };
            if (!BCrypt.Net.BCrypt.Verify(login.Password, emailAccountChecked!.Password)) return new { success = false, message = "Password Salah!" };

            string token = await CreateToken(emailAccountChecked);
            return new { success = true, message = "Berhasil login",role = emailAccountChecked.Role, code = token };
        }

        private async Task<string> CreateToken(Account accounts)
        {
            var account = await GetAccountByEmail(accounts.Email);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.MobilePhone, account.PhoneNumber),
                new Claim(ClaimTypes.Role, account.Role!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: credential
                    );

            var sendToken = new JwtSecurityTokenHandler().WriteToken(token);
            return sendToken;
        }
    }
}