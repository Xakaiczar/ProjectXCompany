using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class GridSystem
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private int width;
        private int height;
        private float cellSize;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public GridSystem(int width, int height, float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + (Vector3.right * 0.5f), Color.white, float.MaxValue);
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0f, z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int z = Mathf.RoundToInt(worldPosition.z / cellSize);

            return new GridPosition(x, z);
        }

        // Private Methods //
    }
}