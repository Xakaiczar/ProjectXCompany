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
        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0f, z) * cellSize;
        }

        public Vector3 GetWorldPosition(GridPosition position)
        {
            return new Vector3(position.x, 0f, position.z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int z = Mathf.RoundToInt(worldPosition.z / cellSize);

            return new GridPosition(x, z);
        }

        // Private Methods //
        private void Awake()
        {
            grid = new GridPosition[gridWidth, gridLength];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridLength; z++)
                {
                    grid[x, z] = new GridPosition(x, z);

                    var sphere = Instantiate(spherePrefab, GetWorldPosition(x, z), Quaternion.identity);

                    Debug.Log(GetGridPosition(sphere.transform.position));
                }
            }
        }
    }
}
