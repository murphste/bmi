﻿// model classes for BMI calculator
// GC

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BMICalculator
{
    public enum BMICategory { Underweight, Normal, Overweight, Obese };

    public class BMI
    {
        const double UnderWeightUpperLimit = 18.4;              // inclusive upper limit
        const double NormalWeightUpperLimit = 24.9;
        const double OverWeightUpperLimit = 29.9;               // Obese from 30 +

        // conversion factors from imperial to metric
        const double PoundsToKgs = 0.453592;
        const double InchestoMetres = 0.0254;

        //Regex onlyNums = new Regex("[^0 - 9] +");


        //Unit tests for these
        [Display(Name = "Weight - Stones")]
        [Range(5, 50, ErrorMessage = "Stones must be between 5 and 50")]                              // max 50 stone
        public int WeightStones
        {
            get
            {
                return WeightStones;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number must be positive");
                }
            }
        }

        [Display(Name = "Pounds")]
        [Range(0, 13, ErrorMessage = "Pounds must be between 0 and 13")]                              // 14 lbs in a stone
        public int WeightPounds 
        {
            get
            {
                return WeightPounds;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number must be positive");
}
            }
        }

        [Display(Name = "Height - Feet")]
        [Range(4, 7, ErrorMessage = "Feet must be between 4 and 7")]                               // max 7 feet
        public int HeightFeet
        {
            get
            {
                return HeightFeet;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number must be positive");
                }
            }
        }
        

        [Display(Name = "Inches")]
        [Range(0, 11, ErrorMessage = "Inches must be between 0 and 11")]                              // 12 inches in a foot
        public int HeightInches
        {
            get
            {
                return HeightInches;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number must be positive");
                }
            }
        }

        // calculate BMI, display to 2 decimal places
        [Display(Name = "Your BMI is")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BMIValue
        {
            get
            {
                // bmi = weight in Kgs / height in metres squared

                double totalWeightInPounds = (WeightStones * 14) + WeightPounds;
                double totalHeightInInches = (HeightFeet * 12) + HeightInches;

                // do conversions to metric
                double totalWeightInKgs = totalWeightInPounds * PoundsToKgs;
                double totalHeightInMetres = totalHeightInInches * InchestoMetres;

                double bmi = totalWeightInKgs / (Math.Pow(totalHeightInMetres, 2));

                return bmi;
            }
        }

        // calculate BMI category 
        [Display(Name = "Your BMI Category is")]
        public BMICategory BMICategory
        {
            get
            {
                double bmi = this.BMIValue;

                // Unit test these if conditions
                // calculate BMI category based on upper limits
                if (bmi <= UnderWeightUpperLimit)
                {
                    return BMICategory.Underweight;
                }
                else if (bmi <= NormalWeightUpperLimit)
                {
                    return BMICategory.Normal;
                }
                else if (bmi <= OverWeightUpperLimit)
                {
                    return BMICategory.Overweight;
                }
                else
                {
                    return BMICategory.Obese;
                }
            }
        }
    }
}

