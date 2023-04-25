using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Store_management_App.Modules
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Stock { get; set; }

        // Foreign key for Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        // Foreign key for Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"Product: {Name}, {Stock}";
        }

    }

}
