using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Configuration")]
public class Configuration : ScriptableObject
{
    [field:SerializeField] public int Radius {get; private set;} = 1;
    [field:SerializeField] public float Power {get; private set;} = 4;
    [field:SerializeField] public GameObject BallPrefab {get; private set;}
}
