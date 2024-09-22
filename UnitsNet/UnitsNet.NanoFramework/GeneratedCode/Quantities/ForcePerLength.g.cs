//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by \generate-code.bat.
//
//     Changes to this file will be lost when the code is regenerated.
//     The build server regenerates the code before each build and a pre-build
//     step will regenerate the code on each local build.
//
//     See https://github.com/angularsen/UnitsNet/wiki/Adding-a-New-Unit for how to add or edit units.
//
//     Add CustomCode\Quantities\MyQuantity.extra.cs files to add code to generated quantities.
//     Add UnitDefinitions\MyQuantity.json and run generate-code.bat to generate new units or quantities.
//
// </auto-generated>
//------------------------------------------------------------------------------

// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System;
using UnitsNet.Units;

namespace UnitsNet
{
    /// <inheritdoc />
    /// <summary>
    ///     The magnitude of force per unit length.
    /// </summary>
    public struct  ForcePerLength
    {
        /// <summary>
        ///     The numeric value this quantity was constructed with.
        /// </summary>
        private readonly double _value;

        /// <summary>
        ///     The unit this quantity was constructed with.
        /// </summary>
        private readonly ForcePerLengthUnit _unit;

        /// <summary>
        ///     The numeric value this quantity was constructed with.
        /// </summary>
        public double Value => _value;

        /// <inheritdoc />
        public ForcePerLengthUnit Unit => _unit;

        /// <summary>
        ///     Creates the quantity with the given numeric value and unit.
        /// </summary>
        /// <param name="value">The numeric value to construct this quantity with.</param>
        /// <param name="unit">The unit representation to construct this quantity with.</param>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public ForcePerLength(double value, ForcePerLengthUnit unit)
        {
            _value = value;
            _unit = unit;
        }

        /// <summary>
        ///     The base unit of ForcePerLength, which is Second. All conversions go via this value.
        /// </summary>
        public static ForcePerLengthUnit BaseUnit { get; } = ForcePerLengthUnit.NewtonPerMeter;

        /// <summary>
        /// Represents the largest possible value of ForcePerLength.
        /// </summary>
        public static ForcePerLength MaxValue { get; } = new ForcePerLength(double.MaxValue, BaseUnit);

        /// <summary>
        /// Represents the smallest possible value of ForcePerLength.
        /// </summary>
        public static ForcePerLength MinValue { get; } = new ForcePerLength(double.MinValue, BaseUnit);

        /// <summary>
        ///     Gets an instance of this quantity with a value of 0 in the base unit Second.
        /// </summary>
        public static ForcePerLength Zero { get; } = new ForcePerLength(0, BaseUnit);
        #region Conversion Properties

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.CentinewtonPerCentimeter"/>
        /// </summary>
        public double CentinewtonsPerCentimeter => As(ForcePerLengthUnit.CentinewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.CentinewtonPerMeter"/>
        /// </summary>
        public double CentinewtonsPerMeter => As(ForcePerLengthUnit.CentinewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.CentinewtonPerMillimeter"/>
        /// </summary>
        public double CentinewtonsPerMillimeter => As(ForcePerLengthUnit.CentinewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecanewtonPerCentimeter"/>
        /// </summary>
        public double DecanewtonsPerCentimeter => As(ForcePerLengthUnit.DecanewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecanewtonPerMeter"/>
        /// </summary>
        public double DecanewtonsPerMeter => As(ForcePerLengthUnit.DecanewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecanewtonPerMillimeter"/>
        /// </summary>
        public double DecanewtonsPerMillimeter => As(ForcePerLengthUnit.DecanewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecinewtonPerCentimeter"/>
        /// </summary>
        public double DecinewtonsPerCentimeter => As(ForcePerLengthUnit.DecinewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecinewtonPerMeter"/>
        /// </summary>
        public double DecinewtonsPerMeter => As(ForcePerLengthUnit.DecinewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.DecinewtonPerMillimeter"/>
        /// </summary>
        public double DecinewtonsPerMillimeter => As(ForcePerLengthUnit.DecinewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilogramForcePerCentimeter"/>
        /// </summary>
        public double KilogramsForcePerCentimeter => As(ForcePerLengthUnit.KilogramForcePerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilogramForcePerMeter"/>
        /// </summary>
        public double KilogramsForcePerMeter => As(ForcePerLengthUnit.KilogramForcePerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilogramForcePerMillimeter"/>
        /// </summary>
        public double KilogramsForcePerMillimeter => As(ForcePerLengthUnit.KilogramForcePerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilonewtonPerCentimeter"/>
        /// </summary>
        public double KilonewtonsPerCentimeter => As(ForcePerLengthUnit.KilonewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilonewtonPerMeter"/>
        /// </summary>
        public double KilonewtonsPerMeter => As(ForcePerLengthUnit.KilonewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilonewtonPerMillimeter"/>
        /// </summary>
        public double KilonewtonsPerMillimeter => As(ForcePerLengthUnit.KilonewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilopoundForcePerFoot"/>
        /// </summary>
        public double KilopoundsForcePerFoot => As(ForcePerLengthUnit.KilopoundForcePerFoot);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.KilopoundForcePerInch"/>
        /// </summary>
        public double KilopoundsForcePerInch => As(ForcePerLengthUnit.KilopoundForcePerInch);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MeganewtonPerCentimeter"/>
        /// </summary>
        public double MeganewtonsPerCentimeter => As(ForcePerLengthUnit.MeganewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MeganewtonPerMeter"/>
        /// </summary>
        public double MeganewtonsPerMeter => As(ForcePerLengthUnit.MeganewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MeganewtonPerMillimeter"/>
        /// </summary>
        public double MeganewtonsPerMillimeter => As(ForcePerLengthUnit.MeganewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MicronewtonPerCentimeter"/>
        /// </summary>
        public double MicronewtonsPerCentimeter => As(ForcePerLengthUnit.MicronewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MicronewtonPerMeter"/>
        /// </summary>
        public double MicronewtonsPerMeter => As(ForcePerLengthUnit.MicronewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MicronewtonPerMillimeter"/>
        /// </summary>
        public double MicronewtonsPerMillimeter => As(ForcePerLengthUnit.MicronewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MillinewtonPerCentimeter"/>
        /// </summary>
        public double MillinewtonsPerCentimeter => As(ForcePerLengthUnit.MillinewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MillinewtonPerMeter"/>
        /// </summary>
        public double MillinewtonsPerMeter => As(ForcePerLengthUnit.MillinewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.MillinewtonPerMillimeter"/>
        /// </summary>
        public double MillinewtonsPerMillimeter => As(ForcePerLengthUnit.MillinewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NanonewtonPerCentimeter"/>
        /// </summary>
        public double NanonewtonsPerCentimeter => As(ForcePerLengthUnit.NanonewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NanonewtonPerMeter"/>
        /// </summary>
        public double NanonewtonsPerMeter => As(ForcePerLengthUnit.NanonewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NanonewtonPerMillimeter"/>
        /// </summary>
        public double NanonewtonsPerMillimeter => As(ForcePerLengthUnit.NanonewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NewtonPerCentimeter"/>
        /// </summary>
        public double NewtonsPerCentimeter => As(ForcePerLengthUnit.NewtonPerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NewtonPerMeter"/>
        /// </summary>
        public double NewtonsPerMeter => As(ForcePerLengthUnit.NewtonPerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.NewtonPerMillimeter"/>
        /// </summary>
        public double NewtonsPerMillimeter => As(ForcePerLengthUnit.NewtonPerMillimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.PoundForcePerFoot"/>
        /// </summary>
        public double PoundsForcePerFoot => As(ForcePerLengthUnit.PoundForcePerFoot);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.PoundForcePerInch"/>
        /// </summary>
        public double PoundsForcePerInch => As(ForcePerLengthUnit.PoundForcePerInch);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.PoundForcePerYard"/>
        /// </summary>
        public double PoundsForcePerYard => As(ForcePerLengthUnit.PoundForcePerYard);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.TonneForcePerCentimeter"/>
        /// </summary>
        public double TonnesForcePerCentimeter => As(ForcePerLengthUnit.TonneForcePerCentimeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.TonneForcePerMeter"/>
        /// </summary>
        public double TonnesForcePerMeter => As(ForcePerLengthUnit.TonneForcePerMeter);

        /// <summary>
        ///     Gets a <see cref="double"/> value of this quantity converted into <see cref="ForcePerLengthUnit.TonneForcePerMillimeter"/>
        /// </summary>
        public double TonnesForcePerMillimeter => As(ForcePerLengthUnit.TonneForcePerMillimeter);

        #endregion

        #region Static Factory Methods

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.CentinewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromCentinewtonsPerCentimeter(double centinewtonspercentimeter) => new ForcePerLength(centinewtonspercentimeter, ForcePerLengthUnit.CentinewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.CentinewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromCentinewtonsPerMeter(double centinewtonspermeter) => new ForcePerLength(centinewtonspermeter, ForcePerLengthUnit.CentinewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.CentinewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromCentinewtonsPerMillimeter(double centinewtonspermillimeter) => new ForcePerLength(centinewtonspermillimeter, ForcePerLengthUnit.CentinewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecanewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecanewtonsPerCentimeter(double decanewtonspercentimeter) => new ForcePerLength(decanewtonspercentimeter, ForcePerLengthUnit.DecanewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecanewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecanewtonsPerMeter(double decanewtonspermeter) => new ForcePerLength(decanewtonspermeter, ForcePerLengthUnit.DecanewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecanewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecanewtonsPerMillimeter(double decanewtonspermillimeter) => new ForcePerLength(decanewtonspermillimeter, ForcePerLengthUnit.DecanewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecinewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecinewtonsPerCentimeter(double decinewtonspercentimeter) => new ForcePerLength(decinewtonspercentimeter, ForcePerLengthUnit.DecinewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecinewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecinewtonsPerMeter(double decinewtonspermeter) => new ForcePerLength(decinewtonspermeter, ForcePerLengthUnit.DecinewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.DecinewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromDecinewtonsPerMillimeter(double decinewtonspermillimeter) => new ForcePerLength(decinewtonspermillimeter, ForcePerLengthUnit.DecinewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilogramForcePerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilogramsForcePerCentimeter(double kilogramsforcepercentimeter) => new ForcePerLength(kilogramsforcepercentimeter, ForcePerLengthUnit.KilogramForcePerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilogramForcePerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilogramsForcePerMeter(double kilogramsforcepermeter) => new ForcePerLength(kilogramsforcepermeter, ForcePerLengthUnit.KilogramForcePerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilogramForcePerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilogramsForcePerMillimeter(double kilogramsforcepermillimeter) => new ForcePerLength(kilogramsforcepermillimeter, ForcePerLengthUnit.KilogramForcePerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilonewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilonewtonsPerCentimeter(double kilonewtonspercentimeter) => new ForcePerLength(kilonewtonspercentimeter, ForcePerLengthUnit.KilonewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilonewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilonewtonsPerMeter(double kilonewtonspermeter) => new ForcePerLength(kilonewtonspermeter, ForcePerLengthUnit.KilonewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilonewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilonewtonsPerMillimeter(double kilonewtonspermillimeter) => new ForcePerLength(kilonewtonspermillimeter, ForcePerLengthUnit.KilonewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilopoundForcePerFoot"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilopoundsForcePerFoot(double kilopoundsforceperfoot) => new ForcePerLength(kilopoundsforceperfoot, ForcePerLengthUnit.KilopoundForcePerFoot);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.KilopoundForcePerInch"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromKilopoundsForcePerInch(double kilopoundsforceperinch) => new ForcePerLength(kilopoundsforceperinch, ForcePerLengthUnit.KilopoundForcePerInch);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MeganewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMeganewtonsPerCentimeter(double meganewtonspercentimeter) => new ForcePerLength(meganewtonspercentimeter, ForcePerLengthUnit.MeganewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MeganewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMeganewtonsPerMeter(double meganewtonspermeter) => new ForcePerLength(meganewtonspermeter, ForcePerLengthUnit.MeganewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MeganewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMeganewtonsPerMillimeter(double meganewtonspermillimeter) => new ForcePerLength(meganewtonspermillimeter, ForcePerLengthUnit.MeganewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MicronewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMicronewtonsPerCentimeter(double micronewtonspercentimeter) => new ForcePerLength(micronewtonspercentimeter, ForcePerLengthUnit.MicronewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MicronewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMicronewtonsPerMeter(double micronewtonspermeter) => new ForcePerLength(micronewtonspermeter, ForcePerLengthUnit.MicronewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MicronewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMicronewtonsPerMillimeter(double micronewtonspermillimeter) => new ForcePerLength(micronewtonspermillimeter, ForcePerLengthUnit.MicronewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MillinewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMillinewtonsPerCentimeter(double millinewtonspercentimeter) => new ForcePerLength(millinewtonspercentimeter, ForcePerLengthUnit.MillinewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MillinewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMillinewtonsPerMeter(double millinewtonspermeter) => new ForcePerLength(millinewtonspermeter, ForcePerLengthUnit.MillinewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.MillinewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromMillinewtonsPerMillimeter(double millinewtonspermillimeter) => new ForcePerLength(millinewtonspermillimeter, ForcePerLengthUnit.MillinewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NanonewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNanonewtonsPerCentimeter(double nanonewtonspercentimeter) => new ForcePerLength(nanonewtonspercentimeter, ForcePerLengthUnit.NanonewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NanonewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNanonewtonsPerMeter(double nanonewtonspermeter) => new ForcePerLength(nanonewtonspermeter, ForcePerLengthUnit.NanonewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NanonewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNanonewtonsPerMillimeter(double nanonewtonspermillimeter) => new ForcePerLength(nanonewtonspermillimeter, ForcePerLengthUnit.NanonewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NewtonPerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNewtonsPerCentimeter(double newtonspercentimeter) => new ForcePerLength(newtonspercentimeter, ForcePerLengthUnit.NewtonPerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NewtonPerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNewtonsPerMeter(double newtonspermeter) => new ForcePerLength(newtonspermeter, ForcePerLengthUnit.NewtonPerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.NewtonPerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromNewtonsPerMillimeter(double newtonspermillimeter) => new ForcePerLength(newtonspermillimeter, ForcePerLengthUnit.NewtonPerMillimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.PoundForcePerFoot"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromPoundsForcePerFoot(double poundsforceperfoot) => new ForcePerLength(poundsforceperfoot, ForcePerLengthUnit.PoundForcePerFoot);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.PoundForcePerInch"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromPoundsForcePerInch(double poundsforceperinch) => new ForcePerLength(poundsforceperinch, ForcePerLengthUnit.PoundForcePerInch);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.PoundForcePerYard"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromPoundsForcePerYard(double poundsforceperyard) => new ForcePerLength(poundsforceperyard, ForcePerLengthUnit.PoundForcePerYard);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.TonneForcePerCentimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromTonnesForcePerCentimeter(double tonnesforcepercentimeter) => new ForcePerLength(tonnesforcepercentimeter, ForcePerLengthUnit.TonneForcePerCentimeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.TonneForcePerMeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromTonnesForcePerMeter(double tonnesforcepermeter) => new ForcePerLength(tonnesforcepermeter, ForcePerLengthUnit.TonneForcePerMeter);

        /// <summary>
        ///     Creates a <see cref="ForcePerLength"/> from <see cref="ForcePerLengthUnit.TonneForcePerMillimeter"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is NaN or Infinity.</exception>
        public static ForcePerLength FromTonnesForcePerMillimeter(double tonnesforcepermillimeter) => new ForcePerLength(tonnesforcepermillimeter, ForcePerLengthUnit.TonneForcePerMillimeter);

        /// <summary>
        ///     Dynamically convert from value and unit enum <see cref="ForcePerLengthUnit" /> to <see cref="ForcePerLength" />.
        /// </summary>
        /// <param name="value">Value to convert from.</param>
        /// <param name="fromUnit">Unit to convert from.</param>
        /// <returns>ForcePerLength unit value.</returns>
        public static ForcePerLength From(double value, ForcePerLengthUnit fromUnit)
        {
            return new ForcePerLength(value, fromUnit);
        }

        #endregion

                #region Conversion Methods

                /// <summary>
                ///     Convert to the unit representation <paramref name="unit" />.
                /// </summary>
                /// <returns>Value converted to the specified unit.</returns>
                public double As(ForcePerLengthUnit unit) => GetValueAs(unit);

                /// <summary>
                ///     Converts this ForcePerLength to another ForcePerLength with the unit representation <paramref name="unit" />.
                /// </summary>
                /// <returns>A ForcePerLength with the specified unit.</returns>
                public ForcePerLength ToUnit(ForcePerLengthUnit unit)
                {
                    var convertedValue = GetValueAs(unit);
                    return new ForcePerLength(convertedValue, unit);
                }

                /// <summary>
                ///     Converts the current value + unit to the base unit.
                ///     This is typically the first step in converting from one unit to another.
                /// </summary>
                /// <returns>The value in the base unit representation.</returns>
                private double GetValueInBaseUnit()
                {
                    return Unit switch
                    {
                        ForcePerLengthUnit.CentinewtonPerCentimeter => (_value * 1e2) * 1e-2d,
                        ForcePerLengthUnit.CentinewtonPerMeter => (_value) * 1e-2d,
                        ForcePerLengthUnit.CentinewtonPerMillimeter => (_value * 1e3) * 1e-2d,
                        ForcePerLengthUnit.DecanewtonPerCentimeter => (_value * 1e2) * 1e1d,
                        ForcePerLengthUnit.DecanewtonPerMeter => (_value) * 1e1d,
                        ForcePerLengthUnit.DecanewtonPerMillimeter => (_value * 1e3) * 1e1d,
                        ForcePerLengthUnit.DecinewtonPerCentimeter => (_value * 1e2) * 1e-1d,
                        ForcePerLengthUnit.DecinewtonPerMeter => (_value) * 1e-1d,
                        ForcePerLengthUnit.DecinewtonPerMillimeter => (_value * 1e3) * 1e-1d,
                        ForcePerLengthUnit.KilogramForcePerCentimeter => _value * 980.665002864,
                        ForcePerLengthUnit.KilogramForcePerMeter => _value * 9.80665002864,
                        ForcePerLengthUnit.KilogramForcePerMillimeter => _value * 9.80665002864e3,
                        ForcePerLengthUnit.KilonewtonPerCentimeter => (_value * 1e2) * 1e3d,
                        ForcePerLengthUnit.KilonewtonPerMeter => (_value) * 1e3d,
                        ForcePerLengthUnit.KilonewtonPerMillimeter => (_value * 1e3) * 1e3d,
                        ForcePerLengthUnit.KilopoundForcePerFoot => _value * 14593.90292,
                        ForcePerLengthUnit.KilopoundForcePerInch => _value * 1.75126835e5,
                        ForcePerLengthUnit.MeganewtonPerCentimeter => (_value * 1e2) * 1e6d,
                        ForcePerLengthUnit.MeganewtonPerMeter => (_value) * 1e6d,
                        ForcePerLengthUnit.MeganewtonPerMillimeter => (_value * 1e3) * 1e6d,
                        ForcePerLengthUnit.MicronewtonPerCentimeter => (_value * 1e2) * 1e-6d,
                        ForcePerLengthUnit.MicronewtonPerMeter => (_value) * 1e-6d,
                        ForcePerLengthUnit.MicronewtonPerMillimeter => (_value * 1e3) * 1e-6d,
                        ForcePerLengthUnit.MillinewtonPerCentimeter => (_value * 1e2) * 1e-3d,
                        ForcePerLengthUnit.MillinewtonPerMeter => (_value) * 1e-3d,
                        ForcePerLengthUnit.MillinewtonPerMillimeter => (_value * 1e3) * 1e-3d,
                        ForcePerLengthUnit.NanonewtonPerCentimeter => (_value * 1e2) * 1e-9d,
                        ForcePerLengthUnit.NanonewtonPerMeter => (_value) * 1e-9d,
                        ForcePerLengthUnit.NanonewtonPerMillimeter => (_value * 1e3) * 1e-9d,
                        ForcePerLengthUnit.NewtonPerCentimeter => _value * 1e2,
                        ForcePerLengthUnit.NewtonPerMeter => _value,
                        ForcePerLengthUnit.NewtonPerMillimeter => _value * 1e3,
                        ForcePerLengthUnit.PoundForcePerFoot => _value * 14.59390292,
                        ForcePerLengthUnit.PoundForcePerInch => _value * 1.75126835e2,
                        ForcePerLengthUnit.PoundForcePerYard => _value * 4.864634307,
                        ForcePerLengthUnit.TonneForcePerCentimeter => _value * 9.80665002864e5,
                        ForcePerLengthUnit.TonneForcePerMeter => _value * 9.80665002864e3,
                        ForcePerLengthUnit.TonneForcePerMillimeter => _value * 9.80665002864e6,
                        _ => throw new NotImplementedException($"Can not convert {Unit} to base units.")
                    };
                    }

                private double GetValueAs(ForcePerLengthUnit unit)
                {
                    if (Unit == unit)
                        return _value;

                    var baseUnitValue = GetValueInBaseUnit();

                    return unit switch
                    {
                        ForcePerLengthUnit.CentinewtonPerCentimeter => (baseUnitValue / 1e2) / 1e-2d,
                        ForcePerLengthUnit.CentinewtonPerMeter => (baseUnitValue) / 1e-2d,
                        ForcePerLengthUnit.CentinewtonPerMillimeter => (baseUnitValue / 1e3) / 1e-2d,
                        ForcePerLengthUnit.DecanewtonPerCentimeter => (baseUnitValue / 1e2) / 1e1d,
                        ForcePerLengthUnit.DecanewtonPerMeter => (baseUnitValue) / 1e1d,
                        ForcePerLengthUnit.DecanewtonPerMillimeter => (baseUnitValue / 1e3) / 1e1d,
                        ForcePerLengthUnit.DecinewtonPerCentimeter => (baseUnitValue / 1e2) / 1e-1d,
                        ForcePerLengthUnit.DecinewtonPerMeter => (baseUnitValue) / 1e-1d,
                        ForcePerLengthUnit.DecinewtonPerMillimeter => (baseUnitValue / 1e3) / 1e-1d,
                        ForcePerLengthUnit.KilogramForcePerCentimeter => baseUnitValue / 980.665002864,
                        ForcePerLengthUnit.KilogramForcePerMeter => baseUnitValue / 9.80665002864,
                        ForcePerLengthUnit.KilogramForcePerMillimeter => baseUnitValue / 9.80665002864e3,
                        ForcePerLengthUnit.KilonewtonPerCentimeter => (baseUnitValue / 1e2) / 1e3d,
                        ForcePerLengthUnit.KilonewtonPerMeter => (baseUnitValue) / 1e3d,
                        ForcePerLengthUnit.KilonewtonPerMillimeter => (baseUnitValue / 1e3) / 1e3d,
                        ForcePerLengthUnit.KilopoundForcePerFoot => baseUnitValue / 14593.90292,
                        ForcePerLengthUnit.KilopoundForcePerInch => baseUnitValue / 1.75126835e5,
                        ForcePerLengthUnit.MeganewtonPerCentimeter => (baseUnitValue / 1e2) / 1e6d,
                        ForcePerLengthUnit.MeganewtonPerMeter => (baseUnitValue) / 1e6d,
                        ForcePerLengthUnit.MeganewtonPerMillimeter => (baseUnitValue / 1e3) / 1e6d,
                        ForcePerLengthUnit.MicronewtonPerCentimeter => (baseUnitValue / 1e2) / 1e-6d,
                        ForcePerLengthUnit.MicronewtonPerMeter => (baseUnitValue) / 1e-6d,
                        ForcePerLengthUnit.MicronewtonPerMillimeter => (baseUnitValue / 1e3) / 1e-6d,
                        ForcePerLengthUnit.MillinewtonPerCentimeter => (baseUnitValue / 1e2) / 1e-3d,
                        ForcePerLengthUnit.MillinewtonPerMeter => (baseUnitValue) / 1e-3d,
                        ForcePerLengthUnit.MillinewtonPerMillimeter => (baseUnitValue / 1e3) / 1e-3d,
                        ForcePerLengthUnit.NanonewtonPerCentimeter => (baseUnitValue / 1e2) / 1e-9d,
                        ForcePerLengthUnit.NanonewtonPerMeter => (baseUnitValue) / 1e-9d,
                        ForcePerLengthUnit.NanonewtonPerMillimeter => (baseUnitValue / 1e3) / 1e-9d,
                        ForcePerLengthUnit.NewtonPerCentimeter => baseUnitValue / 1e2,
                        ForcePerLengthUnit.NewtonPerMeter => baseUnitValue,
                        ForcePerLengthUnit.NewtonPerMillimeter => baseUnitValue / 1e3,
                        ForcePerLengthUnit.PoundForcePerFoot => baseUnitValue / 14.59390292,
                        ForcePerLengthUnit.PoundForcePerInch => baseUnitValue / 1.75126835e2,
                        ForcePerLengthUnit.PoundForcePerYard => baseUnitValue / 4.864634307,
                        ForcePerLengthUnit.TonneForcePerCentimeter => baseUnitValue / 9.80665002864e5,
                        ForcePerLengthUnit.TonneForcePerMeter => baseUnitValue / 9.80665002864e3,
                        ForcePerLengthUnit.TonneForcePerMillimeter => baseUnitValue / 9.80665002864e6,
                        _ => throw new NotImplementedException($"Can not convert {Unit} to {unit}.")
                    };
                    }

                #endregion
    }
}

