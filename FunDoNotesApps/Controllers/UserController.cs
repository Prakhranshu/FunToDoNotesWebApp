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
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MassTransit;

namespace FunDoNotesApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usermanager;
        private readonly IBus bus;
        private readonly ILogger<UserController> logger;    
        /*private readonly FunNoteContext context;
*/
        public UserController(IUserManager usermanager, IBus bus, ILogger<UserController> logger)
        {
            this.usermanager = usermanager;
            this.bus = bus;
            this.logger = logger;
            /*this.context = context;*/
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
                    logger.LogInformation("Register Successful");
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
                    return Ok(new ResModel<bool> { Success = true, Message = str, Data = true });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult Reset(ResetPasswordModel model)
        {
            try
            {
                string email = User.FindFirst("UserEmail").Value;
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

        [HttpPost]
        [Route ("test")]
        public ActionResult Test(int i, string email)
        {
            
            try
            {
                if(i%2==0)
                {
                    Send mail = new Send();
                    string str = mail.sendconfirmation(email);
                    //string str="Even";
                    /*Uri uri = new Uri("rabbitmq://localhost/FunfooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(email);*/
                    return Ok(new ResModel<bool> { Success = true, Message = str, Data = true });
                }
                return Ok(new ResModel<bool> { Success = true, Message = "odd", Data = true });
            }
            catch (Exception)
            {
                return BadRequest("catch error");
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
