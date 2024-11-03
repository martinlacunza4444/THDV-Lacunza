using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class Player
    {
        public string name;
        public int health;
        public int damage;
        public int speed;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();

    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 4 - 1;
        myPlayerList.player = new Player[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myPlayerList.player[i] = new Player();

            myPlayerList.player[i].name = data[4 * (i + 1)];
            int.TryParse(data[4 * (i + 1) + 2], out myPlayerList.player[i].health);
            int.TryParse(data[4 * (i + 1) + 1], out myPlayerList.player[i].damage);
            int.TryParse(data[4 * (i + 1) + 3], out myPlayerList.player[i].speed);

            Debug.Log($"Name: {myPlayerList.player[i].name}, Health: {myPlayerList.player[i].health}, Damage: {myPlayerList.player[i].damage}, speed: {myPlayerList.player[i].speed}");
        }
    }
}