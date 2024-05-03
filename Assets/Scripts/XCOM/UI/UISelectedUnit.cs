using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class UISelectedUnit : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected MeshRenderer MeshRenderer
        {
            get
            {
                if (!_meshRenderer) _meshRenderer = GetComponent<MeshRenderer>();
                return _meshRenderer;
            }
        }

        // Private Properties //

        // Cached Components //
        private MeshRenderer _meshRenderer;

        // Cached References //

        // Public Methods //
        public void ToggleDisplay(bool isEnabled)
        {
            MeshRenderer.enabled = isEnabled;
        }

        // Private Methods //
    }
}
