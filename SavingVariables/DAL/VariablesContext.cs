using System;
using System.Collections.Generic;
using System.Data.Entity;
using SavingVariables.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SavingVariables.DAL
{
    public class VariablesContext : DbContext
    {
        // 'virtual' keyword is needed to use Moq during testing
        public virtual DbSet<Variable> Variables { get; set; }
    }
}
