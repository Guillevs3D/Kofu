using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        //Variables Globales publicas
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        //Variables Globales Privadas
        private float offestZ;
        private Vector3 lasTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadsPos; 

        private void Start()
        {
            lasTargetPosition = target.position;
            offestZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        private void Update()
        {
            //Solo actualiza la posición si acelera o cambia de dirección el personaje
            float xMoveDelta = (target.position - lasTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadsPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadsPos = Vector3.MoveTowards(lookAheadsPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadsPos + Vector3.forward*offestZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

            transform.position = newPos;

            lasTargetPosition = target.position;
        }
    }
}
