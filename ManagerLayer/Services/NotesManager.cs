using Common.RequestModels;
using Manager.Interfaces;
using Repository.Entity;
using Repository.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Services
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository repository;
        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }
        public NotesEntity CreateNote(CreateNotesModel model, int Id)
        {
            return repository.CreateNote(model, Id);
        }
        public List<NotesEntity> GetNote(int id)
        {
            return repository.GetNote(id);
        }
        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model)
        {
            return repository.UpdateNote(NotesId, model);
        }
        public NotesEntity Trash(int NotesId)
        {
            return repository.Trash(NotesId);
        }
        public NotesEntity DeleteNote(int NotesId, int id)
        {
            return repository.DeleteNote(NotesId, id);
        }
        public NotesEntity Archive(int NotesId)
        {
            return repository.Archive(NotesId);
        }
        public NotesEntity Pin(int NotesId)
        {
            return repository.Pin(NotesId);
        }
        public NotesEntity Colour(int NotesId, string colour)
        {
            return repository.Colour(NotesId, colour);
        }
        /*public NotesEntity Reminder(int NotesId)
        {
            return repository.Reminder(NotesId);
        }
        public string UploadImage(string filepath, int NotesId, int Id)
        {
            return repository.UploadImage(filepath, NotesId, Id);
        }

        public NotesEntity Notes(int noteid)
        {
            return repository.Notes(noteid);
        }*/
    }

}