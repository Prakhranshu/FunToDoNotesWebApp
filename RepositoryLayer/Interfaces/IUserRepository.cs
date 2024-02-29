using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(RegisterModel model);
        public string UserLogin(LoginModel model);
        public ForgotPasswordModel ForgotPassword(string email);
        public string ResetPassword(string email, ResetPasswordModel model);
        public bool CheckEmail(string email);


    }
}
