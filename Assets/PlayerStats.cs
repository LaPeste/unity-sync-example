using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class PlayerStats : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public string Username { get; set; }

    public RealmInteger<int> Score { get; set; }

    public PlayerStats() { }

    public PlayerStats(string Username, int Score)
    {
        this.Username = Username;
        this.Score = Score;
    }
}