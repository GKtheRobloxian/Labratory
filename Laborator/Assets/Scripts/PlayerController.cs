using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform firePoint;
    public BoxCollider collide;
    public BoxCollider crouchCollide;
    float dashRate = 0.75f;
    float dash;
    Vector3 startPos;
    public float speed;
    public float Horizontal;
    public float Vertical;
    public float DashForce;
    public float SlideForce;
    public float maxStamina;
    public float stamina;
    public int startingHealth;
    public int maximumHealth;
    public float staminaFillRate;
    int health;
    public Transform cameraControl;
    bool Grounded;
    bool Slide;
    bool wallCling = false;
    Camera cam;
    float slideTimer;
    public float dashBoost = 0.5f;
    float jumpAmount = 2;
    float jump;
    Rigidbody rb;
    GameObject canva;
    UltraInstinct uI;
    GameObject staminar;
    StaminaBar staminaControl;
    Vector3 MoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        staminar = GameObject.Find("Stamina");
        staminaControl = staminar.GetComponent<StaminaBar>();
        stamina = maxStamina;
        canva = GameObject.FindGameObjectWithTag("Canvas");
        uI = canva.GetComponent<UltraInstinct>();
        startPos = transform.position;
        dash = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        cam = cameraControl.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        jump = jumpAmount;
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        AdvancedMovement();
        HealthCheck();
        StaminaHandling();
    }

    void BasicMovement()
    {
        Quaternion rotate = Quaternion.Euler(new Vector3(0, cam.ytate, 0));
        transform.rotation = rotate;
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        if (!Slide)
        {
            RaycastHit hit;
            var them = Physics.Raycast(transform.position, new Vector3 (rb.velocity.x, 0, rb.velocity.z), out hit, 10);
            if (!them)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime * Vertical);
                transform.Translate(Vector3.right * speed * Time.deltaTime * Horizontal);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump <= 0)
            {
                return;
            }
            else
            {
                rb.AddForce(Vector3.up * 200f);
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
        dashBoost -= Time.deltaTime;
        if (dash < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !Slide && stamina >= 1)
            {
                if (Mathf.Abs(rb.velocity.x) > 0 && Mathf.Abs(rb.velocity.z) > 0)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                    rb.AddRelativeForce(Vector3.forward * DashForce, ForceMode.Impulse);
                    slideTimer = 1.5f;
                    dash = dashRate;
                    dashBoost = 0.5f;
                    stamina -= 1;
                }
                else if (rb.velocity.x == 0 && rb.velocity.z == 0)
                {
                    rb.AddRelativeForce(Vector3.forward*DashForce, ForceMode.Impulse);
                    slideTimer = 1.5f;
                    dash = dashRate;
                    dashBoost = 0.5f;
                    stamina -= 1;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Grounded == true && slideTimer < 0)
        {
            rb.AddRelativeForce(Vector3.forward * SlideForce, ForceMode.Impulse);
            slideTimer = 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Grounded == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddRelativeForce(Vector3.down * 60f, ForceMode.Impulse);
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

    public void Bloodshed (int Heal)
    {
        health += Heal;
    }

    void HealthCheck ()
    {
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }
        uI.SetValue(health);
    }

    void StaminaHandling()
    {
        Debug.Log(stamina);
        stamina += Time.deltaTime * staminaFillRate;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        staminaControl.SetValue(stamina);
        staminaControl.SetText(stamina);
    }
}
