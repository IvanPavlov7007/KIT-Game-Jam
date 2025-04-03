using System.Collections;
using UnityEngine;

public class Process : MonoBehaviour
{
    public Mind mindA;
    public Mind mindB;

    protected ProcessDecision? decisionA;
    protected ProcessDecision? decisionB;

    public void InitProcess(Mind mindA, Mind mindB)
    {
        this.mindA = mindA;
        this.mindB = mindB;

        mindA.StartProcess(this, decisionMet);
        mindB.StartProcess(this, decisionMet);
    }

    public void decisionMet(Mind mind, ProcessDecision decision)
    {
        if (mind == mindA)
        {
            if (decisionA != null)
                Debug.LogWarning(mindA.name + " has already decided and now switches decision");
            decisionA = decision;
        }
        if (mind == mindB)
        {
            if (decisionB != null)
                Debug.LogWarning(mindA.name + " has already decided and now switches decision");
            decisionB = decision;
        }
        checkDecisionsFinished();
    }

    protected void checkDecisionsFinished()
    {
        if (!decisionA.HasValue || !decisionB.HasValue)
            return;

        if (decisionA == ProcessDecision.Fight || decisionB == ProcessDecision.Fight)
        {
            fightOutcome();
        }
        else if (decisionA == ProcessDecision.Flock && decisionB == ProcessDecision.Flock)
        {
            flockOutcome();
        }
        else
        {
            noneOutcome();
        }
    }

    protected void fightOutcome()
    {
        StartCoroutine(fightingProcess());
    }

    protected void flockOutcome()
    {
        StartCoroutine(allianceProcess());
    }

    protected void noneOutcome()
    {
        GameManager.Instance.resolveBypass(this, mindA, mindB);
    }

    IEnumerator allianceProcess()
    {
        //TODO pull minds, start particles
        yield return new WaitForEndOfFrame();
        GameManager.Instance.resolveAlliance(this, mindA, mindB);
    }

    IEnumerator fightingProcess()
    {
        //TODO pull minds, start particles
        yield return new WaitForEndOfFrame();
        var resultA = mindA.SufferFight(mindB);
        var resultB = mindB.SufferFight(mindA);
        GameManager.Instance.resolveFight(this, resultA, resultB);
    }
}

public enum ProcessDecision
{
    Fight,
    Flock,
    None
}