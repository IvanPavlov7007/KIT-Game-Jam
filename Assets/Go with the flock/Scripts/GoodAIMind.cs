using System;
using System.Collections;
using UnityEngine;
public class GoodAIMind : Mind
{
    public override void StartProcess(Process process, Action<Mind, ProcessDecision> callBack)
    {
        base.StartProcess(process, callBack);
        if (flock.animalsInFlock.Count == 1)
            callBack.Invoke(this, ProcessDecision.Flock);
        else
            callBack.Invoke(this, ProcessDecision.Fight);
    }


}