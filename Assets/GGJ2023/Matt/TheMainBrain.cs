using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject
    {
        public GameObject prefab;
        public Color color;
        public int score;
        public string ARDK_id;
    }
public class TheMainBrain : MonoBehaviour
{
    public List<PlayerObject> players = new List<PlayerObject>();

    public PlayerObject CreatePlayer(string ARDK_id)
    {
        PlayerObject newPlayer = new PlayerObject();
        newPlayer.color = Random.ColorHSV();
        newPlayer.score = 0;
        newPlayer.ARDK_id = ARDK_id;
        players.Add(newPlayer);
        return newPlayer;
    }

    public PlayerObject GetPlayerByARDKID(string ARDK_id) {
        Debug.Log("Searching for player:" + ARDK_id);
        PlayerObject player = players.Find(p => p.ARDK_id == ARDK_id);
        return player;
    }
}

