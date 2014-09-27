using UnityEngine;
using System.Collections;

namespace BuildingBlocks.Blocks
{
    public class Structure<T> : IEnumerable
    {
        private T[, ,] elements;

        public int Size { get; private set; }

        public Structure(T[, ,] elements)
        {
            this.elements = elements;
            this.Size = elements.GetLength(0) * elements.GetLength(1) * elements.GetLength(2);
        }

        public Structure(int xLength, int yLength, int zLength)
        {
            elements = new T[xLength, yLength, zLength];
        }

        public Structure<T> Rotate()
        {
            Structure<T> newStructure = new Structure<T>(elements.GetLength(1), elements.GetLength(0), elements.GetLength(2));
            for (int z = 0; z < elements.GetLength(1); z++)
            {
                for (int y = 0; y < elements.GetLength(0); y++)
                {
                    for (int x = 0; x < elements.GetLength(2); x++)
                    {
                        newStructure[elements.GetLength(2) - x - 1, z, y] = elements[y, z, x];
                    }
                }
            }

            return newStructure;
        }

        public int GetLength(int dimension)
        {
            return elements.GetLength(dimension);
        }

        public bool IsWithinBounds(int x, int y, int z)
        {
            return x >= 0 && x < elements.GetLength(0)
                && y >= 0 && y < elements.GetLength(1)
                && z >= 0 && z < elements.GetLength(2);
        }

        public bool IsWithinBounds(Vector3 location)
        {
            return IsWithinBounds((int)location.x, (int)location.y, (int)location.z);
        }

        public Vector3 Normalize(Vector3 position, float scale)
        {
            Vector3 normalized = position / scale;
            normalized.x += GetLength(0) / 2;
            normalized.z += GetLength(2) / 2;
            return normalized;
        }

        public Vector3 Denormalize(Vector3 position, float scale)
        {
            position.x -= GetLength(0) / 2;
            position.z -= GetLength(0) / 2;
            return position * scale;
        }

        public T this[int x, int y, int z]
        {
            get
            {
                if (IsWithinBounds(x, y, z))
                {
                    return elements[x, y, z];
                }

                return default(T);
            }

            set
            {
                if (IsWithinBounds(x, y, z))
                {
                    elements[x, y, z] = value;
                }
            }
        }

        public T this[Vector3 location]
        {
            get
            {
                return this[(int)location.x, (int)location.y, (int)location.z];
            }

            set
            {
                this[(int)location.x, (int)location.y, (int)location.z] = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        // To get all keys in a three-dimensional structure, a triple loop is unfortunately needed.
        public IEnumerable Keys
        {
            get
            {
                for (int x = 0; x < GetLength(0); x++)
                {
                    for (int y = 0; y < GetLength(1); y++)
                    {
                        for (int z = 0; z < GetLength(2); z++)
                        {
                            if (this[x, y, z] != null)
                            {
                                yield return new Vector3(x, y, z);
                            }
                        }
                    }
                }
            }
        }
    }
}
