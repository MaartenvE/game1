using UnityEngine;

namespace BuildingBlocks
{
    public class RendererWrapper : IRenderer
    {
        private Renderer wrappedObject;

        public RendererWrapper(Renderer wrappedObject)
        {
            this.wrappedObject = wrappedObject;
        }

        public IMaterial material
        {
            get
            {
                return new MaterialWrapper(wrappedObject.material);
            }
        }

        public bool enabled
        {
            get
            {
                return wrappedObject.enabled;
            }

            set
            {
                wrappedObject.enabled = value;
            }
        }
    }
}
