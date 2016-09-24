using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VariablesRepository
    {
        // Defines a property 'Context' of type 'VariablesContext' 
        public VariablesContext Context { get; set; }

        public VariablesRepository()
        {
            // Constructor that will create instance of the new Context
            Context = new VariablesContext();
        }

        public VariablesRepository(VariablesContext _context)
        {
            Context = _context;
        }

        public List<Variable> GetVariables()
        {
            int i = 1;
            return Context.Variables.ToList();
        }

        public void AddVariable(Variable variable)
        {
            Context.Variables.Add(variable);
            Context.SaveChanges();
        }

        public void AddVariable(string name, int value)
        {
            Variable variable = new Variable { Name = name, Value = value };
            Context.Variables.Add(variable);
            Context.SaveChanges();
        }

    }
}
