using CommonLayer.RequestModels;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            return repository.UserRegistration(model);
        }

        public string UserLogin(LoginModel model)
        {
            return repository.UserLogin(model);
        }

        public ForgotPasswordModel ForgotPassword(string email)
        {
            return repository.ForgotPassword(email);
        }

        public string ResetPassword(string email, ResetPasswordModel model)
        {
            return repository.ResetPassword(email, model);
        }
        public bool CheckEmail(string email)
        {
            return repository.CheckEmail(email);
        }
    }
}
