using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NatCalculator
{
    class BoException : Exception
    {
        public BoException(String message)
            : base(message)
        {
            
        }

    }
}
