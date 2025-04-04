using System;
using System.Collections;
using UnityEngine;

public class PlayerMind : Mind, BaseMovement
{
    public Vector2 Direction { get => flock.Direction; 
        set => flock.Direction = value; }

    public override void initializeMind(FlockingMember flockingMember)
    {
        base.initializeMind(flockingMember);
        CameraController.Instance.selectTarget(flock);
    }

    public override void StartProcess(Process process, Action<Mind, ProcessDecision> callBack)
    {
        base.StartProcess(process, callBack);
        ClashManager.Instance.ShowDecision(this, process.mindA == this ? process.mindB : process.mindA);
        GameManager.Instance.StopTime();
    }

    public void DecideFight()
    {
        decisionCallback.Invoke(this, ProcessDecision.Fight);
        ClashManager.Instance.Hide();
        GameManager.Instance.ResumeTime();
    }

    public void DecideNone()
    {
        decisionCallback.Invoke(this, ProcessDecision.None);
        ClashManager.Instance.Hide();
        GameManager.Instance.ResumeTime();
    }

    public void DecideFlock()
    {
        decisionCallback.Invoke(this, ProcessDecision.Flock);
        ClashManager.Instance.Hide();
        GameManager.Instance.ResumeTime();
    }
}