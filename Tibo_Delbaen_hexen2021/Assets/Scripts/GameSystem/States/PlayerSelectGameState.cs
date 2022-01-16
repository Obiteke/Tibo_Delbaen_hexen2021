using BoardSystem;
using DeckSystem;
using GameSystem.Models;
using GameSystem.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.States
{/*
    public class PlayerSelectGameState : GameStateBase
    {
        private Board<HexenPiece> _board;

        private HexenPiece _player;

        private GameObject _playerView;

        private int _cardsPlayed;

        private int _currentPlayerIndex;

        private Material _playerMaterial, _enemyMaterial;

        public PlayerSelectGameState(Board<HexenPiece> board, HexenPiece player, Material playerMaterial, Material enemyMaterial)
        {
            _board = board;
            _player = player;
            _playerMaterial = playerMaterial;
            _enemyMaterial = enemyMaterial;
        }
        public override void OnEnter()
        {
            //enter the original player into _playerView
            if (_playerView == null)
                _playerView = GameLoop.Instance.PieceViews[_currentPlayerIndex];

            //unhighlight current player
            _playerView.GetComponentInChildren<MeshRenderer>().material = _enemyMaterial;

            //Select the next player
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= _board.Pieces.Count)
                _currentPlayerIndex = 0;//_board.Pieces.IndexOf(_player);
            _player = _board.Pieces[_currentPlayerIndex];
            PlayGameState.NewPlayer = _player;
            _playerView = GameLoop.Instance.PieceViews[_currentPlayerIndex];

            //highlight current player
            _playerView.GetComponentInChildren<MeshRenderer>().material = _playerMaterial;

            StateMachine.MoveTo(GameStates.Player);
        }
    }
    */
}