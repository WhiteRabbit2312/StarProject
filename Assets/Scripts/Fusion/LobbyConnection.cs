using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;

namespace StarProject
{
    public class LobbyConnection : NetworkBehaviour
    {
        private NetworkDictionary<PlayerRef, NetworkString<_32>> PlayerUserID => default;
        private GameStarter _gameStarter;
        private Database _database;
        [Inject]
        public void Construct(GameStarter gameStarter, Database database)
        {
            _gameStarter = gameStarter;
            _database = database;
        }
        
        public override void Spawned()
        {
            InitPlayers();
        }

        private void InitPlayers()
        {
            SetPlayer();
            int count = _gameStarter.NetworkRunner.ActivePlayers.Count();

            if (count == 1)
            {
                
            }
        }
        
        private void SetPlayer()
        {
            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();
            PlayerUserID.Set(playerRef1, _database.FirebaseUser.UserId);
        }
    }
}