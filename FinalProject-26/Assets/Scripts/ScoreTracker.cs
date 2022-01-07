using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    private int blueScore;
    private int redScore;

    // Start is called before the first frame update
    void Start()
    {
        blueScore = 0;
        redScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (redScore == 3) {
            // end game; red wins
        }
        else if (blueScore == 3) {
            // end game; blue wins
        }
    }

    public void redScored()
    {
        redScore++;
        Debug.Log("Red Scored, current score is " + redScore);

    }
    
    public void blueScored()
    {
        blueScore++;
        Debug.Log("Blue Scored, current score is " + blueScore);
    }
}
