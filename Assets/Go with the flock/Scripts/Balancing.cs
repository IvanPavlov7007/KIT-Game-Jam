using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Balancing : Singleton<Balancing>
{
    [SerializeField] BalancingData viewSightBalancing;
    [SerializeField]
    BalancingData speedBalancing = new BalancingData
    {
        a = 0.9F,
        b = 0F,
        p = 0.5f,
        MaxValue = 5f
    };

    public float adjustedSpeed(int speedPoints)
    {
        return powGraph(speedBalancing, speedPoints);
    }

    public float adjustedViewSightT(int viewPoints)
    {
        return powGraph(viewSightBalancing, viewPoints);
    }

    static float powGraph(BalancingData data, float x)
    {
        float val = data.a * Mathf.Pow(x + data.b, data.p);
        if (val > data.MaxValue)
            val = data.MaxValue;
        return val;
    }

    [System.Serializable]
    public struct BalancingData
    {
        public float a;
        public float b;
        public float p;
        public float MaxValue;
        public float MaxX;
    }
}
