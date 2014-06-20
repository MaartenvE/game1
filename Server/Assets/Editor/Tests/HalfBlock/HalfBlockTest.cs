using UnityEngine;
using System.Collections;
using NUnit.Framework;
using BuildingBlocks.Blocks;

namespace BuildingBlocks.HalfBlock
{
    [TestFixture]
    public class HalfBlockTest
    {
        static object[] Colors = 
    {
        new object[] {ColorModel.RED, null, ColorModel.RED},
        new object[] {ColorModel.RED, ColorModel.RED,ColorModel.RED},
        new object[] {ColorModel.YELLOW, ColorModel.YELLOW,ColorModel.YELLOW},
        new object[] {ColorModel.BLUE, ColorModel.BLUE,ColorModel.BLUE},

        new object[] {ColorModel.RED, ColorModel.YELLOW,ColorModel.ORANGE},
        new object[] {ColorModel.YELLOW, ColorModel.RED,ColorModel.ORANGE},
        new object[] {ColorModel.RED, ColorModel.BLUE,ColorModel.PURPLE},
        new object[] {ColorModel.BLUE, ColorModel.RED,ColorModel.PURPLE},
        new object[] {ColorModel.YELLOW, ColorModel.BLUE,ColorModel.GREEN},
        new object[] {ColorModel.BLUE, ColorModel.YELLOW,ColorModel.GREEN}
    };


        [Test, TestCaseSource("Colors")]
        public void CalculateUnityColorTest(Color firstColor, Color secondColor, Color expectedColor)
        {

            HalfBlock halfBlock = new HalfBlock();

            HalfBlockColorDecorator first = new HalfBlockColorDecorator(new HalfBlockColor(firstColor));
            HalfBlockColorDecorator second = null;
            if (!secondColor.Equals(new Color()))
            {
                second = new HalfBlockColorDecorator(new HalfBlockColor(secondColor));
            }
            halfBlock.wrappedObject = first;
            first.wrappedObject = second;

            Color result = halfBlock.CalculateUnityColor();
            Assert.AreEqual(expectedColor, result);
        }

        [Test, TestCaseSource("Colors")]
        public void CombineHalfBlockTest(Color firstColor, Color secondColor, Color expectedColor)
        {

            HalfBlock halfBlock = new HalfBlock(new HalfBlockColor(firstColor));
            HalfBlock second = new HalfBlock(new HalfBlockColor(secondColor));

            halfBlock.CombineHalfBlock(second);

            Color result = halfBlock.CalculateUnityColor();
            Assert.AreEqual(expectedColor, result);
        }
    }
}
