using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 velocity;
    Vector3 Jump;
    public bool isGrounded;
    public int health = 100;
    public CanvasGroup end;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponent<Rigidbody>();
        Jump = 4 * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            end.alpha=1; 
        }
        
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ).normalized * 1500f/ (health + 40f);

        if (Input.GetButton("Jump") && isGrounded)
        {
            audioSource.Play();
            isGrounded = false;
            rigidbody.velocity+=Jump;
        }


        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                end.alpha = 0;
                Time.timeScale = 1;
            }
            else
            {
                // turn on the cursor
                Cursor.lockState = CursorLockMode.None;
                end.alpha = 1;
                Time.timeScale = 0;
            }


        }
        

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Roof" && isGrounded) {
            health--;
        }
        else if (collision.gameObject.tag == "Roof")
        {

        }
        else if(collision.impulse.y>0)
        {
            isGrounded = true;
        }
 
        
    }
    

    void FixedUpdate()
    {
        transform.localScale = new Vector3(1, 1, 1)* ((health + 10f) / 100f);
        
        rigidbody.velocity=transform.localToWorldMatrix.MultiplyVector(velocity) + rigidbody.velocity.y * Vector3.up;

    }
}
