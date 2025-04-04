using System.Collections;
using UnityEngine;

public class HOTFIXanimationOffset : MonoBehaviour
{
    public float randomDeviationScale = 0.2f;
    // Use this for initialization
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("offset", Random.value);
        anim.SetFloat("speedDeviation", Random.Range(1f - randomDeviationScale, 1f + randomDeviationScale));
    }

    // Update is called once per frame
    void Update()
    {

    }
}