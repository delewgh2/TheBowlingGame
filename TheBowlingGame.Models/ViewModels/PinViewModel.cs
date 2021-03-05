using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBowlingGame.Models
{
    public class PinViewModel// : IValidatableObject
    {
        [DisplayName("Pins")]
        [Required(ErrorMessage = "Please enter a valid Pin count")]
        [Range(0, 10, ErrorMessage = "Pins can only be [0 - 10]")]
        public int Pin { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    ErrorMessage = (frame.RollCount == 1 && frame.Rolls.FirstRoll > 10) ||
        //                (frame.RollCount == 2 && frame.Rolls.SecondRoll > 10) ||
        //                (frame.RollCount == 3 && frame.Rolls.ExtraRoll > 10) ? "Pins can only be [0 - 10]" :
        //                (frame.RollCount == 2 && frame.Rolls.FirstRoll + frame.Rolls.SecondRoll > 10) ? $"Pin can only be {10 - frame.Rolls.FirstRoll}" :
        //                string.Empty
        //    if (Genre == Genre.Classic && ReleaseDate.Year > _classicYear)
        //    {
        //        yield return new ValidationResult(
        //            $"Classic movies must have a release year no later than {_classicYear}.",
        //            new[] { nameof(ReleaseDate) });
        //    }
        //}
    }
}
