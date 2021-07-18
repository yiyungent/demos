using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public partial class Person
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public int Age { get; set; }
    }
}
