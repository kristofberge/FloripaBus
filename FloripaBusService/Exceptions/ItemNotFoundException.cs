using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FloripaBusService.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public enum ItemType
        {
            Route, Street, Departure, Unknown
        }

        public ItemType Type { get; set; }

        public ItemNotFoundException()
        {
            Type = ItemType.Unknown;
        }

        public ItemNotFoundException(ItemType elementType)
        {
            this.Type = elementType;
        }
    }
}
