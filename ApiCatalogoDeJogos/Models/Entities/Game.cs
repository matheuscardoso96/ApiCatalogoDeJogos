using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
