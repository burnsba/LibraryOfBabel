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

using UnitsNet.NumberExtensions.NumberToVolumeConcentration;
using Xunit;

namespace UnitsNet.Tests
{
    public class NumberToVolumeConcentrationExtensionsTests
    {
        [Fact]
        public void NumberToCentilitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromCentilitersPerLiter(2), 2.CentilitersPerLiter());

        [Fact]
        public void NumberToCentilitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromCentilitersPerMililiter(2), 2.CentilitersPerMililiter());

        [Fact]
        public void NumberToDecilitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromDecilitersPerLiter(2), 2.DecilitersPerLiter());

        [Fact]
        public void NumberToDecilitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromDecilitersPerMililiter(2), 2.DecilitersPerMililiter());

        [Fact]
        public void NumberToDecimalFractionsTest() =>
            Assert.Equal(VolumeConcentration.FromDecimalFractions(2), 2.DecimalFractions());

        [Fact]
        public void NumberToLitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromLitersPerLiter(2), 2.LitersPerLiter());

        [Fact]
        public void NumberToLitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromLitersPerMililiter(2), 2.LitersPerMililiter());

        [Fact]
        public void NumberToMicrolitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromMicrolitersPerLiter(2), 2.MicrolitersPerLiter());

        [Fact]
        public void NumberToMicrolitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromMicrolitersPerMililiter(2), 2.MicrolitersPerMililiter());

        [Fact]
        public void NumberToMillilitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromMillilitersPerLiter(2), 2.MillilitersPerLiter());

        [Fact]
        public void NumberToMillilitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromMillilitersPerMililiter(2), 2.MillilitersPerMililiter());

        [Fact]
        public void NumberToNanolitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromNanolitersPerLiter(2), 2.NanolitersPerLiter());

        [Fact]
        public void NumberToNanolitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromNanolitersPerMililiter(2), 2.NanolitersPerMililiter());

        [Fact]
        public void NumberToPartsPerBillionTest() =>
            Assert.Equal(VolumeConcentration.FromPartsPerBillion(2), 2.PartsPerBillion());

        [Fact]
        public void NumberToPartsPerMillionTest() =>
            Assert.Equal(VolumeConcentration.FromPartsPerMillion(2), 2.PartsPerMillion());

        [Fact]
        public void NumberToPartsPerThousandTest() =>
            Assert.Equal(VolumeConcentration.FromPartsPerThousand(2), 2.PartsPerThousand());

        [Fact]
        public void NumberToPartsPerTrillionTest() =>
            Assert.Equal(VolumeConcentration.FromPartsPerTrillion(2), 2.PartsPerTrillion());

        [Fact]
        public void NumberToPercentTest() =>
            Assert.Equal(VolumeConcentration.FromPercent(2), 2.Percent());

        [Fact]
        public void NumberToPicolitersPerLiterTest() =>
            Assert.Equal(VolumeConcentration.FromPicolitersPerLiter(2), 2.PicolitersPerLiter());

        [Fact]
        public void NumberToPicolitersPerMililiterTest() =>
            Assert.Equal(VolumeConcentration.FromPicolitersPerMililiter(2), 2.PicolitersPerMililiter());

    }
}
