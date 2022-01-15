using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRotate : MonoBehaviour
{
    public float rotationSpeedX;
    public float rotationSpeedY;
    public float rotationSpeedZ;

    private Quaternion rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeedX,rotationSpeedY,rotationSpeedZ) * Time.deltaTime);
    }
}
