using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class Player : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Update()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRayFromMousePosition());

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Debug.Log(hits[i].transform.name);
                }
            }
        }

        private Ray GetRayFromMousePosition()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}