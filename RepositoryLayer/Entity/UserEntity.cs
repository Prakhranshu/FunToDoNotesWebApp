using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserId { get; set; }
        [Required]

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Useremail { get; set; }

        public string Userpassword { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
