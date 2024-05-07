using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class Model : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected Animator Animator
        {
            get
            {
                if (!_animator) _animator = GetComponent<Animator>();
                return _animator;
            }
        }

        // Private Properties //

        // Cached Components //
        private Animator _animator;

        // Cached References //

        // Public Methods //
        public void UpdateMoveAnimation(bool isMoving)
        {
            Animator.SetBool("isMoving", isMoving);
        }

        public void RotateModel(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }

        // Private Methods //
    }
}