using UnityEngine;

[ExecuteAlways]
public class Billboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 dir = cam.transform.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }
}