using Common.RequestModels;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interfaces
{
    public interface INotesManager
    {
        public NotesEntity CreateNote(CreateNotesModel model, int Id);
        public List<NotesEntity> GetNote(int id);
        /*public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model);
        public NotesEntity Trash(int NotesId);
        public NotesEntity DeleteNote(int NotesId, int id);
        public NotesEntity Archive(int NotesId);
        public NotesEntity Pin(int NotesId);
        public NotesEntity Colour(int NotesId);
        public NotesEntity Reminder(int NotesId);
        public string UploadImage(string filepath, int NotesId, int Id);

        public NotesEntity Notes(int noteid);*/
    }

}