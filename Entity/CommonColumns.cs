using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CommonColumns
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
