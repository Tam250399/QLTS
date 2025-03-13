using GS.Core.Configuration;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GS.Services.Authentication
{
    public partial class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CauHinhNguoiDung _customerSettings;
        private readonly INguoiDungService _nguoiDungService;
        private readonly CauHinhChung _cauHinhChung;
        private readonly GSConfig _gsConfig;
        private NguoiDung _cachedCustomer;

        #endregion

        #region Ctor
        public CookieAuthenticationService(GSConfig gsConfig,
            IHttpContextAccessor httpContextAccessor,
            CauHinhNguoiDung customerSettings,
            INguoiDungService nguoiDungService, CauHinhChung cauHinhChung)
        {
            this._gsConfig = gsConfig;
            this._cauHinhChung = cauHinhChung;
            this._httpContextAccessor = httpContextAccessor;
            this._customerSettings = customerSettings;
            this._nguoiDungService = nguoiDungService;
        }
        #endregion

        #region Methods
        public virtual NguoiDung GetNguoiDungDangNhap()
        {
            //whether there is a cached customer
            if (_cachedCustomer != null)
                return _cachedCustomer;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(GSAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            NguoiDung customer = null;
            //try to get customer by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                && claim.Issuer.Equals(GSAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (usernameClaim != null)
                customer = _nguoiDungService.GetNguoiDungByUsername(usernameClaim.Value);

            //whether the found customer is available
            if (customer == null || customer.TRANG_THAI_ID == (int)NguoiDungStatusID.Delete)
                return null;

            //cache authenticated customer
            _cachedCustomer = customer;

            return _cachedCustomer;
        }

        public virtual async void DangNhap(NguoiDung nguoiDung, bool isPersistent)
        {
            if (nguoiDung == null)
                throw new ArgumentNullException(nameof(nguoiDung));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(nguoiDung.TEN_DANG_NHAP))
                claims.Add(new Claim(ClaimTypes.Name, nguoiDung.TEN_DANG_NHAP, ClaimValueTypes.String, GSAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, GSAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            // sign in
            await _httpContextAccessor.HttpContext.SignInAsync(GSAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);
        }

        public virtual async void DangXuat()
        {
            //reset cached customer
            _cachedCustomer = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(GSAuthenticationDefaults.AuthenticationScheme);
        }
        #endregion
        #region Token
        private List<Claim> GetUserClaims(NguoiDung item)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.PrimarySid, item.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.PrimaryGroupSid, item.GUID.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, item.TEN_DANG_NHAP));
            claims.Add(new Claim(ClaimTypes.Name, item.TEN_DAY_DU));
            return claims;
        }

        public string GenerateToken(NguoiDung item)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes("1q2w3e4r5t6y7u8i9o0p");
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, item.ID.ToString()),
            //        new Claim(ClaimTypes.Role, item.TEN_DANG_NHAP)
            //    }),
            //    Issuer = "http://gsolution.vn",
            //    Audience = "http://gsolution.vn",
            //    //Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            var claims = GetUserClaims(item);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _gsConfig.ApiValidIssuer,
                Audience = _gsConfig.ApiValidIssuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_gsConfig.ApiKeyEncrypt)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            //var jwtToken = new JwtSecurityToken(
            //    issuer: this._cauHinhChung.ApiValidIssuer,
            //    audience: this._cauHinhChung.ApiValidIssuer,
            //    claims: claims,
            //    signingCredentials: credentials
            //);
            //return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }
        private IEnumerable<Claim> GetClaims(string tokenString)
        {
            try
            {
                SecurityToken securityToken = new JwtSecurityToken(tokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_gsConfig.ApiKeyEncrypt)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _gsConfig.ApiValidIssuer,
                    ValidIssuer = _gsConfig.ApiValidIssuer,
                    ValidateLifetime = false
                };

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);

                return claimsPrincipal.Claims;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public NguoiDung ValidateToken(string tokenString)
        {
            var claims = GetClaims(tokenString);
            if (claims != null)
            {
                var item = new NguoiDung();
                item.ID = Convert.ToDecimal(claims.Where(c => c.Type == ClaimTypes.PrimarySid).First().Value);
                item.GUID = new Guid(claims.Where(c => c.Type == ClaimTypes.PrimaryGroupSid).First().Value);
                item.TEN_DANG_NHAP = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
                item.TEN_DAY_DU = claims.Where(c => c.Type == ClaimTypes.Name).First().Value;
                item.MAT_KHAU_KEY = tokenString;
                return item;
            }
            return null;
        }
        #endregion
    }
}
