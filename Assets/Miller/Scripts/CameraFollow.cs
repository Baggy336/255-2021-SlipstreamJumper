using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miller
{


    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The thing that the camera is following.
        /// </summary>
        public Transform target;

        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 pos = transform.position;

            pos.x = target.position.x;
            pos.y = target.position.y;

            //transform.position = pos;

            // asymtotic easing:
            // exponential slide:
            transform.position += (pos - transform.position) * Time.deltaTime * 10;
        }
    }
}
