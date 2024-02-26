using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Net.Http;

namespace FunDoNotesApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usermanager;

        public UserController(IUserManager usermanager)
        {
            this.usermanager = usermanager;
        }

        [HttpPost]
        [Route("Reg")]  

        public ActionResult Register(RegisterModel model)
        {
            var response = usermanager.UserRegistration(model);

            if(response != null)
            {
                return Ok(new ResModel<UserEntity> { Success = true, Message = "register successfully", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Register failed", Data = response });
            }
        }

        [HttpPost]
        [Route("Log")]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var response = usermanager.UserLogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<UserEntity> { Success = true, Message = "Login sucessfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Login failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
    }
}
