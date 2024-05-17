using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM.Grid
{
    public class GridObject : MonoBehaviour
    {
        // Public Events //
        public event EventHandler<MonoBehaviour> OnEnterTile;
        public event EventHandler<MonoBehaviour> OnExitTile;

        // Public Enums //

        // Public Properties //
        public List<MonoBehaviour> EntitiesOnTile { get { return entitiesOnTile; } }

        // Protected Properties //

        // Private Properties //
        [SerializeField] private List<MonoBehaviour> entitiesOnTile;

        private GridPosition position;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public string GetPosition()
        {
            return position.ToString();
        }

        public override string ToString()
        {
            return GetPosition();
        }

        public void SetPosition(GridPosition position)
        {
            this.position = position;

            string name = GetPosition();

            this.name = $"Grid Object ({name})";
        }

        public void SetPosition(int x, int z)
        {
            SetPosition(new GridPosition(x, z));
        }

        public void AddEntityToTile(MonoBehaviour entity)
        {
            if (entitiesOnTile.Contains(entity)) return;

            entitiesOnTile.Add(entity);

            OnEnterTile?.Invoke(this, entity);
        }

        public void RemoveEntityFromTile(MonoBehaviour entity)
        {
            if (!entitiesOnTile.Contains(entity)) return;

            entitiesOnTile.Remove(entity);

            OnExitTile?.Invoke(this, entity);
        }

        // Private Methods //
        private void Awake()
        {
            entitiesOnTile = new List<MonoBehaviour>();
        }
    }
}
