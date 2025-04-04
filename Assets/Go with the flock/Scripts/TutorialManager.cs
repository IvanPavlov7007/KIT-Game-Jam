using System.Collections;
using UnityEngine;
using TMPro;
using Pixelplacement;

public class TutorialManager : Singleton<TutorialManager>
{

    public GameObject helloTut;
    public GameObject rulesTut;
    public GameObject fightTut;

    bool pressedThisFrame;
    static bool showed = false;

    private IEnumerator Start()
    {
        helloTut.SetActive(false);
        rulesTut.SetActive(false);
        fightTut.SetActive(false);

        if (showed)
            yield return null;
        else
        {
            InputManager.Instance.onMousePressed += onContinue;
            InputManager.Instance.onAccept += onContinue;
            yield return new WaitForSeconds(2f);
            PlayerController.Instance.enabled = false;
            helloTut.SetActive(true);
            yield return new WaitUntil(() => pressedThisFrame);
            pressedThisFrame = false;
            helloTut.SetActive(false);
            PlayerController.Instance.enabled = true;
        }
    }

    public bool showEncounterExplanation(System.Action callback)
    {
        if (!showed)
        {
            StartCoroutine(encounterExplanation(callback));
            showed = true;
            return true;
        }
        return false;
    }

    IEnumerator encounterExplanation(System.Action callback)
    {
        PlayerController.Instance.enabled = false;
        rulesTut.SetActive(true);
        yield return new WaitUntil(() => pressedThisFrame);
        pressedThisFrame = false;
        rulesTut.SetActive(false);
        PlayerController.Instance.enabled = true;
        callback.Invoke();
    }

    private void OnDestroy()
    {
        InputManager.Instance.onMousePressed -= onContinue;
        InputManager.Instance.onAccept -= onContinue;
    }
    void onContinue()
    {
        pressedThisFrame = true;
    }
}