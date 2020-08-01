using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//References:
//https://github.com/jiankaiwang/FirstPersonController
public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    private float translation;
    private float straffe;

    bool canHide = false;
    bool isHiding = false;

    [SerializeField]
    Rigidbody rb;
    bool isFalling = false;
    [SerializeField]
    Vector3 force;
    void Start()
    {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            isFalling = true;
            rb.AddForce(force, ForceMode.Impulse);
        }

        //Let the player hide when in a safe spot, unhide when left shift is toggled
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canHide && !isHiding)
            {
                isHiding = true;
            }
            else if(isHiding)
            {
                isHiding = false;
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hide")
        {
            canHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hide")
        {
            canHide = false;
            isHiding = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isFalling = false;
        }
    }
}
