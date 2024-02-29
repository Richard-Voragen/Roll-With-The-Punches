using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{

    /*
     * For my stage 4 command, I chose to do a jump command. 
     * My jump command gets the objects that the captain is 
     * currently touching and checks if any of them are the 
     * ground. If the captain is on the ground, then the 
     * captain is allowed to jump via setting his ridgidbody 
     * velocity x value to a positive value. If the captain 
     * is not touching the ground, then the captain is not 
     * allowed to jump.  
    */
    public class JumpCommand : ScriptableObject, ICaptainCommand
    {
        private float speed = 10.0f;
        //private GameObject motivator;
        private BoxCollider2D collisionBox;

        public void Execute(GameObject gameObject)
        {
            collisionBox = gameObject.GetComponent<BoxCollider2D>();
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                var contacts = new Collider2D[32];
                this.collisionBox.GetContacts(contacts);

                foreach (var contactedObject in contacts)
                {
                    if (contactedObject != null && contactedObject.gameObject != null && contactedObject.gameObject.name == "Ground")
                    {
                        rigidBody.velocity = new Vector2(0, this.speed);
                    }
                }
            }
        }
    }
}
