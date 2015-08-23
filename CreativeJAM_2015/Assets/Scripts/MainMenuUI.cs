﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject creditsPanel;
    public AnimationCurve animCurve;

    void Awake() {
        FadeTexts(creditsPanel, 0, 0);
    }

    public void OnClickPlay() {
        StartCoroutine(RemovePanel());
    }

    public void OnClickCredits() {
        FadeTexts(mainPanel, 0);
        FadeTexts(creditsPanel, 1);
    }

    public void OnClickQuit() {
        Application.Quit();
    }

    public void OnClickReturn() {
        FadeTexts(creditsPanel, 0);
        FadeTexts(mainPanel, 1);
    }

    void FadeTexts(GameObject panel, float alphaTarget, float time = 0.5f) {
        Text[] array = panel.GetComponentsInChildren<Text>();
        for (int i = 0; i < array.Length; i++) {
            array[i].CrossFadeAlpha(alphaTarget, time, false);
        }
        Button[] arrayButton = panel.GetComponentsInChildren<Button>();
        for (int i = 0; i < arrayButton.Length; i++) {
            if (alphaTarget == 0) arrayButton[i].interactable = false;
            else arrayButton[i].interactable = true;
        }
    }

    IEnumerator RemovePanel() {
        yield return new WaitForEndOfFrame();
    }
}
