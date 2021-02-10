using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Velting
{
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// When the player wants to move, this value
        /// is used to scale the players acceleration
        /// </summary>
        [Header("Horizontal Movement")]

        public float acceleration = 20;

        /// <summary>
        /// This value is used to scale the players
        /// deceleration
        /// </summary>
        public float deceleration = 40;

        /// <summary>
        /// This value is used to clamp the players
        /// horizontal velocity. Measured in m/s
        /// </summary>
        public float maxSpeed = 5;
        private Vector3 velocity = new Vector3();

        /// <summary>
        /// This is used to scale the player's downward acceleration
        /// due to gravity
        /// </summary>
        [Header ("Vertical Movement")]

               
        public float gravity = 10;
        bool isGrounded = false;
        private bool isJumpingUpwards = false;
        /// <summary>
        /// The velocity we launch the player when they jump.
        /// Measured in m/s
        /// </summary>
        public float jumpImpulse = 10;
        
        void Start()
        {

        }

       
        void Update()
        {
            //detect if on ground:
            CheckIfGrounded();
            CalcVerticalMovement();
            CalcMovementHorizontal();

            //applying our velocity to our position
            transform.position += velocity * Time.deltaTime;
        }

        private void CalcMovementHorizontal()
        {
            float h = Input.GetAxis("Horizontal");

            // Euler physics integration:

            if (h != 0) //user is pressing left or right (or both)
            {
                //applying acceleration to our velocity:
                velocity.x += h * Time.deltaTime * acceleration;
            }

            else //user is not pushing left or right
            {
                if (velocity.x > 0 && isGrounded) // player is moving right
                {
                    velocity.x += -deceleration * Time.deltaTime;
                    if (velocity.x < 0) velocity.x = 0;
                }

                if (velocity.x < 0 && isGrounded) //player is moving left 
                {
                    velocity.x += deceleration * Time.deltaTime;
                    if (velocity.x > 0) velocity.x = 0;
                }
            }

            //clamp:
            //if (velocity.x < -maxSpeed) velocity.x = -maxSpeed;

            //unity clamp:
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }

        private void CalcVerticalMovement()
        {
            float gravMultiplier = 1;



            //Jump Mechanic

            bool wantsToJump = Input.GetButtonDown("Jump");
            bool isHoldingJump = Input.GetButton("Jump");

            if (wantsToJump && isGrounded)
            {
                velocity.y = jumpImpulse;
                isJumpingUpwards = true;
            }

            if (!isHoldingJump || velocity.y < 0)
            {
                isJumpingUpwards = false;
            }

            if (isJumpingUpwards) gravMultiplier = .5f;

            //This is the force of gravity:
            velocity.y -= gravity * Time.deltaTime * gravMultiplier;

        }

        private void CheckIfGrounded()
        {
            if (transform.position.y <= 0) //on the ground
            {
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                velocity.y = 0;
                isGrounded = true;
            }
        }
    }
}
