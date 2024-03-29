﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetWatcher.Database.Schemas;
using System;

namespace BudgetWatcherTests.Database.Schemas
{
    [TestClass]
    public class IncomeTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            BudgetWatcher.Database.Manager.Instance.OpenOrCreateDatabase();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BudgetWatcher.Database.Manager.Instance.CloseDatabase();
        }

        [TestMethod]
        public void LoadFromId()
        {
            Income income1 = new Income("Salariu", 4000);
            income1.Insert();

            Income income2 = new Income(income1.Id);

            Assert.IsTrue(
                (income1.Id == income2.Id) && 
                (income1.Name == income2.Name) && 
                (income1.Value == income2.Value));
        }

        [TestMethod]
        public void Update()
        {
            Income income1 = new Income("Salariu", 2500);
            income1.Insert();

            income1.Value = 5000;
            income1.Update();

            Income income2 = new Income(income1.Id);
            Assert.AreEqual(income2.Value, 5000);
        }

        [TestMethod]
        public void Delete()
        {
            Income income1 = new Income("Test", 1200);
            income1.Insert();

            Income income2 = new Income(income1.Id);
            income2.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                Income income = new Income(income1.Id);
            });
        }
    }
}
