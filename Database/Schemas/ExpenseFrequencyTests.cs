﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BudgetWatcher.Database.Schemas;

namespace BudgetWatcherTests.Database.Schemas
{
    [TestClass]
    public class ExpenseFrequencyTests
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
            ExpenseFrequency freq1 = new ExpenseFrequency("Lunara", 30);
            freq1.Insert();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);

            Assert.IsTrue(
                (freq1.Id == freq2.Id) &&
                (freq1.Name == freq2.Name) &&
                (freq1.Days == freq2.Days));
        }

        [TestMethod]
        public void Update()
        {
            ExpenseFrequency freq1 = new ExpenseFrequency("Random", 15);
            freq1.Insert();

            freq1.Name = "Lunara";
            freq1.Days = 30;
            freq1.Update();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);
            Assert.AreEqual(freq2.Name, "Lunara");
            Assert.AreEqual(freq2.Days, 30);
        }

        [TestMethod]
        public void Delete()
        {
            ExpenseFrequency freq1 = new ExpenseFrequency("Random", 10);
            freq1.Insert();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);
            freq2.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                ExpenseFrequency freq = new ExpenseFrequency(freq1.Id);
            });
        }
    }
}
