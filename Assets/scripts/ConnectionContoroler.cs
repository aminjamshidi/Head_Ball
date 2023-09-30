using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class ConnectionContoroler : MonoBehaviour
{
    #region private variables
    [SerializeField]private Button startbtn;
    [SerializeField]private GameObject ballgame;
    [SerializeField]private Button[] Powerbuttonsleft;
    [SerializeField]private Button[] Powerbuttonsright;
    private GameObject current;
    private GameObject other;
    private PlayerContoroler playercontoroler,selfplayercontoroler;
    private SpriteRenderer SRball;
    [SerializeField]private Sprite[] sprites_ball;
    private SpriteRenderer[] SRplayer;
    [SerializeField]private Sprite[] sprites_player;
    #endregion
    #region public variables
    public SocketIOComponent socket;
    public JoyStick joystick;
    public PlayerRepository playerrepo;
    public Sprite t;
    #endregion
    #region private methods
    private void Start()
    {
        socket.On("sendproperty", StartGame);
        socket.On("OTHERPLAY", StartGameForOther);
       socket.On("movetoleft", OtherMoveingToLeft);
       socket.On("movetoright", OtherMoveingToRight);
       socket.On("stop", OtherStopMoveing);
       socket.On("stright", OtherStrightShooting);
       socket.On("chip", OtherChipShooting);
       socket.On("jump", OtherJumping);
       socket.On("hide", OtherHideBall);
       socket.On("hideplayer", OtherHideSelf);
    }
    private void StartGameForOther(SocketIOEvent evt)
    {
        if (JsonToString(evt.data.GetField("point").ToString(), "\"") == "1")
        {
            other = (GameObject)Instantiate(playerrepo.GetCurrentLeftPlayer(), new Vector3(-6.5f, -1.3f, 0f), Quaternion.identity);
        }
        else
        {
            other = (GameObject)Instantiate(playerrepo.GetCurrentRightPlayer(), new Vector3(7.3f, -1.3f, 0f), Quaternion.identity);
        }
        playercontoroler = other.GetComponent<PlayerContoroler>();
        playercontoroler.ball = ballgame.GetComponent<Rigidbody2D>();
    }
    private void StartGame(SocketIOEvent evt)
    {
        ballgame = (GameObject)Instantiate(ballgame, new Vector3(0, 0, 0), Quaternion.identity);
       if (JsonToString(evt.data.GetField("point").ToString(),"\"")=="1")
        {
            current= (GameObject)Instantiate(playerrepo.GetCurrentLeftPlayer(), new Vector3(-6.5f, -1.3f, 0f), Quaternion.identity);
            joystick.Attach(current.GetComponent<PlayerContoroler>());
            Powerbuttonsleft[0].gameObject.SetActive(true);
            Powerbuttonsleft[1].gameObject.SetActive(true);
       }
       else
        {
            current= (GameObject)Instantiate(playerrepo.GetCurrentRightPlayer(), new Vector3(7.3f, -1.3f, 0f), Quaternion.identity);
            joystick.Attach(current.GetComponent<PlayerContoroler>());
            Powerbuttonsright[0].gameObject.SetActive(true);
            Powerbuttonsright[1].gameObject.SetActive(true);
        }
       startbtn.gameObject.SetActive(false);
       selfplayercontoroler = current.GetComponent<PlayerContoroler>();
       selfplayercontoroler.ball = ballgame.GetComponent<Rigidbody2D>();
    }
    private string JsonToString(string x, string y)
    {
        string[] Nst = Regex.Split(x, y);
        return Nst[0];
    }
    private void OtherMoveingToLeft(SocketIOEvent evt)
    {
        playercontoroler.FunctionOfBTN_LeftMoveing();
    }
    private void OtherMoveingToRight(SocketIOEvent evt)
    {
        playercontoroler.FunctionOfBTN_RightMoveing();
    }
    private void OtherStopMoveing(SocketIOEvent evt)
    {
        playercontoroler.FunctionOfStopMoveing();
    }
    private void OtherJumping(SocketIOEvent evt)
    {
        playercontoroler.HeadShoot();
    }
    private void OtherStrightShooting(SocketIOEvent evt)
    {
        playercontoroler.StraightShooting();
    }
    private void OtherChipShooting(SocketIOEvent evt)
    {
        playercontoroler.chipShooting();
    }
    private void OtherHideBall(SocketIOEvent evt)
    {
        StartCoroutine(HideBall());
    }
    private IEnumerator HideBall()
    {
        SRball = ballgame.GetComponent<SpriteRenderer>();
        SRball.sprite = sprites_ball[1];
        yield return new WaitForSecondsRealtime(5f);
        SRball.sprite = sprites_ball[0];
    }
    private void OtherHideSelf(SocketIOEvent evt)
    {
        StartCoroutine(HideSelf());
    }
    private IEnumerator HideSelf()
    {
        yield return new WaitForSecondsRealtime(5f);
        for (int i = 0; i < 5; i++)
        {
            selfplayercontoroler.GetPlayerParts(i).GetComponent<SpriteRenderer>().sprite = t;
        }
    }
    #endregion
    #region public methods
    public void ClickToStart()
    {
        socket.Emit("indicatevector");
    }
    public void MoveingToLeft()
    {
        socket.Emit("movetoleft");
    }
    public void MoveingToRight()
    {
        socket.Emit("movetoright");
    }
    public void Stoping()
    {
        socket.Emit("stop");
    }
    public void StrightShooting()
    {
        socket.Emit("stright");
    }
    public void ChipShooting()
    {
        socket.Emit("chip");
    }
    public void Jump()
    {
        socket.Emit("jump");
    }
    public void MakeBallInvisible()
    {
        socket.Emit("hide");
    }
    public void MakePlayerInvisible()
    {
        socket.Emit("hideplayer");
    }
    #endregion
}