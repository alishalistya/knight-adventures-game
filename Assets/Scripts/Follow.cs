using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.transform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
