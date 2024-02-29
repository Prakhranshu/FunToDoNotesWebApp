using CommonLayer;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interfaces;
using ManagerLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunDoNotesApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usermanager;
        private IBus bus;

        public UserController(IUserManager usermanager, IBus bus)
        {
            this.usermanager = usermanager;
            this.bus = bus;
        }

        [HttpPost]
        [Route("Reg")]  

        public ActionResult Register(RegisterModel model)
        {
            try
            {


                var response = usermanager.UserRegistration(model);

                if (response != null)
                {
                    return Ok(new ResModel<UserEntity> { Success = true, Message = "register successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Register failed", Data = response });
                }
            }

            catch(Exception ex)
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });

            }
        }

        [HttpPost]
        [Route("Log")]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                string response = usermanager.UserLogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<String> { Success = true, Message = "Login sucessfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<String> { Success = false, Message = "Login failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<String> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            try
            {
                if (usermanager.CheckEmail(Email))
                {
                    Send mail = new Send();
                    ForgotPasswordModel model = usermanager.ForgotPassword(Email);
                    string str = mail.SendMail(model.UserEmail, model.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FunfooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(model);
                    return Ok(new ResModel<string> { Success = true, Message = str, Data = model.Token });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult Reset(ResetPasswordModel model)
        {
            try
            {
                string email = User.FindFirst("Email").Value;
                return Ok(new ResModel<string>
                {
                    Success = true,
                    Message = "Password Reset Successful",
                    Data = usermanager.ResetPassword(email, model)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = "Password reset unsuccessful"
                });
            }
        }



        /*[Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;

                if(userBusiness.ResetPassword(Email,reset))
                {
                    return Ok(new ResModel<bool> { IsSuccess = true, Message = "Password Changed", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { IsSuccess = false, Message = "Password Not Changed", Data = false });
                }
            }


        presentation 
        manage              
        repo
        common



            catch(Exception ex)
            {
                throw ex;
            }
        }*/
    }
}
