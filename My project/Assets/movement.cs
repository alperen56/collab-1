using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] float camshakeamount;
    [Range(0.001f, 00.1f)]
    public float amount = 0.002f;
    [Range(0f, 100f)]
    [SerializeField] float smooth;
    [Range(0f, 100f)]
    [SerializeField] private float speed = 2f;
    [Range(0f, 100f)]
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Vector3 orginalposcamera;
    [SerializeField] Transform playerBody;
    [SerializeField] GameObject cameraobject;
    float xr = 0f;//rotation of camera(x)
    [SerializeField] CharacterController charController;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {

    }

    void Start()

    {

        Cursor.lockState = CursorLockMode.Locked;


    }

    private void Update()
    {






        cam();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {

            audioSource.enabled = true;
            shake();

        }
        else
        {
            stopshake();
            audioSource.enabled = false;

        }

        movement();


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            speed = 3.7f;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            speed = 2f;

        }
    }


    //movement------------------------------------------

    private void movement()


    {




        float hori = Input.GetAxis("Horizontal") * speed;

        float veri = Input.GetAxis("Vertical") * speed;

        Vector3 fm = transform.forward * veri;

        Vector3 rm = transform.right * hori;


        charController.SimpleMove(fm + rm);




    }

    //cam--------------------------------------------------

    private void cam()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xr -= mouseY;
        xr = Mathf.Clamp(xr, -90f, 90f);

        cameraobject.transform.localRotation = Quaternion.Euler(xr, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

    }


    //cam buble
    private void shake()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * camshakeamount) * amount * 1.4f, smooth * Time.deltaTime);
        cameraobject.transform.localPosition += pos;

        
    }



    private void stopshake()
    {
        if (cameraobject.transform.localPosition == orginalposcamera) return;
        cameraobject.transform.localPosition = Vector3.Lerp(cameraobject.transform.localPosition, orginalposcamera, 3 * Time.deltaTime);
    }
}

