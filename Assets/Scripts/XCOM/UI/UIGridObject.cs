using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

using XCOM.Grid;

namespace XCOM.UI
{
    public class UIGridObject : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected TextMeshPro Text
        {
            get
            {
                if (!_text) _text = GetComponent<TextMeshPro>();
                return _text;
            }
        }

        protected GridObject GridObject
        {
            get
            {
                if (!_gridObject) _gridObject = GetComponentInParent<GridObject>();
                return _gridObject;
            }
        }

        // Private Properties //

        // Cached Components //
        private TextMeshPro _text;

        // Cached References //
        private GridObject _gridObject;

        // Public Methods //
        public void SetText(string newText)
        {
            Text.text = newText;
        }

        // Private Methods //
        private void Awake()
        {
            GridObject.OnEnterTile += UpdateText;
            GridObject.OnExitTile += UpdateText;
        }

        private void UpdateText(object sender, MonoBehaviour entity)
        {
            string text = GridObject.ToString();
            var entities = GridObject.EntitiesOnTile;

            if (entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    text += $"\n {item.name}";
                }
            }

            SetText(text);
        }
    }
}
