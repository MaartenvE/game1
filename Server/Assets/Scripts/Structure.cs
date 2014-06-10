using UnityEngine;
using System;
using System.Collections;

public class Structure<T>
{
    public T[, ,] Elements;

    public Structure(T[, ,] elements)
    {
        this.Elements = elements;
    }

    public int GetLength(int dimension)
    {
        return Elements.GetLength(dimension);
    }

    public T this[int x, int y, int z]
    {
        get
        {
            if (x >= 0 && x < Elements.GetLength(0)
                && y >= 0 && y < Elements.GetLength(1)
                && z >= 0 && z < Elements.GetLength(2))
            {
                return Elements[x, y, z];
            }

            return default(T);
        }

        set
        {
            if (x >= 0 && x < Elements.GetLength(0)
                && y >= 0 && y < Elements.GetLength(1)
                && z >= 0 && z < Elements.GetLength(2))
            {
                Elements[x, y, z] = value;
            }
        }
    }
}
