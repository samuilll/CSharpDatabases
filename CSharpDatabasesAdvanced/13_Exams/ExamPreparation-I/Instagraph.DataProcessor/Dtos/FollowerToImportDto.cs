﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagraph.DataProcessor.Dtos
{
   public class FollowerToImportDto
    {
        [Required]
        public string User{ get; set; }
        [Required]
        public string Follower { get; set; }
    }
}
