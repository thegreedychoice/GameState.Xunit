﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

// Custom Attributes
namespace GameEngine.Tests
{
    public class HealthDamageDataAttribute: DataAttribute
    {
        //public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        //{
        //    yield return new object[] { 0, 100 };
        //    yield return new object[] { 1, 99 };
        //    yield return new object[] { 50, 50 };
        //    yield return new object[] { 75, 25 };
        //    yield return new object[] { 95, 5 };
        //    yield return new object[] { 101, 1 };
        //}

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            string[] csvLines = File.ReadAllLines("TestData.csv");

            var testCases = new List<object[]>();

            foreach (var csvLine in csvLines)
            {
                IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);
                object[] testCase = values.Cast<object>().ToArray();
                testCases.Add(testCase);
            }
            return testCases;
        }
    }
}
