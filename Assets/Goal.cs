using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goalText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGoalText();
    }

    // Update is called once per frame
    void UpdateGoalText()
    {
        if (GameManager.Instance.GetSelectedConfiguration().HasCubesLimit)
        {
            var _minCubes = GameManager.Instance.GetSelectedConfiguration().MinCubesToWin;
            var _minScore = GameManager.Instance.GetSelectedConfiguration().MinScoreToWin;
            goalText.text = $" Cubes : {_minCubes} \n " +
                $"Score : {_minScore}";
        }
        else
        {
            goalText.text = "Achieve the highest score";
        }

    }
}
