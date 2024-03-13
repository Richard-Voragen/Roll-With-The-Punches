using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class PositionFollowCameraController : AbstractCameraController
    {
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;

        public float followSpeedFactor;
        public float leashDistance;
        public float catchUpSpeed;
        private Vector3 mLastPosition;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //Get the distance between the camera and target
        private float GetDistance()
        {
            var targetPosition = (Vector2)this.Target.transform.position;
            var cameraPosition = (Vector2)managedCamera.transform.position;

            return (targetPosition - cameraPosition).magnitude;
        }

        //Returns the percentage to lerp in order to move x units.
        private float MoveAmount(float units)
        {
            return (units*Time.deltaTime)/GetDistance();
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = managedCamera.transform.position;

            float speed = 
                ((Vector2)targetPosition - (Vector2)this.mLastPosition).magnitude / Time.deltaTime;
            mLastPosition = targetPosition;

            if (GetDistance() > leashDistance && speed > 0) 
            {
                managedCamera.transform.position = new Vector3(managedCamera.transform.position.x + this.Target.GetComponent<Rigidbody2D>().velocity.x * Time.deltaTime, cameraPosition.y, -10);
            } 
            else 
            {
                if (speed > 0) 
                {
                    cameraPosition = Vector2.Lerp(cameraPosition, 
                        targetPosition, MoveAmount(speed * followSpeedFactor));
                } 
                else 
                {
                    cameraPosition = Vector2.Lerp(cameraPosition, 
                        targetPosition, MoveAmount(catchUpSpeed));
                }
            }
            //Reset cameraPosition's z component
            cameraPosition = 
                new Vector3(cameraPosition.x, cameraPosition.y, managedCamera.transform.position.z);

            managedCamera.transform.position = cameraPosition;

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
            var x = this.Target.transform.position.x - this.managedCamera.transform.position.x;
            var y = this.Target.transform.position.y - this.managedCamera.transform.position.y;
            var z = this.Target.transform.position.z - this.managedCamera.transform.position.z;

            cameraLineRenderer.positionCount = 5;
            cameraLineRenderer.useWorldSpace = false;
            cameraLineRenderer.SetPosition(0, new Vector3(2.5f, 0f, z));
            cameraLineRenderer.SetPosition(1, new Vector3(-2.5f, 0f, z));
            cameraLineRenderer.SetPosition(2, new Vector3(0f, 0f, z));
            cameraLineRenderer.SetPosition(3, new Vector3(0f, 2.5f, z));
            cameraLineRenderer.SetPosition(4, new Vector3(0f, -2.5f, z));
        }
    }
}
