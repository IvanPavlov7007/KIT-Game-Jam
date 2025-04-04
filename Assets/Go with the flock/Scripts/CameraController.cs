using System.Collections;
using UnityEngine;
using Pixelplacement;

public class CameraController : Singleton<CameraController>
{
    [SerializeField]
    Camera cam;
    Flock currentFlockTarget;

    public float minSize = 5f;
    public float maxSize = 20f;
    public int maxView = 50;

    [Space]
    public float flockVelocityMult = 1f;
    public float smoothness = 0.9f;
    public void selectTarget(Flock flock)
    {
        currentFlockTarget = flock;
    }

    private void FixedUpdate()
    {
        if (currentFlockTarget == null)
            return;
        float size = Mathf.Lerp(minSize, maxSize, Mathf.InverseLerp(0f, maxView, currentFlockTarget.stats.additionalView));
        cam.orthographicSize = size;
        
        Vector3 targetPos = currentFlockTarget.position + currentFlockTarget.velocity * flockVelocityMult;
        targetPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothness * Time.fixedDeltaTime);
    }
}