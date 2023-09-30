using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerContoroler : MonoBehaviour
{
    #region private variables
    [SerializeField]private Rigidbody2D RBplayer;
    [SerializeField]private GameObject anima_changesizeshadow;
    [SerializeField]private GameObject[] playerparts;
    [SerializeField]private float speed;
    [SerializeField]private float SpeedOfShooting;
    [SerializeField] private float yBallForce;
    [SerializeField] private float xBallForce;
    [SerializeField] private float distanseWithBallMax;
    [SerializeField]private float distanseWithBallMin;
    [SerializeField]private float speedofjumping;
    [SerializeField]private int directionofplayer;
    private int direction;
    //in 2 parametr ziri baraye in hast ke player natavanad poshtesarham paresh konad va hengami ke be zamin resid betavanad dobareh paresh konad
    private float lastjump;
    private float jumprate = 2f;
    #endregion
    #region public variables
    public ShadowContoroler shadowcontorol;
    public Rigidbody2D ball;
    #endregion
    #region private methods
    private void MoveingPlayerToRight()
    {
        direction = 1;
        playerparts[4].transform.Rotate(new Vector3(0, 0, 10)*directionofplayer);
    }
    private void MoveingPlayerToLeft()
    {
        direction = -1;
        playerparts[4].transform.Rotate(new Vector3(0, 0, -20) * directionofplayer);
    }
    private void StopingPlayer()
    {
        direction = 0;
        if (directionofplayer == 1)
        {
            playerparts[4].transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (directionofplayer == -1)
        {
            playerparts[4].transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
    private void CheckAndSetBoundOfPlayer()//baraye biroon naraftan player az sahneh
    {
        Vector3 temp = transform.position;
        temp = new Vector3(Mathf.Clamp(transform.position.x, -7.4f, 7.5f),Mathf.Clamp(transform.position.y,-1.6f,0.8f),0);
        transform.position = temp;
    }
    private void MoveingPlayer()//baraye harkat player bedoneh dar nazar gereftan jahat harkat
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        CheckAndSetBoundOfPlayer();
    }
    private void Update() { MoveingPlayer(); }
    #endregion
    #region public methods
    // in 3 method baraye erjae be class joystick hastan ta az in tarinq besh ba dokmeha dakhel sahneh player harkat dad
    // yeki braye harkat be samt rast chap va digari baraye samt rast hast va an yeki ham vase stop harkat player hast
    public void FunctionOfBTN_LeftMoveing() { MoveingPlayerToLeft(); }
    public void FunctionOfBTN_RightMoveing() { MoveingPlayerToRight(); }
    public void FunctionOfStopMoveing() { StopingPlayer(); }
    public void StraightShooting()
    {
        yBallForce = 0;
        xBallForce = 350*directionofplayer;
        distanseWithBallMax = 1.3f;
        distanseWithBallMin = 0.6f;
        StartCoroutine(shootWithDelay(playerparts[4]));
    }
    public void chipShooting()
    {
        yBallForce = 350*directionofplayer;
        xBallForce = 400*directionofplayer;
        distanseWithBallMax = 1.3f;
        distanseWithBallMin = 0.6f;
        StartCoroutine(shootWithDelay(playerparts[4]));
    }
    public void HeadShoot()
    {
        xBallForce = 0;
        yBallForce = 0;
        if (Time.time > lastjump + jumprate)
        {
            RBplayer.velocity = Vector2.up * speedofjumping;
            StartCoroutine(shootWithDelay(playerparts[1]));
            Instantiate(anima_changesizeshadow,new Vector3(transform.position.x,-1.95f,0),Quaternion.identity);
            shadowcontorol.HideingShadow();
            lastjump = Time.time;
        }
    }
    IEnumerator shootWithDelay(GameObject shoter)
    {
        shoter.transform.Rotate(0, 0, 20);
        float distX, distY ,distance;
        distX = shoter.transform.position.x - ball.position.x;
        distY = shoter.transform.position.y - ball.position.y;
        distance = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));
        if ( distance>=distanseWithBallMin && distance <= distanseWithBallMax)
        {
            ball.AddRelativeForce(new Vector3(xBallForce,yBallForce, 0));
        }
        yield return new WaitForSecondsRealtime(0.1f);
        shoter.transform.Rotate(0, 0, -20);
        
    }
    public GameObject GetPlayerParts(int x)
    {
        return playerparts[x];
    }
    #endregion
}
