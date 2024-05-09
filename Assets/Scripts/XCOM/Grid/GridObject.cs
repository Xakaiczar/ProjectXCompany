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
        [SerializeField] private List<MonoBehaviour> entitiesOnTile;

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

        public void AddEntityToTile(MonoBehaviour entity)
        {
            if (entitiesOnTile.Contains(entity)) return;

            entitiesOnTile.Add(entity);

            UpdateText();
        }

        public void RemoveEntityFromTile(MonoBehaviour entity)
        {
            if (!entitiesOnTile.Contains(entity)) return;

            entitiesOnTile.Remove(entity);

            UpdateText();
        }

        // Private Methods //
        private void Awake()
        {
            entitiesOnTile = new List<MonoBehaviour>();
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            string text = position.ToString();

            if (entitiesOnTile.Count > 0)
            {
                foreach (var item in entitiesOnTile)
                {
                    text += $"\n {item.name}";
                }
            }

            UIGridObject.SetText(text);
        }
    }
}
