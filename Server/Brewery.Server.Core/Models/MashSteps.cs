using System.Collections.Generic;

namespace Brewery.Server.Core.Models
{
    public class MashSteps : List<MashStep>
    {
        public MashSteps()
        {
            Add(new MashStep()
            {
                Step = "Aufheizen & Einmaischen",
                Temperature = 70,
                Rast = 0,
                Alert = true,
                Mixer = true,
                Active = true,
                Guid = "AF113411-29CD-4876-91A7-D12187FC1E5F"
            });
            Add(new MashStep()
            {
                Step = "1. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = true,
                Guid = "B9AA3601-54C6-4AC3-8108-328B43B4D32C"
            });
            Add(new MashStep()
            {
                Step = "2. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false,
                Guid = "896559FD-D480-4325-A402-CCC57295AAA6"
            });
            Add(new MashStep()
            {
                Step = "3. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false,
                Guid = "7C171068-DFA8-459C-B72A-B01B3E2ECAE7"
            });
            Add(new MashStep()
            {
                Step = "4. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false,
                Guid = "E5F7A750-5A46-46A2-87A1-4054ED77F63F"
            });
            Add(new MashStep()
            {
                Step = "5. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false,
                Guid = "9A74CBA0-4B2E-448B-8CDF-1C476C26570B"
            });
            Add(new MashStep()
            {
                Step = "Aufheizen",
                Temperature = 76,
                Rast = 0,
                Alert = true,
                Mixer = true,
                Active = true,
                Guid = "C587ECB2-EE3E-4E70-B132-DDE2EBED0170"
            });
            Add(new MashStep()
            {
                Step = "Abmaischen",
                Temperature = 0,
                Rast = 60,
                Alert = false,
                Mixer = true,
                Active = true,
                Guid = "4D77D393-931D-46F0-9679-B03B9C8AD91F"
            });
        }
    }
}