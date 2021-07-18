using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreUseMySqlEFCore
{
    public class Data
    {
        [Key]
        public int Id { get; set; }
        public string Annotation { get; set; }
    }
}
