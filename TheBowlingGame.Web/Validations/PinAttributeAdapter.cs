using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace TheBowlingGame.Web.Validations
{
    public class PinAttributeAdapter : AttributeAdapterBase<PinAttribute>
    {
        public PinAttributeAdapter(PinAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-classicmovie", GetErrorMessage(context));

            var year = Attribute.Pin.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-classicmovie-year", year);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            Attribute.GetErrorMessage();
    }
}
