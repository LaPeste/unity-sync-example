using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using System.Threading.Tasks;

public class CustomUserDataShowLabel : MonoBehaviour
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

        if (_gameManager.CustomUserData)
        {
            var user = _gameManager.User;
            
            if (_gameManager.UserPrefs != null)
            {
                _userData.text += _gameManager.UserPrefs.ToString();
            }
            else
            {
                _userData.text += "None yet";
            }
        }
        else
        {
            _userData.text = "CustomUserData OFF";
        }
    }

    private void OnDestroy()
    {
        _gameManager.OnFinishInit -= Init;
    }

    void Update()
    {
    }
}