using System;
using UnityEngine;
using Zenject;

namespace StarProject
{
    public class CausedDamage
    {
        private Database _database;
        private int _damage;
        
        [Inject]
        public void Construct(Database database)
        {
            _database = database;
        }
        
        
    }
}