using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarUI : MonoBehaviour
{
    [SerializeField] BaseHealth character;
    [SerializeField] Image lifebar;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cameraTransform);
    }

    public void UpdateLifeBar(BaseHealth character)
    {
        float currentLife = character.CurrentLife;
        float maxLife = character.MaxLife;
        float lifePercent = Mathf.Clamp(currentLife / maxLife, 0, 1);
        lifebar.fillAmount = lifePercent;
    }
}
