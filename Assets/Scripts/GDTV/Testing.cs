using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class Testing : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private GridSystem gridSystem;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            gridSystem = new GridSystem(10, 10, 2f);
        }

        private void Update()
        {
            Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
        }
    }
}
