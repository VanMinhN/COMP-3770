using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


[CreateAssetMenu(fileName ="New Map Set", menuName ="Round/Map Set")]
public class ListMap : ScriptableObject
{
    [Scene]
    [SerializeField] private List<string> MapList = new List<string>(); //private so can't change

    public IReadOnlyCollection<string> ListofMaps => MapList.AsReadOnly();
}
