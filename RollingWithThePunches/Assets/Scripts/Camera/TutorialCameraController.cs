using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControls
{
    public class TutorialCameraController : MonoBehaviour
    {
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;
        public float speed = 4f;
        public bool scrolling = false;

        private float[] stopPoints = {-290f, -270.58f, -251.208f, -223.7f};
        private int stopIndex = 0;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            if (managedCamera.transform.position.x < stopPoints[stopIndex])
            {
                managedCamera.transform.position = new Vector3(managedCamera.transform.position.x + (speed*Time.deltaTime), managedCamera.transform.position.y, managedCamera.transform.position.z);
                scrolling = true;
            }
            else
            {
                scrolling = false;
            }
        }

        public void NextImage() {
            this.stopIndex++;
        }
    }
}
