using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Visyde.V_SMC_Handler crosshairHandler;
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // the chacter is the capsule
    public GameObject character;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;
    // For the raycasting function:
    Vector3 fireDirection;
    Vector3 firePoint;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState != CursorLockMode.None)
        {
            // md is mosue delta
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            // the interpolated float result between the two float values
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            // incrementally add to the camera look
            mouseLook += smoothV;

       

            Hit();
        }

    
    }
    void Hit()
    {
        // Raycasting variables:
        RaycastHit hit;
        fireDirection = transform.TransformDirection(Vector3.forward) * 10;
        firePoint = transform.position;

        // Do raycasting:
        if (Physics.Raycast(firePoint, (fireDirection), out hit, Mathf.Infinity))
        {
            // Change the color based on what object is under the crosshair:
            if (hit.transform.tag == "Friendly")
            {
                crosshairHandler.ChangeColor(Color.green);
            }
            else if (hit.transform.tag == "Enemy")
            {
                crosshairHandler.ChangeColor(Color.red);
            }
            else
            {
                crosshairHandler.ChangeColor(Color.white);
            }
        }
        else
        {
            crosshairHandler.ChangeColor(Color.white);
        }

        // Debug the ray out in the editor:
        Debug.DrawRay(firePoint, fireDirection, Color.green);
    }
    void FixedUpdate()
    {
        // vector3.right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
