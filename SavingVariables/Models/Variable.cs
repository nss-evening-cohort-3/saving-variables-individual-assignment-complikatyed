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

        [MaxLength(length: 1, ErrorMessage = "Choose one character, please.")]
        public int Value { get; set; }
    }
}
