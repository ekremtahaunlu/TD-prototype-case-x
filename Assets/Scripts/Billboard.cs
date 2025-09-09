using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Billboard : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        // Y eksenini sabitleyerek döndür (upright billboard)
        Vector3 dir = cam.transform.position - transform.position;
        dir.y = 0f; // eğer tam 3D dönüş istersen bu satırı kaldır
        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }
}