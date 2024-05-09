using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Cinemachine;

namespace XCOM
{
    public class CameraController : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected CinemachineVirtualCamera CVC
        {
            get
            {
                if (!_cam) _cam = GetComponentInChildren<CinemachineVirtualCamera>();
                return _cam;
            }
        }

        protected CinemachineTransposer Transposer
        {
            get
            {
                if (!_transposer) _transposer = CVC.GetCinemachineComponent<CinemachineTransposer>();
                return _transposer;
            }
        }

        // Private Properties //
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;

        // Cached Components //
        private CinemachineVirtualCamera _cam;
        private CinemachineTransposer _transposer;

        // Cached References //

        // Public Methods //
        public void MoveCamera(Vector3 moveVector)
        {
            Vector3 x = transform.right * moveVector.x;
            Vector3 y = transform.up * moveVector.y;
            Vector3 z = transform.forward * moveVector.z;
            Vector3 v = x + y + z;

            transform.position += v * moveSpeed * Time.deltaTime;
        }

        public void RotateCamera(Vector3 rotationVector)
        {
            transform.Rotate(rotationVector * rotateSpeed * Time.deltaTime);
        }

        public void Zoom(Vector3 factor)
        {
            Vector3 followOffset = Transposer.m_FollowOffset;
            Vector3 offsetMod = factor * scrollSpeed * Time.deltaTime;
            
            Transposer.m_FollowOffset = new Vector3(
                followOffset.x + offsetMod.x,
                Mathf.Clamp(followOffset.y + offsetMod.y, minZoom, maxZoom),
                followOffset.z + offsetMod.z
            );
        }

        // Private Methods //
    }
}