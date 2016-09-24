using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SavingVariables.Models
{
    public class Variable
    {
        [Key]
        public int VariableId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
