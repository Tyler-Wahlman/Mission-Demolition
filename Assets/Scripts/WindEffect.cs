using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class WindEffect : MonoBehaviour
{
    static private WindEffect S;
 
    [Header("Inscribed")]
    [Tooltip("UI Text element to display wind direction and strength.")]
    public Text uitWind;
 
    [Header("Dynamic")]
    public Vector2 windForce;
 
    private void Awake()
    {
        S = this;
    }
 
    static public void NEW_WIND()
    {
        if (S == null) return;
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-1f, 1f);
        S.windForce = new Vector2(x, y);
        S.UpdateWindUI();
    }
 
    void UpdateWindUI()
    {
        if (uitWind == null) return;

        string dir = windForce.x > 0 ? ">>>" : "<<<";
        string strength = Mathf.Abs(windForce.x) > 3.5f ? "Strong" :
                          Mathf.Abs(windForce.x) > 1.5f ? "Medium" : "Light";
        uitWind.text = "Wind: " + dir + " " + strength;
    }
 
    static public Vector2 GET_WIND()
    {
        return S.windForce;
    }
}