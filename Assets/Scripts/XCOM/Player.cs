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

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected UnitController UnitController
        {
            get
            {
                if (!_unitController) _unitController = GetComponent<UnitController>();
                return _unitController;
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

        // Cached Components //
        private UnitController _unitController;

        // Cached References //
        private CameraController _camera;

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            UnitController.CreateUnits();
        }

        private void Update()
        {
            cursor.transform.position = GetHitLocation();

            HandleClickEvent();
            HandleButtonEvent();
        }

        private Vector3 GetHitLocation()
        {
            RaycastHit hit = GetHitOnLayer(floorLayer);

            return hit.point;
        }

        private RaycastHit GetHitOnLayer(LayerMask layer)
        {
            Ray ray = GetRayFromMousePosition();
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
                TryMoveUnit();
            }
        }

        private void TrySelectUnit()
        {
            Transform hitTransform = GetHitOnLayer(unitLayer).transform;
            Unit clickedUnit = hitTransform?.GetComponent<Unit>();

            if (clickedUnit)
            {
                UnitController.SelectUnit(clickedUnit);
            }
        }

        private void TryMoveUnit()
        {
            Transform hitTransform = GetHitOnLayer(floorLayer).transform;
            GridObject clickedGridSpace = hitTransform?.GetComponent<GridObject>();

            if (clickedGridSpace)
            {
                UnitController.MoveSelectedUnit(clickedGridSpace);
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