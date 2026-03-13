using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class GameOverScene : MonoBehaviour
{
    [Header("Inscribed")]
    public Text uitFinalShots;
 
    void Start()
    {
        // Retrieve the shot count passed from the main game scene
        int shots = PlayerPrefs.GetInt("FinalShots", 0);
        uitFinalShots.text = "All castles demolished in " + shots + " shots!";
    }
 
    public void PlayAgain()
    {
        Debug.Log("PlayAgain called");
        PlayerPrefs.DeleteKey("FinalShots");
        SceneManager.LoadScene("MainScene"); // change to your main scene name
    }
}
