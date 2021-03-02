using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBowlingGame.Models
{
    public class ValidateModel
    {
        [DisplayName("Pins")]
        [Required(ErrorMessage = "Please enter a valid Pin count")]
        [Range(0, 10, ErrorMessage = "Pins can only be [0 - 10]")]
        public int Roll { get; set; }
    }
}
