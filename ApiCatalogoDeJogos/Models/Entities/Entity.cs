using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Models.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
