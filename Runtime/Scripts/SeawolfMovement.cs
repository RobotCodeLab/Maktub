using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeawolfMovement : MonoBehaviour
{

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                                 0.0f,
                                 Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }
}
