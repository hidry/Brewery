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
                Active = true
            });
            Add(new MashStep()
            {
                Step = "1. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = true
            });
            Add(new MashStep()
            {
                Step = "2. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false
            });
            Add(new MashStep()
            {
                Step = "3. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false
            });
            Add(new MashStep()
            {
                Step = "4. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false
            });
            Add(new MashStep()
            {
                Step = "5. Rast",
                Temperature = 66.5,
                Rast = 90,
                Alert = false,
                Mixer = true,
                Active = false
            });
            Add(new MashStep()
            {
                Step = "Aufheizen",
                Temperature = 76,
                Rast = 0,
                Alert = true,
                Mixer = true,
                Active = true
            });
            Add(new MashStep()
            {
                Step = "Abmaischen",
                Temperature = 0,
                Rast = 60,
                Alert = false,
                Mixer = true,
                Active = true
            });
        }
    }
}