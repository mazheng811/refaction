using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorMeNetCore.Exceptions
{
    public class OptionNotFoundException : Exception
    {
        public OptionNotFoundException() : base("Option was not found.")
        {
        }
    }
}
