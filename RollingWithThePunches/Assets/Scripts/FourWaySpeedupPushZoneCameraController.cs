using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class FourWaySpeedupPushZoneCameraController : AbstractCameraController
    {
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;

        public float pushRatio;
        public Vector2 topLeft;
        public Vector2 bottomRight;
        public Vector2 innerBoxTopLeft;
        public Vector2 innerBoxBottomRight;

        private Vector3 mLastPosition;
        private bool firstUpdate = true;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //Move the camera to the player's location when game starts
        private void GotoPlayerAtStart()
        {
            if (firstUpdate)
            {
                var targetPosition = (Vector2)this.Target.transform.position;
                var cameraPosition = managedCamera.transform.position;

                cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);
                managedCamera.transform.position = cameraPosition;

                firstUpdate = false;
            }
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = managedCamera.transform.position;

            float xSpeed = targetPosition.x - this.mLastPosition.x;
            float ySpeed = targetPosition.y - this.mLastPosition.y;
            float speed = 
                ((Vector2)targetPosition - (Vector2)this.mLastPosition).magnitude / Time.deltaTime;
            mLastPosition = targetPosition;

            if (targetPosition.x >= cameraPosition.x + bottomRight.x || 
                targetPosition.x <= cameraPosition.x + topLeft.x) 
            {
                cameraPosition = new Vector2(cameraPosition.x + xSpeed, cameraPosition.y);
            } 
            else if ((targetPosition.x >= cameraPosition.x + innerBoxBottomRight.x && xSpeed > 0) ||
                     (targetPosition.x <= cameraPosition.x + innerBoxTopLeft.x && xSpeed < 0))
            {
                cameraPosition = 
                    new Vector2(cameraPosition.x + (xSpeed * pushRatio), cameraPosition.y);
            }

            if (targetPosition.y >= cameraPosition.y + topLeft.y || 
                targetPosition.y <= cameraPosition.y + bottomRight.y) 
            {
                cameraPosition = new Vector2(cameraPosition.x, cameraPosition.y + ySpeed);
            } 
            else if ((targetPosition.y >= cameraPosition.y + innerBoxTopLeft.y && ySpeed > 0) ||
                     (targetPosition.y <= cameraPosition.y + innerBoxBottomRight.y && ySpeed < 0))
            {
                cameraPosition = 
                    new Vector2(cameraPosition.x, cameraPosition.y + (ySpeed * pushRatio));
            }
            
            //Reset cameraPosition's z component
            cameraPosition = 
                new Vector3(cameraPosition.x, cameraPosition.y, managedCamera.transform.position.z);

            managedCamera.transform.position = cameraPosition;

            //Move to player on game load
            GotoPlayerAtStart();

            if (this.DrawLogic)
            {
                cameraLineRenderer.enabled = true;
                DrawCameraLogic();
            }
            else
            {
                cameraLineRenderer.enabled = false;
            }
        }

        public override void DrawCameraLogic()
        {
            var z = this.Target.transform.position.z - this.managedCamera.transform.position.z;

            cameraLineRenderer.positionCount = 10;
            cameraLineRenderer.useWorldSpace = false;
            cameraLineRenderer.SetPosition(0, new Vector3(topLeft.x, topLeft.y, z));
            cameraLineRenderer.SetPosition(1, new Vector3(bottomRight.x, topLeft.y, z));
            cameraLineRenderer.SetPosition(2, new Vector3(bottomRight.x, bottomRight.y, z));
            cameraLineRenderer.SetPosition(3, new Vector3(topLeft.x, bottomRight.y, z));
            cameraLineRenderer.SetPosition(4, new Vector3(topLeft.x, topLeft.y, z));

            cameraLineRenderer.SetPosition(5, new Vector3(innerBoxTopLeft.x, innerBoxTopLeft.y, z));
            cameraLineRenderer.SetPosition(6, new Vector3(innerBoxBottomRight.x, innerBoxTopLeft.y, z));
            cameraLineRenderer.SetPosition(7, new Vector3(innerBoxBottomRight.x, innerBoxBottomRight.y, z));
            cameraLineRenderer.SetPosition(8, new Vector3(innerBoxTopLeft.x, innerBoxBottomRight.y, z));
            cameraLineRenderer.SetPosition(9, new Vector3(innerBoxTopLeft.x, innerBoxTopLeft.y, z));
        }
    }
}
