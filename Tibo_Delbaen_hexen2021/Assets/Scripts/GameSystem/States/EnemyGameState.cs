using BoardSystem;
using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.States
{
    /*
    public class EnemyGameState : GameStateBase
    {

        private Board<HexenPiece> _board;

        private HexenPiece _player;

        public EnemyGameState(Board<HexenPiece> board, HexenPiece player)
        {
            _board = board;
            _player = player;
        }

        public override void OnEnter()
        {

            foreach (var enemyView in GameLoop.Instance.Enemies)
            {
                enemyView.UpdateMaterial(false);
            }

            MoveEnemies();

            UpdateEnemyTargets();

            StateMachine.MoveTo(GameStates.Player);
        }

        private void UpdateEnemyTargets()
        {
            // Find free tiles around player
            var _occupiedTiles = FindFreeTiles().Where(x => _board.PieceAt(x) != null).ToList();
            var _tiles = FindFreeTiles().Where(x => _board.PieceAt(x) == null).ToList();

            // Assign tile to enemies as target
            var _enemiesFiltered = _board.Enemies.Where(x => (x.Target == null && !_occupiedTiles.Contains(_board.TileOf(x)))).ToList();

            for (int i = 0; i < _enemiesFiltered.Count; i++)
            {
                // Exit if all tiles are covered
                if (_tiles.Count == 0)
                    break;

                var _enemy = _enemiesFiltered[i];

                // Check if enemy can reach tile
                var toTile = _tiles.First();
                var fromTile = _board.TileOf(_enemy);

                var _path = Tiles(fromTile, toTile);

                // Continue with others if no path found
                if (_path.Count == 0)
                    continue;

                // Remove tile from list to process
                _tiles.Remove(toTile);

                _enemy.Path = _path;
                _enemy.Target = toTile;

                SetMaterial(_enemy);
            }
        }

        private void SetMaterial(HexenPiece _enemy)
        {
            var _enemyView = GameLoop.Instance.Enemies.Find(x => x.Model == _enemy);
            if (_enemyView != null)
                _enemyView.UpdateMaterial(_enemy.Target != null);
        }

        private List<Tile> Tiles(Tile fromTile, Tile toTile)
        {
            List<Tile> NeighbourStrategy(Tile centerTile) => Neighbours(centerTile, _board);

            float DistanceStrategy(Tile ft, Tile tt) => Distance(ft, tt, _board);

            var pf = new AStarPathFinding<Tile>(NeighbourStrategy, DistanceStrategy, DistanceStrategy);

            return pf.Path(fromTile, toTile);
        }

        List<Tile> Neighbours(Tile tile, Board<HexenPiece> board)
        {
            var neighbours = new List<Tile>();

            var validTiles = new HexMovementHelper(board, tile, 1)
                .Radius(1)
                .GenerateTiles();

            foreach (var validTile in validTiles)
            {
                if (validTile != null && board.PieceAt(validTile) == null)
                    neighbours.Add(validTile);
            }

            return neighbours;
        }

        float Distance(Tile fromTile, Tile toTile, Board<HexenPiece> board)
        {
            var fromPosition = fromTile.Position;
            var toPosition = toTile.Position;

            var totalDistance = HexagonHelper.Distance(fromPosition.X, fromPosition.Y, fromPosition.Z, toPosition.X, toPosition.Y, toPosition.Z);

            return totalDistance;
        }

        private void MoveEnemies()
        {
            var _enemiesToMove = _board.Enemies.Where(x => x.Target != null).ToList();

            foreach (HexenPiece enemy in _enemiesToMove)
            {
                _board.Move(_board.TileOf(enemy), enemy.Target);
                enemy.Target = null;
            }
        }

        private List<Tile> FindFreeTiles()
        {
            // Find and highlight tiles around player
            var _highlightedTiles = new HexMovementHelper(_board, _player, 1)
                    .NorthEast(1)
                    .GenerateTiles();

            return _highlightedTiles;

            // Uncomment this to highlight the tiles around the player
            //_board.Highlight(_highlightedTiles);
        }

    }
    */
}
