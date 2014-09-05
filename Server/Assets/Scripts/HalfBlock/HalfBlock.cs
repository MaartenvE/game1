using UnityEngine;
using System.Collections;

namespace BuildingBlocks.HalfBlock
{
    public class HalfBlock
    {

        private HalfBlockColorDecorator _wrappedObject;

        public HalfBlockColorDecorator wrappedObject
        {
            get { return _wrappedObject; }
            set { _wrappedObject = value; }
        }

        public HalfBlock(AbstractHalfBlockColor color)
        {
            if (!color.color.Equals(new Color()))
            {
                wrappedObject = new HalfBlockColorDecorator(color);
            }
        }

        public HalfBlock()
        {
            // EMPTY CONSTRUCTOR
        }

        public virtual Color CalculateUnityColor()
        {
            return wrappedObject.CalculateUnityColor();
        }

        public void CombineHalfBlock(HalfBlock other)
        {
            HalfBlockDecorator currentWrappedObject = this.wrappedObject;
            while (currentWrappedObject.wrappedObject != null)
            {
                currentWrappedObject = currentWrappedObject.wrappedObject;
            }
            currentWrappedObject.wrappedObject = other.wrappedObject;
            other.wrappedObject = currentWrappedObject.wrappedObject;
        }
    }
}
