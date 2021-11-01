using System;

namespace ApiCatalogoDeJogos.Models.Entities
{
    public class Game : Entity
    {
        public string Name { get; set; }
        public string Developer { get; set; }
        public double Price { get; set; }

        public Game(Guid id, string name, string developer, double price)
        {
            Id = id;
            Name = name;
            Developer = developer;
            Price = price;
            
        }

        public Game()
        {

        }
    }
}
