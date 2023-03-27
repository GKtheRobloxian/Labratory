using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Horizontal;
    public float Vertical;
    public float DashForce;
    public Transform cameraControl;
    bool Grounded;
    Camera cam;
    float jumpAmount = 2;
    float jump;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = cameraControl.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        jump = jumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotate = Quaternion.Euler(new Vector3(0, cam.ytate, 0));
        transform.rotation = rotate;
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Vertical, Space.Self);
        transform.Translate(Vector3.right * speed * Time.deltaTime * Horizontal, Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump <= 0)
            {
                return;
            }
            else
            {
                rb.AddForce(Vector3.up * 250f);
                jump -= 1;
                Grounded = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddRelativeForce(Vector3.forward * DashForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = jumpAmount;
            Grounded = true;
        }
    }
}
