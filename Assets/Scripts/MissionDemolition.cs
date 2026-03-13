using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public enum GameMode
{
    idle,
    playing,
    levelEnd
}
 
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;
 
    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;
 
    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";
 
    void Start()
    {
        S = this;
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }
 
    void StartLevel()
    {
        if (castle != null)
        {
            Destroy(castle);
        }
        Projectile.DESTROY_PROJECTILES();
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
        WindEffect.NEW_WIND();
    }
 
    void UpdateGUI()
    {
        if (uitLevel != null)
        {
            uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        }
        if (uitShots != null)
        {
            uitShots.text = "Shots Taken: " + shotsTaken;
        }
    }
 
    void Update()
    {
        UpdateGUI();
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            Goal.goalMet = false;
            SoundManager.STOP_WHIR();
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);
            Invoke("NextLevel", 2f);
        }
    }
 
    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            // Save shot count and load Game Over scene
            PlayerPrefs.SetInt("FinalShots", shotsTaken);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver"); // must match your scene name exactly
            return;
        }
        StartLevel();
    }
 
    static public void SHOT_FIRED()
    {
        S.shotsTaken++;
    }
 
    static public GameObject GET_CASTLE()
    {
        return S.castle;
    }
}