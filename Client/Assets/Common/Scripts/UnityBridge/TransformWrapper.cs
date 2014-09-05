using UnityEngine;

namespace BuildingBlocks
{
    public class TransformWrapper : GameObjectWrapper, ITransform
    {
        public Transform Transform { get; private set; }

        public TransformWrapper(Transform wrappedObject)
            : base(wrappedObject.gameObject)
        {
            this.Transform = wrappedObject;
        }

        public ITransform parent
        {
            get
            {
                Transform parent = Transform.parent;
                return parent != null ? new TransformWrapper(parent) : null;
            }

            set
            {
                Transform.parent = value.Transform;
            }
        }

        public Quaternion localRotation
        {
            get
            {
                return Transform.localRotation;
            }

            set
            {
                Transform.localRotation = value;
            }
        }

        public Vector3 localPosition
        {
            get
            {
                return Transform.localPosition;
            }

            set
            {
                Transform.localPosition = value;
            }
        }

        public Vector3 localScale
        {
            get
            {
                return Transform.localScale;
            }

            set
            {
                Transform.localScale = value;
            }
        }

        public Vector3 InverseTransformPoint(Vector3 point)
        {
            return Transform.InverseTransformPoint(point);
        }
    }
}
