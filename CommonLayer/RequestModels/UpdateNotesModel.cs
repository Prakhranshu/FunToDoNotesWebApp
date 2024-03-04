using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModels
{
    public class UpdateNotesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}