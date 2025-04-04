using System.Collections;
using UnityEngine;
using Pixelplacement;

public class CameraController : Singleton<CameraController>
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    Vector3 additionaloffset;
    Flock currentFlockTarget;

    public float minSize = 5f;
    public float maxSize = 20f;
    public int maxView = 50;

    [Space]
    public float flockVelocityMult = 1f;
    public float movementSmoothness = 0.9f;
    public float sizeSmoothness = 0.5f;
    public void selectTarget(Flock flock)
    {
        currentFlockTarget = flock;
    }

    private void smoothSize(float targetSize)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, sizeSmoothness * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        if (currentFlockTarget == null)
            return;
        float size = Mathf.Lerp(minSize, maxSize, Mathf.InverseLerp(0f, maxView, currentFlockTarget.stats.additionalView));
        smoothSize(size);
        Vector3 targetPos = currentFlockTarget.position + currentFlockTarget.velocity.normalized * flockVelocityMult + (Vector2) additionaloffset;
        targetPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, movementSmoothness);
    }
}