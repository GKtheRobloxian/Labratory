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
    public float xtate;
    public float ztate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xtate = transform.localEulerAngles.x;
        ytate = transform.localEulerAngles.y;
        ztate = transform.localEulerAngles.z;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position = player.transform.position - new Vector3 (0, 1.0f, 0);
        }
        else
        {
            transform.position = player.transform.position;
        }
        rotate = Input.GetAxis("Mouse X");
        votate = Input.GetAxis("Mouse Y");
        transform.RotateAround(transform.position, -Vector3.up, rotate * -720 * Time.deltaTime);
        transform.Rotate(-Vector3.right, votate * 720 * Time.deltaTime);
    }
}
