using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMICalculator;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace BMITests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BMIPropertiesTests
    {
        

        // Attrtibutes
        BMI b1;
        int min;
        int max;
        RangeAttribute validRange;
        int testVal;
        double expected;
        double actual;
        BMICategory? actualCategory;
        BMICategory? expectedCategory;
       

        const double UnderWeightUpperLimit = 18.4;              // inclusive upper limit
        const double NormalWeightUpperLimit = 24.9;
        const double OverWeightUpperLimit = 29.9;
        const double PoundsToKgs = 0.453592;
        const double InchestoMetres = 0.0254;

        [TestInitialize]
        public void Setup()
        {
            b1 = new BMI();
            min = 0;
            max = 0;
            validRange = new RangeAttribute(min, max);
            testVal = 0;
            expected = 0;
            actual = 0;
            actualCategory = null;
            expectedCategory = null;
        }


        [TestCleanup]
        public void TearDown()
        {
            min = 0;
            max = 0;
            b1 = null;
            validRange = null;
            testVal = 0;
            expected = 0;
            actual = 0;
            actualCategory = null;
            expectedCategory = null;
        }



        /* PROPERTY SET Tests*/
        
        // -- Stones Property  
        [TestMethod]
        public void SetStonesProperty1_IsNotValid_FalseWillPass()
        {
            min = 5;
            max = 50;

            // Set stones to invalid weight
            testVal = b1.WeightStones = 4;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetStonesProperty2_IsNotValid_FalseWillPass()
        {
            min = 5;
            max = 50;

            // Set stones to invalid weight
            testVal = b1.WeightStones = 51;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetStonesProperty3_AllowsOnlyPositiveIntNumbers_FalseWillPass()
        {
            min = 5;
            max = 50;
            // Set stones to negative amount
            testVal = b1.WeightStones = -10;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetStonesProperty4_IsValid_WillPass()
        {
            min = 4;
            max = 7;
            RangeAttribute validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            testVal = b1.WeightStones = 5;

            Assert.IsTrue((validRange.IsValid(testVal)));
        }




        // -- Pounds Property
        [TestMethod]
        public void SetPoundsProperty1_IsNotValid_FalseWillPass()
        {
            min = 0;
            max = 13;

            // Set pounds to invalid weight
            testVal = b1.WeightPounds = 14;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetPoundsProperty2_AllowsOnlyPositiveIntNumbers_FalseWillPass()
        {
            min = 0;
            max = 13;
            // Set pounds to negative amount
            testVal = b1.WeightPounds = -10;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetPoundsProperty3_IsValid_WillPass()
        {
            min = 0;
            max = 11;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            testVal = b1.HeightInches = 10;

            Assert.IsTrue((validRange.IsValid(testVal)));
        }




        // -- Feet Property
        [TestMethod]
        public void SetFeetProperty1_IsNotValid_FalseWillPass()
        {
            min = 4;
            max = 7;

            // Set feet to invalid amount
            testVal = b1.HeightFeet = 3;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetFeetProperty2_IsNotValid_FalseWillPass()
        {
            min = 4;
            max = 7;

            // Set feet to invalid amount
            testVal = b1.HeightFeet = 8;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }

        [TestMethod]
        public void SetFeetProperty3_IsValid_WillPass()
        {
            min = 4;
            max = 7;
            validRange = new RangeAttribute(min, max);

            // Set feet to invalid amount
            testVal = b1.HeightInches = 5;

            Assert.IsTrue((validRange.IsValid(testVal)));
        }


        [TestMethod]   
        public void SetFeetProperty4_AllowsOnlyPositiveIntNumbers_FalseWillPass()
        {
            min = 4;
            max = 7;
            // Set feet to negative amount
            testVal = b1.HeightFeet = -1;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }



        // -- Inches property
        [TestMethod]
        public void SetInchesProperty1_ValOutisdeRange_FalseWillPass()
        {
            min = 0;
            max = 11;
            
            // Set inches to invalid amount
            testVal = b1.HeightInches = 12;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetInchesProperty2_AllowsOnlyPositiveIntNumbers_FalseWillPass()
        {
            min = 0;
            max = 11;
            // Set inches to negative amount
            testVal = b1.HeightInches = -1;
            Assert.IsFalse((validRange.IsValid(testVal)));
        }


        [TestMethod]
        public void SetInchesProperty3_IsValid_WillPass()
        {
            min = 0;
            max = 11;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            testVal = b1.HeightInches = 10;

            Assert.IsTrue((validRange.IsValid(testVal)));
        }




        // -- BMIValue property
        [TestMethod]
        public void BMI_CalculatesCorrectly_EqualWillPass()
        {
            b1.WeightStones = 10;
            b1.WeightPounds = 5;
            b1.HeightFeet = 5;
            b1.HeightInches = 11;
 
            actual = b1.BMIValue;

            // Define expected value
            expected = 20.22339478354387;
            
            // Check accuracy to two decimal places
            actual = Math.Round(actual, 2);         // = 20.22
            expected = Math.Round(expected, 2);     // = 20.22

            Assert.AreEqual(expected, actual);
        }


        // -- BMI Category property
        [TestMethod]
        public void BMICategory_UnderweightCalcWorksCorrectly_EqualWillPass()
        {
            b1.WeightStones = 8;
            b1.WeightPounds = 5;
            b1.HeightFeet = 5;
            b1.HeightInches = 10;

            expectedCategory = BMICategory.Underweight;
            actualCategory = b1.BMICategory;

            Assert.AreEqual(expectedCategory, actualCategory);
        }


        [TestMethod]
        public void BMICategory_NormalCalcWorksCorrectly_EqualWillPass()
        {
            b1.WeightStones = 10;
            b1.WeightPounds = 11;
            b1.HeightFeet = 5;
            b1.HeightInches = 10;

            expectedCategory = BMICategory.Normal;
            actualCategory = b1.BMICategory;

            Assert.AreEqual(expectedCategory, actualCategory);
        }


        [TestMethod]
        public void BMICategory_OverweightCalcWorksCorrectly_EqualWillPass()
        {
            b1.WeightStones = 14;
            b1.WeightPounds = 1;
            b1.HeightFeet = 5;
            b1.HeightInches = 9;

            expectedCategory = BMICategory.Overweight;
            actualCategory = b1.BMICategory;

            Assert.AreEqual(expectedCategory, actualCategory);
        }



        [TestMethod]
        public void BMICategory_ObeseCalcWorksCorrectly_EqualWillPass()
        {
            b1.WeightStones = 16;
            b1.WeightPounds = 5;
            b1.HeightFeet = 5;
            b1.HeightInches = 9;

            expectedCategory = BMICategory.Obese;
            actualCategory = b1.BMICategory;

            Assert.AreEqual(expectedCategory, actualCategory);
        }

    }
}
