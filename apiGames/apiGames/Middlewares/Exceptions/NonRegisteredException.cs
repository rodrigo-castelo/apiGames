using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Middlewares.Exceptions
{
    public class NonRegisteredException : Exception
    {
        public NonRegisteredException() : base("We do not have a register for this game yet.") { }
    }
}
