using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowContoroler : MonoBehaviour {
    #region private variables
    [SerializeField]private SpriteRenderer SR;
    [SerializeField]private Sprite[] spitrs;
    #endregion
    #region public variables
    #endregion
    #region private methods 
    private void FixedUpdate(){
     //   MoveShadow();
    }
    private void MoveShadow(){
        Vector3 temp;
        temp = new Vector3(transform.position.x, -1.95f, 0);
        transform.position = temp;
    }
    private void HideShadow()
    {
        SR.sprite = spitrs[0];
       Invoke("ShowShadow", 1f);
    }
    private void ShowShadow()
    {
        SR.sprite = spitrs[1];
    }
    #endregion
    #region public methods
    public void HideingShadow()
    {
        HideShadow();
    }
    #endregion
    }

