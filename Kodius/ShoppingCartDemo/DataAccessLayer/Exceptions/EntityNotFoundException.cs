using ShoppingCartDemo.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer
{
    public class EntityNotFoundException : Exception
    {
        public readonly int Id;
        public readonly Type EntityType;

        public EntityNotFoundException(int Id, Type EntityType): base(string.Format("Entity not found for ID {0}, entity type {1}", Id, EntityType.ToString()))
        {
            this.Id = Id;
            this.EntityType = EntityType;
        }
    }
}
