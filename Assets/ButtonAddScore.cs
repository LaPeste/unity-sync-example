using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;

public class ButtonAddScore : MonoBehaviour
{
    [SerializeField] private Button _addScoreBtn;

    private PlayerStats _playerStats;
    private GameManager _gameManager;
    private Realm _realm;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _addScoreBtn.onClick.AddListener(AddScoreOnClick);
    }

    void AddScoreOnClick()
    {
        if (_playerStats == null)
        {
            if (_realm == null)
            {
                _realm = _gameManager.Realm;
            }
            _playerStats = _realm.Find<PlayerStats>("player1");
            if (_playerStats == null)
            {
                _realm.Write(() =>
                {
                    _realm.Add(new PlayerStats("player1", 0));
                });
            }
        }
        _realm.Write(() =>
        {
            _playerStats.Score.Increment();
        });
    }
}