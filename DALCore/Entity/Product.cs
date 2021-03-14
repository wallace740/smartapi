using System;
using DALCore.Common;

namespace DALCore.Entity
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
