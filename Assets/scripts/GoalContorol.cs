using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalContorol : MonoBehaviour {
    #region private variables
    [SerializeField]private Text TXT_Score_Player;
    private float score=0;
    #endregion
    #region public variables
    #endregion
    #region private methods
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ball")
        {
            score++;
            TXT_Score_Player.text = score.ToString();
        }
    }
    
    #endregion
    #region public methods
    #endregion
}
