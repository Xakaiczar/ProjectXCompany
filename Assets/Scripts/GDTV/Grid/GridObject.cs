using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class GridObject
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private GridSystem gridSystem;
        private GridPosition gridPosition;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
        }

        public override string ToString()
        {
            return gridPosition.ToString();
        }

        // Private Methods //
    }
}
