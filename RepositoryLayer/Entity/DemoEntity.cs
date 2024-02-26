using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class DemoEntity
    {
        [Key]
        public int DemoId { get; set; }

        [Required]
        public string DemoName { get; set; }

        public string DemoAddress { get; set; }

        public string DemoCity { get; set; }

    }
}
