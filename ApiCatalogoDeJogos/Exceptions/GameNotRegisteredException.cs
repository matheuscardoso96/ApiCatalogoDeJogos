using System;

namespace ApiCatalogoDeJogos.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException():base("This game is not registered in database.")
        {

        }
    }
}
