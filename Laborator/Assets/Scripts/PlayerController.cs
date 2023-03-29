using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Quaternion proj;
    public Rigidbody laser;
    public Transform firePoint;
    float fireRate = 0.49f;
    float fire;
    public float speed;
    public float Horizontal;
    public float Vertical;
    public float DashForce;
    public float SlideForce;
    public Transform cameraControl;
    bool Grounded;
    bool Slide;
    bool wallCling = false;
    Camera cam;
    float slideTimer;
    float jumpAmount = 2;
    float jump;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        fire = fireRate;
        Cursor.lockState = CursorLockMode.Locked;
        cam = cameraControl.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        jump = jumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        fire -= Time.deltaTime;
        slideTimer -= Time.deltaTime;
        Quaternion rotate = Quaternion.Euler(new Vector3(0, cam.ytate, 0));
        proj = Quaternion.Euler(new Vector3(cam.xtate, cam.ytate, cam.ztate));
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (!Slide)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime * Vertical, Space.Self);
            transform.Translate(Vector3.right * speed * Time.deltaTime * Horizontal, Space.Self);
            transform.rotation = rotate;
        }
        if (Input.GetMouseButtonDown(0) && fire < 0)
        {
            Rigidbody bullet;
            bullet = Instantiate(laser, transform.position, cameraControl.rotation) as Rigidbody;
            bullet.AddRelativeForce(Vector3.forward * 100f, ForceMode.Impulse);
            fire = fireRate;
        }
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
            if (wallCling && !Grounded)
            {
                rb.AddForce(Vector3.up * 250f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(Vector3.forward * DashForce, ForceMode.Impulse);
            slideTimer = 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Grounded == true)
        {
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(Vector3.forward * SlideForce, ForceMode.Impulse);
            slideTimer = 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Grounded == false)
        {
            rb.AddRelativeForce(Vector3.down * 30f, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Slide = true;
        }
        else
        {
            Slide = false;
        }
        if (Horizontal == 0 && Vertical == 0 && Grounded && rb.velocity.x != 0 && rb.velocity.z != 0 && slideTimer < 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = jumpAmount;
            Grounded = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallCling = true;
        }
    }
}
