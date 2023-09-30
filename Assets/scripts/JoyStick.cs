using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    #region private variables
    private PlayerContoroler playercontoroler;
    #endregion
    #region public variables
    #endregion
    #region private methods
    #endregion
    #region public methods
    public void MoveToLeft() { playercontoroler.FunctionOfBTN_LeftMoveing(); }
    public void MoveToRight() { playercontoroler.FunctionOfBTN_RightMoveing(); }
    public void Stop() { playercontoroler.FunctionOfStopMoveing(); }
    public void Jump() { playercontoroler.HeadShoot(); }
    public void StrightShoot() { playercontoroler.StraightShooting(); }
    public void ChipShoot() { playercontoroler.chipShooting(); }
    public void Attach(PlayerContoroler P)
    {
        playercontoroler = P;

    }
    #endregion
}
