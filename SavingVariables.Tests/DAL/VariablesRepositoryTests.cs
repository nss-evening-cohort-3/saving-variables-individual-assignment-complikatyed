using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using SavingVariables.Models;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class VariablesRepositoryTests
    {

        Mock<VariablesContext> mock_context { get; set; }
        Mock<DbSet<Variable>> mock_variable_table { get; set; }
        List<Variable> variable_list { get; set; } // Fake
        VariablesRepository repo { get; set; }

        public void ConnectMocksToDatastore()
        {
            // Casting the list as something queryable
            var queryable_list = variable_list.AsQueryable();

            // Lie to LINQ make it think that our new Queryable List is a Database table.
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            // Have our Author property return our Queryable List AKA Fake database table.
            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            mock_variable_table.Setup(t => t.Add(It.IsAny<Variable>())).Callback((Variable v) => variable_list.Add(v));
            mock_variable_table.Setup(t => t.Remove(It.IsAny<Variable>())).Callback((Variable v) => variable_list.Remove(v));
        }

        [TestInitialize]
        public void Initialize()
        {
            // Create Mock VariablesContext
            mock_context = new Mock<VariablesContext>();
            mock_variable_table = new Mock<DbSet<Variable>>();
            variable_list = new List<Variable>(); // Fake
            repo = new VariablesRepository(mock_context.Object);

            ConnectMocksToDatastore();
        }

        [TestCleanup]
        public void TearDown()
        {
            repo = null; // 
        }

        [TestMethod]
        public void EnsureThereAreNoVariables()
        {
            List<Variable> actual_variables = repo.GetVariables();
            Assert.AreEqual(0, actual_variables.Count());
        }

        [TestMethod]
        public void EnsureInstantiateVariableAddsToVariablesList()
        {
            // Instantiates a new variable (thus adding it to the variables list)
            Variable my_variable = new Variable { Name = "x", Value = 9 };

            repo.AddVariable(my_variable);

            List<Variable> these_variables = repo.GetVariables();
            Assert.AreEqual(1, these_variables.Count());
        }

        [TestMethod]
        public void EnsureCanCreateRepoInstance()
        {
            VariablesRepository repo = new VariablesRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureRepoHasContext()
        {
            VariablesRepository repo = new VariablesRepository();

            VariablesContext actual_context = repo.Context;

            Assert.IsInstanceOfType(actual_context, typeof(VariablesContext));
        }

    }
}
