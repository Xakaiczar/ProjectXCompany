using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XCOM.UI;

namespace XCOM.Grid
{
    public class UIManager : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private UISelectedUnit selectedVisualPrefab;

        private UISelectedUnit selectedVisual;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void ToggleSelectedVisual(bool isEnabled) => selectedVisual.ToggleDisplay(isEnabled);
        public void ShowSelectedVisual() => selectedVisual.ToggleDisplay(true);
        public void HideSelectedVisual() => selectedVisual.ToggleDisplay(false);

        public void MoveSelectedVisual(Vector3 newPosition)
        {
            selectedVisual.transform.position = selectedVisualPrefab.transform.position + newPosition;
        }

        public void FollowSelectedVisual(Transform target)
        {
            MoveSelectedVisual(target.position);
            
            selectedVisual.transform.parent = target;
        }

        // Private Methods //
        private void Awake()
        {
            selectedVisual = Instantiate(selectedVisualPrefab, transform);
        }
    }
}
