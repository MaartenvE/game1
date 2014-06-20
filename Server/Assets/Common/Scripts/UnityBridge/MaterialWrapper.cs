using UnityEngine;

namespace BuildingBlocks
{
    public class MaterialWrapper : IMaterial
    {
        private Material wrappedObject;

        public MaterialWrapper(Material wrappedObject)
        {
            this.wrappedObject = wrappedObject;
        }

        public Color color
        {
            get
            {
                return wrappedObject.color;
            }

            set
            {
                wrappedObject.color = value;
            }
        }
    }
}
