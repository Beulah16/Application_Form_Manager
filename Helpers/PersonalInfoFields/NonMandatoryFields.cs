using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationFormManager.PersonalInfoFields
{
    public class NonMandatoryField<T>
    {
        public bool IsInternal { get; set; }
        public bool IsHidden { get; set; }
    }
}