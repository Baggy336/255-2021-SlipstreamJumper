using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Howley
{
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The thing the camera is following.
        /// </summary>
        public Transform target;

        

        void Start()
        {
            
        }

        void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x + 5;
            pos.y = target.position.y;
            //transform.position = pos;
            // Ease towards the object.

            transform.position += (pos - transform.position) * Time.deltaTime * 10; 
            
        }
    }
}

