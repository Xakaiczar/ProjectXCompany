using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class MouseWorld : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private LayerMask floorLayer;

        private static MouseWorld instance;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.floorLayer);

            return raycastHit.point;
        }

        // Private Methods //
        private void Awake()
        {
            instance = this;
        }

        // private void Update()
        // {
        //     transform.position = MouseWorld.GetPosition();
        // }
    }
}