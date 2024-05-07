using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using XCOM.UI;

namespace XCOM.Grid
{
    public class GridObject : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected UIGridObject UIGridObject
        {
            get
            {
                if (!_uiGridObject) _uiGridObject = GetComponentInChildren<UIGridObject>();
                return _uiGridObject;
            }
        }

        // Private Properties //
        private GridPosition position;

        // Cached Components //
        private UIGridObject _uiGridObject;

        // Cached References //

        // Public Methods //
        public void SetPosition(GridPosition position)
        {
            this.position = position;

            string name = this.position.ToString();

            this.name = $"Grid Object ({name})";

            UIGridObject.SetText(name);

        }

        public void SetPosition(int x, int z)
        {
            SetPosition(new GridPosition(x, z));
        }

        // Private Methods //
        private void Start()
        {
            UIGridObject.SetText(position.ToString());
        }
    }
}
