using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float rotate;
    public float votate;
    public float ytate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ytate = transform.localEulerAngles.y;
        transform.position = player.transform.position;
        rotate = Input.GetAxis("Mouse X");
        votate = Input.GetAxis("Mouse Y");
        if (transform.rotation.x < 90 && transform.rotation.x > -90)
        {
            transform.RotateAround(transform.position, -Vector3.up, rotate * -720 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3 (0, transform.rotation.y, transform.rotation.z));
        }
        transform.Rotate(-Vector3.right, votate * 720 * Time.deltaTime);
    }
}
