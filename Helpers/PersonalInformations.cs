using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationFormManager.PersonalInfoFields;

namespace ApplicationFormManager.Helpers
{
    public class PersonalInformations
    {
        public MandatoryField<string> FirstName { get; set; } = new MandatoryField<string>();
        public MandatoryField<string> LastName { get; set; } = new MandatoryField<string>();
        public MandatoryField<string> Email { get; set; } = new MandatoryField<string>();
        public NonMandatoryField<string> PhoneNumber { get; set; } = new NonMandatoryField<string>();
        public NonMandatoryField<string> Nationality { get; set; } = new NonMandatoryField<string>();
        public NonMandatoryField<string> CurrentResidence { get; set; } = new NonMandatoryField<string>();
        public NonMandatoryField<string> IdNumber { get; set; } = new NonMandatoryField<string>();
        public NonMandatoryField<DateTime> DateOfBirth { get; set; } = new NonMandatoryField<DateTime>();
        public NonMandatoryField<string> Gender { get; set; } = new NonMandatoryField<string>();
    }
}