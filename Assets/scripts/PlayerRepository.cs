using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepository : MonoBehaviour
{
    #region private variables
    #endregion
    #region public variables
    public GameObject playerOBJ_left;
    public GameObject playerOBJ_right;
    #endregion
    #region private methods
    #endregion
    #region public methods
    public GameObject GetCurrentLeftPlayer()
    {
        return playerOBJ_left;
    }
    public GameObject GetCurrentRightPlayer()
    {
        return playerOBJ_right;
    }
    #endregion
}
