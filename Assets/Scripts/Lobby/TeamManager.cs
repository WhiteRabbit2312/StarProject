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
    [SerializeField] private GameObject _playerNamePrefab;
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
    
    [Networked, OnChangedRender(nameof(UpdateTeams))] private NetworkDictionary<NetworkString<_32>, Team> _teams => default;


    private List<GameObject> _spawnedPlayers = new();

    private void Awake()
    {
        _redTeamButton.onClick.AddListener(() => SelectTeam(Team.Red));
        _blackTeamButton.onClick.AddListener(() => SelectTeam(Team.Black));
    }

    public override void Spawned()
    {
        RPC_AddName(_playerData.PlayerDataModel.PlayerName, Team.None);
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_AddName(string playerName, Team team)
    {
        if (!_teams.ContainsKey(playerName))
        {
            _teams.Add(playerName, team);

        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_ChangeTeam(string playerName, Team team)
    {
        _teams.Set(playerName, team);
    }

    private void UpdateTeams()
    {
        SpawnPlayerNames();
    }
    public void SelectTeam(Team team)
    {
        RPC_ChangeTeam(_playerData.PlayerDataModel.PlayerName, team);
    }

    private void SpawnPlayerNames()
    {
        DespawnPlayers();
        foreach (var item in _teams)
        {
            Transform pivot;
            switch (item.Value)
            {
                case Team.Red: pivot = _redPivot; break;
                case Team.Black: pivot = _blackPivot; break;
                default: pivot = _nonePivot; break;
            }
            GameObject playerName = Instantiate(_playerNamePrefab, pivot.transform.position, Quaternion.identity);
            playerName.transform.SetParent(pivot.transform);
            playerName.GetComponent<TMP_Text>().text = item.Key.ToString();
            Debug.LogWarning("playerName: " + _playerData.PlayerDataModel.PlayerName);
            _spawnedPlayers.Add(playerName);
        }
    }
    
    private void DespawnPlayers()
    {
        foreach (var player in _spawnedPlayers)
        {
            Destroy(player.gameObject);
        }
    }
}

public enum Team
{
    Red,
    Black,
    None
}
