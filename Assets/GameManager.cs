using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using System.Threading;
using Realms.Sync;
using System;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public delegate Task Initialization();
    public event Initialization OnFinishInit;

    public Realm Realm => _realm;
    private Realm _realm;

    public bool BackgroundThreadOnStart => _backgroundThreadOnStart;
    [SerializeField] private bool _backgroundThreadOnStart = false;

    public bool SyncedRealm => _syncedRealm;
    [SerializeField] private bool _syncedRealm = false;
    public bool CustomUserData => _customUserData && _syncedRealm;
    [SerializeField] private bool _customUserData = false;

    private App _app;
    public User User { get; private set; }
    private SyncConfiguration _config;

    public CustomUserData UserPrefs { get; private set; }
    private CustomUserData _userPref;

    private async void Start()
    {
        if (BackgroundThreadOnStart)
        {
            ThreadStart work = RealmOnThread;
            Thread thread = new Thread(work);
            thread.Start();
        }
        if (SyncedRealm)
        {
            _app = App.Create("unitysdktesting-btqpl");
            User = await _app.LogInAsync(Credentials.ApiKey("aT32bWajf4xJm9n6zpoARCSQ40rMZuUQb1ZMpaPPRqNPHGEXIJKdmvjk5QXRU50m"));
            _config = new SyncConfiguration("Development", User);
            _realm = await Realm.GetInstanceAsync(_config);

            if (CustomUserData)
            {
                var mongoClient = User.GetMongoClient("mongodb-atlas");
                var dbTracker = mongoClient.GetDatabase("UnityDevDB");
                var cudCollection = dbTracker.GetCollection<CustomUserData>("UserPrefs");
                UserPrefs = await cudCollection.FindOneAsync(new { _id = "player1" });
            }
            if (OnFinishInit != null) await OnFinishInit();
        }
        else
        {
            _realm = Realm.GetInstance();
        }
        //if (_syncedRealm)
        //{
        //    await _user.LogOutAsync();
        //}
    }

    private void OnDestroy()
    {
        _realm.Dispose();
    }

    private void RealmOnThread()
    {
        Realm realm = Realm.GetInstance();
        PlayerStats playerStats = realm.Find<PlayerStats>("player1");
        int rounds = 10;
        for (; rounds >= 0; rounds--)
        {
            realm.Write(() =>
            {
                playerStats.Score.Increment();
            });
            Thread.Sleep(1000);
        }
        realm.Dispose();
    }
}