using System.Collections;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using TMPro;

public class ClashManager : Singleton<ClashManager>
{
    public GameObject panel;

    private void Start()
    {
        Hide();
    }

    public void ShowDecision(Mind a, Mind b)
    {
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }
}