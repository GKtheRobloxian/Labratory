using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Quaternion proj;
    public Rigidbody laser;
    public Transform firePoint;
    public BoxCollider collide;
    public BoxCollider crouchCollide;
    float fireRate = 0.49f;
    float fire;
    float dashRate = 2.0f;
    float dash;
    Vector3 startPos;
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
        startPos = transform.position;
        dash = 0f;
        fire = fireRate;
        Cursor.lockState = CursorLockMode.Locked;
        cam = cameraControl.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        jump = jumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        AdvancedMovement();
        Firing();
    }

    void Firing()
    {
        fire -= Time.deltaTime;
        proj = Quaternion.Euler(new Vector3(cam.xtate, cam.ytate, cam.ztate));
        if (Input.GetMouseButtonDown(0) && fire < 0)
        {
            Rigidbody bullet;
            bullet = Instantiate(laser, transform.position, cameraControl.rotation) as Rigidbody;
            bullet.AddRelativeForce(Vector3.forward * 100f, ForceMode.Impulse);
            fire = fireRate;
        }
    }

    void BasicMovement()
    {
        Quaternion rotate = Quaternion.Euler(new Vector3(0, cam.ytate, 0));
        transform.rotation = rotate;
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        if (!Slide)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime * Vertical);
            transform.Translate(Vector3.right * speed * Time.deltaTime * Horizontal);
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
    }

    void AdvancedMovement()
    {
        slideTimer -= Time.deltaTime;
        dash -= Time.deltaTime;
        if (dash < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !Slide)
            {
                rb.AddRelativeForce(Vector3.forward * DashForce, ForceMode.Impulse);
                slideTimer = 1.5f;
                dash = dashRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Grounded == true && slideTimer < 0)
        {
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
            collide.isTrigger = true;
            crouchCollide.isTrigger = false;
        }
        else
        {
            Slide = false;
            collide.isTrigger = false;
            crouchCollide.isTrigger = true;
        }
        if (Horizontal == 0 && Vertical == 0 && Grounded && rb.velocity.x != 0 && rb.velocity.z != 0 && slideTimer < 0)
        {
            rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, 0.01f), Mathf.Lerp(rb.velocity.y, 0, 0.01f), Mathf.Lerp(rb.velocity.z, 0, 0.01f));
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
