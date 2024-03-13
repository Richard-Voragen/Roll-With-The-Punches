using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class PositionLockCameraController : AbstractCameraController
    {
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = managedCamera.transform.position;

            cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);

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
