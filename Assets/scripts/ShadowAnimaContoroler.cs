using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimaContoroler : MonoBehaviour
{
    #region private variables
    [SerializeField]private Animator anim_shadow;
    #endregion
    #region public variables
    #endregion
    #region private methods
    private void Start()
    {
        StartCoroutine(DestroyThis());
    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds((anim_shadow.GetCurrentAnimatorStateInfo(0).length));
        Destroy(gameObject);
    } 
    #endregion
    #region public methods
    #endregion
}
