using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using System.Threading.Tasks;

public class UserDataShow : MonoBehaviour
{
    [SerializeField] private Text _userData;

    private PlayerStats _playerStats;

    private Realm _realm;
    private GameManager _gameManager;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnFinishInit += Init;
    }

    private async Task Init()
    {
        _realm = _gameManager.Realm;
        _playerStats = _realm.Find<PlayerStats>("player1");
    }

    private void OnDestroy()
    {
        _gameManager.OnFinishInit -= Init;
    }

    void Update()
    {
    }
}