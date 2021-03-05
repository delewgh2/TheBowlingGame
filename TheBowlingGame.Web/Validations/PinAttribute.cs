using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheBowlingGame.Models;
using TheBowlingGame.Models.ViewModels;

namespace TheBowlingGame.Web.Validations
{
    public class PinAttribute : ValidationAttribute
    {
        private int input;
        private Frame frame;
        public PinAttribute(int pin)
        {
            Pin = pin;
        }
        public int Pin { get; }

        public string GetErrorMessage() =>
            (frame.RollCount == 1 && input > 10) ||
            (frame.RollCount == 2 && input > 10) ||
            (frame.RollCount == 3 && input > 10) ? "Pins can only be [0 - 10]" :
            (frame.RollCount == 2 && frame.Rolls.FirstRoll + input > 10) ? $"Pin can only be {10 - input}" :
            string.Empty;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            input = (int)value;
            frame = (Frame)validationContext.ObjectInstance;
            
            if ((frame.RollCount == 1 && input > 10) ||
                (frame.RollCount == 2 && input > 10) ||
                (frame.RollCount == 3 && input > 10) ||
                (frame.RollCount == 2 && frame.Rolls.FirstRoll + input > 10))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
