using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.ProcessData.Dtos
{
    public class ClassDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:2,MinimumLength =2)]
        public string Abbreviation { get; set; }

    }
}
