using System;

namespace ApiCatalogoDeJogos.Exceptions
{
    public class GameAlreadyInDataBaseException : Exception
    {
        public GameAlreadyInDataBaseException():base("This game already exist.")
        {

        }
    }
}
