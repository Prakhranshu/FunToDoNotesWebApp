using Common.RequestModels;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using System.Collections.Generic;
using System;
using Manager.Interfaces;
using Manager.Services;
using MassTransit;
using RepositoryLayer.Migrations;

namespace FunDoNotesApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager inotesManager;
        public NotesController(INotesManager inotesManager)
        {
            this.inotesManager = inotesManager;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddNote(CreateNotesModel model)
        {
            int id = Convert.ToInt32(User.FindFirst("UserId").Value);
            try
            {
                var response = inotesManager.CreateNote(model, id);
                if (response != null)
                {
                    return Ok(new ResModel<NotesEntity> { Success = true, Message = "Created Note Success", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Create Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }
            
        }

        /*
        [Authorize]
        [HttpGet]
        [Route("{id}", Name = "GetNote")]
        public ActionResult FetchData(int id)
        {
            List<NotesEntity> data = inotesManager.GetNote(id);
            if (data != null)
            {

                return Ok(new ResModel<List<NotesEntity>> { Success = true, Message = "Get Note Successful", Data = data });

            }
            else
            {
                return BadRequest(new ResModel<List<NotesEntity>> { Success = false, Message = "Get Note Failure", Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateNote(int NotesId, UpdateNotesModel model)
        {

            var response = inotesManager.UpdateNote(NotesId, model);
            if (response != null)
            {

                return Ok(new ResModel<NotesEntity> { Success = true, Message = "Update Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Update Note Failed", Data = response });
            }


        }

        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult Trash(int NotesId)
        {

            var response = inotesManager.Trash(NotesId);
            if (response != null)
            {

                return Ok(new ResModel<NotesEntity> { Success = true, Message = "Trash Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Trash Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(int NotesId, int id)
        {

            try
            {
                int idd = Convert.ToInt32(User.FindFirst("userId").Value);
                var response = inotesManager.DeleteNote(NotesId, idd);
                if (response != null)
                {

                    return Ok(new ResModel<NotesEntity> { Success = true, Message = "Delete Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Delete Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult IsArchive(int NotesId)
        {
            try
            {
                var response = inotesManager.Archive(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NotesEntity> { Success = true, Message = "IsArchive Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "IsArchive Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public ActionResult IsPin(int NotesId)
        {
            try
            {
                var response = inotesManager.Pin(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NotesEntity> { Success = true, Message = "IsPin Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "IsPin Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Colour")]
        public ActionResult Colour(int NotesId)
        {

            var response = inotesManager.Colour(NotesId);
            if (response != null)
            {
                return Ok(new ResModel<NotesEntity> { Success = true, Message = "Colour Note Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Colour Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Remind")]
        public ActionResult Remind(int NotesId)
        {
            var response = inotesManager.Reminder(NotesId);
            if (response != null)
            {
                return Ok(new ResModel<NotesEntity> { Success = true, Message = "Reminder Note Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Reminder Note Failed", Data = response });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("UploadImage")]
        public ActionResult UploadImage(string filepath, int NotesId, int Id)
        {
            var response = inotesManager.UploadImage(filepath, NotesId, Id);
            if (response != null)
            {
                return Ok(new ResModel<string> { Success = true, Message = "Upload Image Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "Upload Image Failed", Data = response });
            }
        }

        [HttpGet]
        [Route ("Get notes")]
        public ActionResult Notes(int Id)
        {
            int id = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response= inotesManager.Notes(id);
                if (response != null)
                {
                    return Ok(new ResModel<NotesEntity> { Success = true, Message = "Upload Image Success", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<NotesEntity> { Success = false, Message = "Upload Image Failed", Data = response });
                }
        }*/
    }
}

