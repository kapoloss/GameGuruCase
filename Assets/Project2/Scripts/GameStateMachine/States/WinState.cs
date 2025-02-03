using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class WinState : IGameState
    {
        private readonly GameController _gameController;
    
        public WinState(GameController gameController)
        {
            _gameController = gameController;
        }

        public void OnEnter()
        { }
        
        public void Update() { }
        
        public void OnExit() { }
    }
}

