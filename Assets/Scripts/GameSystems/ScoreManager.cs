using System;
using TMPro;
using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ReactiveProperty<int> score = new ReactiveProperty<int>(0);
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = score.ToString();
    }

    public void AddPoints(int points)
    {
        score.Value += points;
        _text.text = score.ToString();
    } 
}
