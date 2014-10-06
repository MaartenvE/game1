using UnityEngine;

namespace BuildingBlocks.Blocks
{
    public class Block
    {
        public Color Color { get; private set; }

        public Block()
        {
            this.Color = ColorModel.RandomPrimaryColor();
        }

        public bool Mix(Block otherBlock)
        {
            Color result;
            if (ColorMixer.Mix(this.Color, otherBlock.Color, out result))
            {
                this.Color = result;
                return true;
            }
            return false;
        }
    }
}
