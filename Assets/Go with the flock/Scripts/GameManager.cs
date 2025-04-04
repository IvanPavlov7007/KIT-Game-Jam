using System.Collections;
using UnityEngine;
using Pixelplacement;
public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public PlayerMind playerMind;
    public AnimalBase firstAnimal;
    public Rect spawnRect = new Rect(-100f, -100f, 200f, 200f);

    void Start()
    {
        
        //SpawnManager.Instance.SpawnAnimals(spawnRect);
    }

    public Process initNewProcess(Mind a, Mind b)
    {
        Process process = new GameObject("process between " + a.name + " and " + b.name).AddComponent<Process>();
        process.InitProcess(a, b);
        return process;
    }

    public void resolveBypass(Process process, Mind mindA, Mind mindB)
    {
        mindA.StopProcess();
        mindB.StopProcess();
        Destroy(process.gameObject);
    }

    Mind selectSupreriorMind(Mind mindA, Mind mindB)
    {
        if (mindA == playerMind)
            return mindA;
        if (mindB == playerMind)
            return mindB;

        if (mindA.flock.animalsInFlock.Count >= mindB.flock.animalsInFlock.Count)
            return mindA;
        return mindB;
    }

    public void resolveAlliance(Process process, Mind mindA, Mind mindB)
    {

        Mind supreriorMind = selectSupreriorMind(mindA, mindB);
        Mind inferiorMind = supreriorMind == mindA ? mindB : mindA;
        Flock toTransfer = inferiorMind.flock;

        supreriorMind.growFlock(toTransfer);
        inferiorMind.DestroyMind();

        supreriorMind.StopProcess();

        Destroy(process.gameObject);
    }

    public void resolveFight(Process process, FightResult result1, FightResult result2)
    {
        if (result1.mind == playerMind && result1.shouldBeDestroyed)
        {
            GameOver();
        }
        if (result2.mind == playerMind && result2.shouldBeDestroyed)
        {
            GameOver();
        }

        processFightResult(result1);
        processFightResult(result2);
        Destroy(process.gameObject);

    }

    private void processFightResult(FightResult result)
    {
        if (result.shouldBeDestroyed)
        {
            result.mind.killFlock();
            result.mind.DestroyMind();
        }
        else
        {
            result.applySufferChanges(result);
        }
    }
    public Mind provideMind(FlockingMember flockingMember)
    {
        if(flockingMember.AnimalBase == firstAnimal)
        {
            playerMind.initializeMind(flockingMember);
            playerController.currentTarget = playerMind;
            return playerMind;
        }

        GameObject go = new GameObject(flockingMember.gameObject.name + "'s initial mind");
        var mind = go.AddComponent<GoodAIMind>();
        mind.initializeMind(flockingMember);
        return mind;
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}