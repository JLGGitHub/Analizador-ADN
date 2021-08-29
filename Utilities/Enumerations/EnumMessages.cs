using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utilities.Enumerations
{
    public enum EnumMessages
    {
        [Description("Felicidades, es un Mutante")]
        isMutant = 1,
        [Description("Lo sentimos, es un Humano")]
        isHuman = 2,

    }
}
