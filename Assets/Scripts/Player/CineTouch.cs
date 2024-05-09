using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CineTouch : MonoBehaviour
{
    CinemachineFreeLook cineCam;
    [SerializeField] TouchField touchField;

    [SerializeField] float sensitivity = 0.1f;

    private void Awake()
    {
        cineCam = GetComponent<CinemachineFreeLook>();
        if (touchField != null)
        {
            cineCam.m_XAxis.m_InputAxisName = "";
        }
    }

    void Update()
    {
        if (touchField != null)
        {
            cineCam.m_XAxis.Value += touchField.TouchDist.x * sensitivity;
        }
    }
}
