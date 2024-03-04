using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Common.RequestModels;
using Repository.Entity;
using Repository.Interfaces;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.RequestModels;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private readonly FunNoteContext context;
        public NotesRepository(FunNoteContext context)
        {
            this.context = context;
        }
        //Create Note
        public NotesEntity CreateNote(CreateNotesModel model, int UserID)
        {
            try
            {
                var user = context.UserTable.FirstOrDefault(x=>x.UserId == UserID);
                if(user == null)
                {
                    throw new Exception("User does not exist");
                }
                NotesEntity notesentity = new NotesEntity();
                notesentity.UserId = UserID;
                notesentity.Title = model.Title;
                notesentity.Description = model.Description;
                notesentity.Reminder = model.Reminder;
                notesentity.Colour = model.Colour;
                notesentity.Image = model.Image;
                notesentity.IsArchive = model.IsArchive;
                notesentity.IsPin = model.IsPin;
                notesentity.IsTrash = model.IsTrash;
                notesentity.CreatedAt = DateTime.UtcNow;
                notesentity.UpdatedAt = DateTime.UtcNow;

                context.NotesTable.Add(notesentity);
                context.SaveChanges();
                return notesentity;
            }
            catch
            {
                throw new Exception("Error in Saving");
            }
            
        }

        
        //Get Note
        public List<NotesEntity> GetNote(int id)
        {
            return context.NotesTable.Where<NotesEntity>(a => a.UserId == id).ToList();
        }
        
        /*//Update Note
        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model)
        {
            var noteToUpdate = context.NotesTable.FirstOrDefault(note => note.NotesId == NotesId);
            if (noteToUpdate != null)
            {
                noteToUpdate.Title = model.Title;
                noteToUpdate.Description = model.Description;
                noteToUpdate.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
                return noteToUpdate;

            }
            return null;
        }

        //Trash Note
        public NotesEntity Trash(int NotesId)
        {
            var trash = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (trash != null)
            {
                if (trash.IsTrash)
                {
                    trash.IsTrash = false;
                    context.SaveChanges();

                }
                else
                {
                    trash.IsTrash = true;
                }
            }
            return trash;

        }
        //Delete Note
        public NotesEntity DeleteNote(int NotesId, int id)
        {
            var del = context.NotesTable.FirstOrDefault(o => (o.NotesId == NotesId && o.UserId == id));
            if (del != null)
            {
                context.NotesTable.Remove(del);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Wrong DATA");
            }
            return del;
        }
        //Is Archive
        public NotesEntity Archive(int NotesId)
        {
            var archive = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (archive != null)
            {
                if (archive.IsArchive)
                {
                    archive.IsArchive = false;
                    context.SaveChanges();
                }
                else
                {
                    archive.IsArchive = true;
                }
                return archive;
            }
            else
            {
                throw new Exception("IsArchive not found");
            }

        }
        //Is Pin
        public NotesEntity Pin(int NotesId)
        {
            var pin = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (pin != null)
            {
                if (pin.IsPin)
                {
                    pin.IsPin = false;
                    context.SaveChanges();
                }
                else
                {
                    pin.IsPin = true;
                }
                return pin;
            }
            else
            {
                throw new Exception("IsPin not found");
            }

        }
        //Colour
        public NotesEntity Colour(int NotesId)
        {
            var color = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (color != null)
            {
                color.Colour = "Blue";
                context.SaveChanges();
            }
            return color;

        }
        //Reminder
        public NotesEntity Reminder(int NotesId)
        {
            var remind = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (remind != null)
            {
                remind.Reminder = DateTime.UtcNow;
                context.SaveChanges();
            }
            return remind;
        }
        //Image
        public string UploadImage(string filepath, int NotesId, int Id)
        {
            try
            {
                var filter = context.NotesTable.Where(e => e.NotesId == Id);
                if (filter != null)
                {
                    var findNotes = filter.FirstOrDefault(e => e.NotesId == NotesId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dygoi0kzf", "822117938224726", "***************************");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filepath),
                            PublicId = findNotes.Title
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        context.SaveChanges();
                        return "Upload Successfull";
                    }
                    return null;
                }
                else { return null; }

            }
            catch (Exception) { return null; }

        }
        //AddLable
        public LabelEntity AddLabel(int userid, AddLableModel label)
        {
            var add = context.NotesTable.FirstOrDefault(o => (o.NotesId == label.NoteId && o.Id == userid));
            LabelEntity note = new LabelEntity();
            if (add != null)
            {

                note.LabelName = label.LabelName;
                note.NoteId = label.NoteId;
                note.UserId = userid;
                context.LableTable.Add(note);
                context.SaveChanges();
                return note;
            }
            throw new Exception();
        }

        public NotesEntity Notes(int noteid)
        {
            var notes=context.NotesTable.FirstOrDefault(o => (o.NotesId==noteid));
            if(notes != null)
            {
                return notes;
            }
            throw new Exception("Notes does not exist");
        }*/
        
    }
}
