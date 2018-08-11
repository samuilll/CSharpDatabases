using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.ProcessData.Dtos
{
  public  class TrainDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        public string Type { get; set; }

        public virtual ICollection<ClassForTrainDto> Seats { get; set; }
    }
}
