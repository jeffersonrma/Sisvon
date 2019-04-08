using System;

namespace Sisvon.BO.Validation
{
    public class BoException : Exception
    {
        public BoException()
        {
            
        }

        public BoException(string message)
            : base(message)
        {
            
        }
        public override string Message { get { return "Ocorreu um erro, " + base.Message; } }

    }
}
