using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XCOM.UI;

namespace XCOM
{
    public class UIManager : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private UISelectedUnit selectedVisualPrefab;
        [SerializeField] private UIGridObject gridObjectPrefab;

        private UISelectedUnit selectedVisual;
        // private UIGridObject[,] grid;


        // Cached Components //

        // Cached References //

        // Public Methods //
        public void ToggleSelectedVisual(bool isEnabled) => selectedVisual.ToggleDisplay(isEnabled);
        public void ShowSelectedVisual() => selectedVisual.ToggleDisplay(true);
        public void HideSelectedVisual() => selectedVisual.ToggleDisplay(false);
        public void SetGridObjectVisualText(UIGridObject visual, string text) => visual.SetText(text);

        public void MoveSelectedVisual(Vector3 newPosition)
        {
            selectedVisual.transform.position = selectedVisualPrefab.transform.position + newPosition;
        }

        public void FollowSelectedVisual(Transform target)
        {
            MoveSelectedVisual(target.position);

            selectedVisual.transform.parent = target;
        }

        public void CreateGridObjectVisual(string text, Transform parent)
        {
            var visual = Instantiate(gridObjectPrefab, parent);

            visual.transform.position = gridObjectPrefab.transform.position + parent.position;

            visual.SetText(text);
        }

        // Private Methods //
        private void Awake()
        {
            selectedVisual = Instantiate(selectedVisualPrefab, transform);
        }
    }
}
