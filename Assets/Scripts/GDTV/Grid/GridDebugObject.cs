using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

namespace GDTV
{
    public class GridDebugObject : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private TextMeshPro textMeshPro;

        private GridObject gridObject;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void SetGridObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
        }

        // Private Methods //
        private void Update()
        {
            textMeshPro.text = gridObject.ToString();
        }
    }
}