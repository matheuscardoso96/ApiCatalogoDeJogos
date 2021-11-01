using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Exceptions
{
    public class GameAlreadyInDataBaseException : Exception
    {
        public GameAlreadyInDataBaseException():base("This game already exist.")
        {

        }
    }
}
