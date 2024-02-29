using CommonLayer.RequestModels;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FunNoteContext context;
        private readonly IConfiguration config;

        BcryptEncryption bcrypt= new BcryptEncryption();


        public UserRepository(FunNoteContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;

        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity entity =new UserEntity();
            entity.Firstname = model.Firstname;
            entity.Lastname = model.Lastname;
            entity.Useremail = model.Useremail;
            entity.Userpassword = bcrypt.HashPassGenerator( model.Userpassword);
            entity.CreateTime = DateTime.Now;
            entity.LastLoginTime = DateTime.Now;
            context.UserTable.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public string UserLogin(LoginModel model)
        {

            UserEntity user = context.UserTable.FirstOrDefault(x => x.Useremail == model.useremail);

            if (user != null)
            {
                if (bcrypt.MatchPass(model.userpassword, user.Userpassword))
                {
                    user.LastLoginTime = DateTime.Now;
                    string token = GenerateToken(user.Useremail, user.UserId);
                    //Console.WriteLine(token);
                    return token;
                }
                else
                {
                    throw new Exception("Incorrect password");
                }
            }
            else
            {
                throw new Exception("Incorrect email");
            }
        }



        private string GenerateToken(string Email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("email",Email),
                new Claim("userId",userId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }



        public ForgotPasswordModel ForgotPassword(string email)
        {
            var entity = context.UserTable.SingleOrDefault(user => user.Useremail == email);
            ForgotPasswordModel model = new ForgotPasswordModel();
            model.UserId = entity.UserId;
            model.UserEmail = entity.Useremail;
            model.Token = GenerateToken(email, entity.UserId);
            return model;
        }
        public string ResetPassword(string email, ResetPasswordModel model)
        {
            if (model.new_password == model.confirm_password)
            {
                if (CheckEmail(email))
                {
                    var entity = context.UserTable.SingleOrDefault(user => user.Useremail == email);
                    entity.Userpassword = bcrypt.HashPassGenerator(model.new_password);
                    context.SaveChanges();
                    return "true";
                }
                throw new Exception("Such Email does not exist...");
            }
            throw new Exception("Password Does not match...");
        }

        public bool CheckEmail(string email)
        {
            var user = context.UserTable.SingleOrDefault(user => user.Useremail == email);
            return user != null;
        }

        //public bool ResetPassword(string Email, ForgotPassword model, )





    }
}
