using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using System.Drawing;

public class UICheckTestScript : MonoBehaviour
{
    public HealthComponent Target;
    public Image Image;
    public TextMeshProUGUI Text;
    public GameObject ShowOnGameOver;
    public TextMeshProUGUI Flavor;
    public TextMeshProUGUI Rank;
    DifficultyThreshold GetDifficulty(int Points)
    {
        switch (Points)
        {
            case < (int)DifficultyThreshold.Green:
                return DifficultyThreshold.Blue;
            case < (int)DifficultyThreshold.Gray:
                return DifficultyThreshold.Green;
            case < (int)DifficultyThreshold.Black:
                return DifficultyThreshold.Gray;
            case < (int)DifficultyThreshold.White:
                return DifficultyThreshold.Black;
            case < (int)DifficultyThreshold.Purple:
                return DifficultyThreshold.White;
            case < (int)DifficultyThreshold.RGB:
                return DifficultyThreshold.Purple;
            case < (int)DifficultyThreshold.Progamer:
                return DifficultyThreshold.RGB;
            case >= (int)DifficultyThreshold.Progamer:
                return DifficultyThreshold.Progamer;
        }
    }

    String GetText(DifficultyThreshold diff)
    {
        return diff switch
        {
            DifficultyThreshold.Blue => "F,Failure",
            DifficultyThreshold.Green => "C,Commonfolk",
            DifficultyThreshold.Gray => "B,Brilliant",
            DifficultyThreshold.Black => "A,Are you Sans?????",
            DifficultyThreshold.White => "S,Super Professional",
            DifficultyThreshold.Purple => "SS,Sick Skills",
            DifficultyThreshold.RGB => "SSS,Supreme Sour Smore",
            DifficultyThreshold.Progamer => "J,Jelly Master",
            _ => "the., what",
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

        if (!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Image.fillAmount = Target.Health / Target.MaxHealth;
        Text.text = Target.Health + " / " + Target.MaxHealth;

        if (Target.Health <= 0)
        {
            ShowOnGameOver.SetActive(true);
            string[] words = GetText(GetDifficulty(GameObject.FindObjectOfType<GameHandler>().Points)).Split(',');
            Rank.text = words[0];
            Flavor.text = words[1];
        }
    }
}
