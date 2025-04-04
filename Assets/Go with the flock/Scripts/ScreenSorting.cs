using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenSorting : MonoBehaviour
{
    SortingGroup group;

    static float density = 2;

    private void Start()
    {
        group = GetComponent<SortingGroup>();
    }

    private void Update()
    {
        group.sortingOrder = -Mathf.RoundToInt(CameraController.Instance.transform.InverseTransformPoint(transform.position).y * density);
    }

}