using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//Code is an adaption/inspiration from Dapper Dino tutorial
// Link:www.youtube.com/c/DapperDinoCodingTutorials/playlists 
public class MapHandler 
{
    private readonly IReadOnlyCollection<string> maplist;

    private List<string> remainingMaps; //dynamic

    public MapHandler(ListMap mapList)
    {
        maplist = mapList.ListofMaps;
        ResetMaps();
    }

    public string NextMap
    {
        get
        {
            if(remainingMaps.Count == 0) { ResetMaps(); }
            string map = remainingMaps[UnityEngine.Random.Range(0, remainingMaps.Count)];
            remainingMaps.Remove(map);
            return map;
        }
    }

    private void ResetMaps()
    {
        remainingMaps = maplist.ToList();
    }
}
