using System;
using zs.nn.NoshNavigatorServices.Application.Enumerations;

namespace zs.nn.NoshNavigatorServices.Application.Exceptions
{
    public class NoshNavigatorException : Exception
    {
        public NoshNavigatorException(ApplicationConcern concern, ApplicationRules rule, string message) : base(message) { }

        public NoshNavigatorException(ApplicationConcern concern, ApplicationRules rule, string message, Exception innerException) : base(message, innerException) { }
    }
}
