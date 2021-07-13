using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using System.Threading.Tasks;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _score;

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


        if (_playerStats is null)
        {
            _realm.Write(() =>
            {
                _playerStats = _realm.Add(new PlayerStats("player1", 0));
            });
        }

        _score.text = "Score: " + _playerStats.Score.ToString();
        _realm.RealmChanged += (sender, eventArgs) =>
        {
            _score.text = "Score: " + _playerStats.Score.ToString();
        };
    }

    private void OnDestroy()
    {
        _gameManager.OnFinishInit -= Init;
    }

    void Update()
    {
    }
}