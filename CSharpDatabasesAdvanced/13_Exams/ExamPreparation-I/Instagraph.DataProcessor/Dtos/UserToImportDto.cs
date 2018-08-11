using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagraph.DataProcessor.Dtos
{
   public class UserToImportDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public string ProfilePicture { get; set; }
    }
}
