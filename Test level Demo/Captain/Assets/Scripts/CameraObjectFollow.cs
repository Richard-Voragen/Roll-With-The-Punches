/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectFollow : MonoBehaviour
{
    public GameObject target;
    public float speed = 0.75f;

    private Vector3 targetPosition;

    void Start()
    {
        this.targetPosition = this.transform.position;
    }
    
    void FixedUpdate()
    {
        if (this.target)
        {
            var from = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            var to = new Vector3(this.target.transform.position.x, this.transform.position.y, this.transform.position.z);
            transform.position = Vector3.Lerp(from, to, this.speed);
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class CameraObjectFollow : MonoBehaviour
    {
        private Camera managedCamera;
        private LineRenderer cameraLineRenderer;
        public GameObject target;

        private void Awake()
        {
            managedCamera = gameObject.GetComponent<Camera>();
            cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            var targetPosition = this.target.transform.position;
            var cameraPosition = managedCamera.transform.position;

            cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);

            managedCamera.transform.position = cameraPosition;
        }
    }
}