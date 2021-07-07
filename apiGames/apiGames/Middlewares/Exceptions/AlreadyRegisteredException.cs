using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Middlewares.Exceptions
{
    public class AlreadyRegisteredException : Exception
    {
        public AlreadyRegisteredException() : base("We already have a register for this game.") { }
    }
}
