using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XCOM.Grid;

namespace XCOM
{
    public class Player : MonoBehaviour
    {
        // Public Events //
        public event EventHandler<Unit> OnUnitSelected;

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected GridSystem GridSystem
        {
            get
            {
                if (!_gridSystem) _gridSystem = FindObjectOfType<GridSystem>();
                return _gridSystem;
            }
        }

        protected CameraController CameraController
        {
            get
            {
                if (!_camera) _camera = FindObjectOfType<CameraController>();
                return _camera;
            }
        }

        // Private Properties //
        [SerializeField] private GameObject cursor;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private LayerMask unitLayer;
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private int nUnits;

        private List<Unit> units;
        private Unit selectedUnit;

        // Cached Components //

        // Cached References //
        private GridSystem _gridSystem;
        private CameraController _camera;

        // Public Methods //
        public void CreateUnits()
        {
            units = new List<Unit>();

            for (int i = 0; i < nUnits; i++)
            {
                Vector3 nextPos = new Vector3(i * 2f, 0f, 0f);

                CreateUnit(i, nextPos);
            }

            SelectUnit(units[0]);
        }

        // Private Methods //
        private void Start()
        {
            CreateUnits();
        }

        private void Update()
        {
            cursor.transform.position = GetHitLocation();

            HandleClickEvent();
            HandleButtonEvent();
        }

        private void CreateUnit(int id, Vector3 position)
        {
            Unit unit = Instantiate(unitPrefab, position, Quaternion.identity, transform);

            unit.name = $"Player Unit {id}";

            units.Add(unit);

            AddUnitToCurrentTile(unit);
        }

        private void AddUnitToCurrentTile(Unit unit)
        {
            GridObject gridObject = GridSystem.GetGridObject(unit.transform.position);

            gridObject.AddEntityToTile(unit);
        }

        private Vector3 GetHitLocation()
        {
            RaycastHit hit = GetHitOnLayer(floorLayer);

            return hit.point;
        }

        private RaycastHit GetHitOnLayer(LayerMask layer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layer);

            return hit;
        }

        private void GetMultipleHits()
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

        private void HandleClickEvent()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectUnit();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                MoveUnit();
            }
        }

        private void MoveUnit()
        {
            Vector3 hit = GetHitLocation();
            GridObject clickedGridObject = GridSystem.GetGridObject(hit);

            StartCoroutine(UpdateTile(clickedGridObject.transform.position));

            selectedUnit?.SetMoveDestination(clickedGridObject.transform.position);
        }

        private IEnumerator UpdateTile(Vector3 destination)
        {
            Vector3 start = selectedUnit.transform.position;

            while (!selectedUnit.HasReachedDestination(destination))
            {
                Vector3 current = selectedUnit.transform.position;

                GridObject before = GridSystem.GetGridObject(start);
                GridObject after = GridSystem.GetGridObject(current);

                if (before != after)
                {
                    before.RemoveEntityFromTile(selectedUnit);
                    AddUnitToCurrentTile(selectedUnit);

                    start = current;
                }

                yield return null;
            }
        }

        private void TrySelectUnit()
        {
            Transform hitTransform = GetHitOnLayer(unitLayer).transform;
            Unit clickedUnit = hitTransform?.GetComponent<Unit>();

            if (clickedUnit)
            {
                SelectUnit(clickedUnit);
            }
        }

        private void SelectUnit(Unit selectedUnit)
        {
            this.selectedUnit = selectedUnit;

            OnUnitSelected?.Invoke(null, this.selectedUnit);

            foreach (Unit unit in units)
            {
                unit.ToggleSelectedDisplay(unit == selectedUnit);
            }
        }

        private void HandleButtonEvent()
        {
            MoveCamera();
            RotateCamera();
            ZoomCamera();
        }

        private void MoveCamera()
        {
            Vector3 moveVector = Vector3.zero;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveVector += Vector3.left;
            }
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveVector += Vector3.right;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveVector += Vector3.forward;
            }
            
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveVector += Vector3.back;
            }

            CameraController.MoveCamera(moveVector);
        }

        private void RotateCamera()
        {
            Vector3 rotateVector = Vector3.zero;

            if (Input.GetKey(KeyCode.Q))
            {
                rotateVector += Vector3.up;
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                rotateVector += Vector3.down;
            }

            CameraController.RotateCamera(rotateVector);
        }

        private void ZoomCamera()
        {
            CameraController.Zoom(-Input.mouseScrollDelta);
        }
    }
}