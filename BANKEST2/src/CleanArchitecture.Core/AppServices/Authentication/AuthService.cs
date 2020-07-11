using BANKEST2.Core.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BANKEST2.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private byte[] salt = new byte[] { 62, 4, 14, 42, 12,44, 62, 0 };
        private IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AuthenticateUser(string UserName, string Password)
        {
            var users = _unitOfWork.Users.GetFiltered(i => i.UserName == UserName);
            var user = users.FirstOrDefault();
            if (user == null)
                return false;
            string hashed = GenerateHash(Password);
            return string.Equals(hashed, user.Password);
        }

        private string GenerateHash(string Password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                   password: Password,
                   salt: salt,
                   prf: KeyDerivationPrf.HMACSHA1,
                   iterationCount: 10000,
                   numBytesRequested: 256 / 8));
        }
    }
}
