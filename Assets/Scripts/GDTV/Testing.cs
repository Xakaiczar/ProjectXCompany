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

        // Cached Components //
        [SerializeField] private Unit unit;

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            // 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                unit.GetMoveAction().GetValidActionGridPositionList();
            }
        }
    }
}
