using System;

namespace ApiCatalogoDeJogos.Models.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public double Price { get; set; }
    }
}
