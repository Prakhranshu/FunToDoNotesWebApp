using CommonLayer.RequestModels;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly FunNoteContext context;
        BcryptEncryption bcrypt= new BcryptEncryption();


        public UserRepository(FunNoteContext context)
        {
            this.context = context;
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

        public UserEntity UserLogin(LoginModel model)
        {

            UserEntity user = context.UserTable.FirstOrDefault(x => x.Useremail == model.useremail);

            if (user != null)
            {
                if (bcrypt.MatchPass(model.userpassword, user.Userpassword))
                {
                    return user;
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
    }
}
