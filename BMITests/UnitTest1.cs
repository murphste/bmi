using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMICalculator;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace BMITests
{
    [TestClass]
    public class UnitTest1
    {
        

            BMI b1 = new BMI();
            int min = 0;
            int max = 0;
            Regex regex;
            RangeAttribute validRange;
            double totalWeightInPounds;
            double totalWeightInStones;
            double totalWeightInKgs;
            double totalHeightInInches;
            double totalHeightInMetres;
            double bmi;
        
        


        /* PROPERTY SET Tests*/

        // -- Stones Property  
        [TestMethod]
        public void SetStonesProperty1_IsNotValid_FalseWillPass()
        {
            min = 5;
            max = 50;
            validRange = new RangeAttribute(min, max);
            

            // Set stones to invalid weight
            int value = b1.WeightStones = 4;

            Assert.IsFalse((validRange.IsValid(value)));
        }


        [TestMethod]
        public void SetStonesProperty2_IsNotValid_FalseWillPass ()
        {
            min = 0;
            max = 13;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.WeightStones = 51;

            Assert.IsFalse((validRange.IsValid(value)));
        }

        
        [TestMethod]
        public void SetStonesProperty3_IsValid_TrueWillPass()
        {
            min = 5;
            max = 50;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.WeightStones = 10;

            Assert.IsTrue((validRange.IsValid(value)));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckStonesProperty_AllowsOnlyPositiveIntNumbers_WillThrowException()
        {
            b1.WeightStones = -10;
        }



        // -- Pounds Property
        [TestMethod]
        public void SetPoundsProperty2_IsNotValid_FalseWillPass ()
        {
            min = 0;
            max = 13;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.WeightPounds = 14;

            Assert.IsFalse((validRange.IsValid(value)));
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetPoundsProperty3_AllowsOnlyPositiveIntNumbers_WillThrowException()
        {
            b1.WeightPounds = -10;
        }



        // -- Feet Property
        [TestMethod]
        public void SetFeetProperty1_IsNotValid_FalseWillPass()
        {
            min = 4;
            max = 7;
            validRange = new RangeAttribute(min, max);


            // Set stones to invalid weight
            int value = b1.HeightFeet = 3;

            Assert.IsFalse((validRange.IsValid(value)));
        }

        [TestMethod]
        public void SetFeetProperty2_IsNotValid_FalseWillPass()
        {
            min = 4;
            max = 7;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.HeightFeet = 3;

            Assert.IsFalse((validRange.IsValid(value)));
        }

        [TestMethod]
        public void SetFeetProperty3_IsNotValid_FalseWillPass()
        {
            min = 4;
            max = 7;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.HeightFeet = 8;

            Assert.IsFalse((validRange.IsValid(value)));
        }


        [TestMethod]
        public void SetStonesProperty4_IsValid_WillPass()
        {
            min = 4;
            max = 7;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.WeightStones = 5;

            Assert.IsTrue((validRange.IsValid(value)));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckFeetProperty_AllowsOnlyPositiveIntNumbers_WillThrowException()
        {
            b1.HeightFeet = -1;
        }


        // -- Inches Property
        [TestMethod]
        public void SetInchesProperty1_IsNotValid_FalseWillPass()
        {
            min = 0;
            max = 11;
            validRange = new RangeAttribute(min, max);


            // Set stones to invalid weight
            int value = b1.HeightInches = 12;

            Assert.IsFalse((validRange.IsValid(value)));
        }


        [TestMethod]
        public void SetInchesProperty2_IsValid_WillPass()
        {
            min = 0;
            max = 11;
            validRange = new RangeAttribute(min, max);

            // Set stones to invalid weight
            int value = b1.HeightInches = 10;

            Assert.IsTrue((validRange.IsValid(value)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckInchesProperty_AllowsOnlyPositiveIntNumbers_WillThrowException()
        {
            b1.HeightInches = -1;
        }


        // -- BMIValue Property

        [TestMethod]
        public void BMIValueProperty1_WeightinPoundsCalculation_EqualWillPass()
        {
            
            b1.WeightStones = 10;
            b1.WeightPounds = 5;
            // formula is (WeightStones * 14) + WeightPounds;
            double expected = 145.00;
            double actual = (b1.WeightStones * 14) + b1.WeightPounds;

            Assert.Equals(expected, actual);

        }









    }
}
