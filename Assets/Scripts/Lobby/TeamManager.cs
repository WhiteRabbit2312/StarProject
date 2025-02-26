using System;
using System.Collections.Generic;
using Fusion;
using StarProject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TeamManager : NetworkBehaviour
{
    [SerializeField] private NetworkObject _playerNamePrefab;
    [SerializeField] private Button _redTeamButton; 
    [SerializeField] private Button _blackTeamButton;
    
    [SerializeField] private Transform _redPivot;
    [SerializeField] private Transform _blackPivot;
    [SerializeField] private Transform _nonePivot;

    private PlayerData _playerData;
    [Inject]
    public void Construct(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
    [Networked] private NetworkDictionary<string, Team> Teams => default;
    private List<NetworkObject> _spawnedPlayers = new List<NetworkObject>();

    private void Awake()
    {
        _redTeamButton.onClick.AddListener(() => SelectTeam(Team.Red));
        _blackTeamButton.onClick.AddListener(() => SelectTeam(Team.Black));
    }

    public override void Spawned()
    {
        if (Teams.ContainsKey(_playerData.PlayerDataModel.PlayerName)) 
            return;
        Teams.Add(_playerData.PlayerDataModel.PlayerName, Team.None);
        DespawnPlayers();
        SpawnPlayerNames();
    }

    public void SelectTeam(Team team)
    {
        Teams.Set(_playerData.PlayerDataModel.PlayerName, team);
        DespawnPlayers();
        SpawnPlayerNames();
    }

    private void SpawnPlayerNames()
    {
        foreach (var item in Teams)
        {
            Transform pivot;
            switch (item.Value)
            {
                case Team.Red: pivot = _redPivot; break;
                case Team.Black: pivot = _blackPivot; break;
                default: pivot = _nonePivot; break;
            }
            NetworkObject playerName = Runner.Spawn(_playerNamePrefab, pivot.transform.position, Quaternion.identity);
            playerName.transform.SetParent(pivot.transform);
            playerName.GetComponent<TMP_Text>().text = _playerData.PlayerDataModel.PlayerName;
            
            _spawnedPlayers.Add(playerName);
        }
    }

    private void DespawnPlayers()
    {
        if(_spawnedPlayers.Count == 0) 
            return;
        foreach (var player in _spawnedPlayers)
        {
            Runner.Despawn(player);
        }
    }
}

public enum Team
{
    Red,
    Black,
    None
}
