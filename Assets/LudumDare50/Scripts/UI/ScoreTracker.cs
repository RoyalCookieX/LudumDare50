using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public float Score = 0;

    public void IncreaseScore(float amount)
    {
        Score += amount;
    }


    public void SetText()
    {
        text.text = "Score = " + Score;
    }


}
