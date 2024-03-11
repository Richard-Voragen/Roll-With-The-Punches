using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class MoveCharacterJump : ScriptableObject, IPlayerCommand
    {
        private float jumpForce = 8.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                // Apply an upward force to the Rigidbody2D to simulate jumping
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            }
        }
    }
}
