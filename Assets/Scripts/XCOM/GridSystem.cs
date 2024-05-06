using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TextCore.Text;

namespace XCOM
{
    public class GridSystem : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private int cellSize;
        [SerializeField] private int gridWidth;
        [SerializeField] private int gridLength;
        [SerializeField] private GameObject spherePrefab;

        private GridPosition[,] grid;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Awake()
        {
            grid = new GridPosition[gridWidth, gridLength];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridLength; z++)
                {
                    grid[x, z] = new GridPosition(x, z);

                    Instantiate(spherePrefab, new Vector3(x * cellSize, 0f, z * cellSize), Quaternion.identity);
                }
            }
        }

        // separate gameplay area into cells / gridpositions
        // width + length
        // generate all gridpositions
        // all logic in grid system will be based on gridpositions
        // convert gridposition to world position
        // cell size * gridpos = world units
    }
}
