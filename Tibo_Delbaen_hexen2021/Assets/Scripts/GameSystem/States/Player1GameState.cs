using BoardSystem;
using DeckSystem;
using GameSystem.Models;
using GameSystem.Models.Cards;
using MoveSystem;
using System.Collections.Generic;

namespace GameSystem.States
{
    public class Player1GameState : GameStateBase
    {
        private Board<HexenPiece> _board;

        private HexenPiece _player;

        //private GameObject _playerView;

        private CardBase _activeCard;

        private List<Tile> _highlightedTiles = new List<Tile>();

        private Deck<CardBase> _deck;
        private Hand<CardBase> _hand;
        private GameLoop _gameLoop;
        private int _cardsPlayed;

        //private int _currentPlayerIndex;

        //private Material _playerMaterial, _enemyMaterial;

        //public PlayerGameState(Board<HexenPiece> board, HexenPiece player, Deck<CardBase> deck, Hand<CardBase> hand, Material playerMaterial, Material enemyMaterial)
        public Player1GameState(Board<HexenPiece> board, HexenPiece player, Deck<CardBase> deck, Hand<CardBase> hand, GameLoop gameLoop)
        {
            _board = board;
            _player = player;
            _deck = deck;
            _hand = hand;
            _gameLoop = gameLoop;
        }
        public override void OnEnter()
        {
            _cardsPlayed = 0;
            //_player = NewPlayer;
        }
        public override void OnExit()
        {
            _gameLoop.UIChange();
        }
        public override void OnCardReleased(Tile focusedTile, string card)
        {
            if (_activeCard == null)
                return;

            _board.UnHighlight(_highlightedTiles);
            if (_highlightedTiles.Contains(focusedTile))
            {
                Tile tile = _board.TileOf(_player);
                _activeCard.OnMouseReleased(tile, focusedTile);
                _hand.RemoveCard(card);
                _hand.FillHand();
                _cardsPlayed++;
            }
            else
                _activeCard = null;

            _highlightedTiles.Clear();

            if (_cardsPlayed == 1)
                StateMachine.MoveTo(GameStates.Player2);
            //{
            //    StateMachine.MoveTo(GameStates.Select);
            //}

        }
        public override void OnCardDragStart(string card)
        {
            _activeCard = _deck.GetCardAction(card);
        }
        public override void OnCardTileFocused(Tile focusedTile, bool entered)
        {
            if (_activeCard == null)
                return;

            if (!entered)
            {
                _board.UnHighlight(_highlightedTiles);
                _highlightedTiles.Clear();
                return;
            }

            Tile _playerTile = _board.TileOf(_player);
            _highlightedTiles = _activeCard.Tiles(_playerTile, focusedTile);
            _board.Highlight(_highlightedTiles);
        }
        public override void OnPointerEnterTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
        {
            var activeCard = eventData.pointerDrag;

            HexenPiece piece = _board.PieceAt(_model);
            if (activeCard == null && piece != null && piece.Target != null)
            {
                _highlightedTiles = piece.Path;
                _board.Highlight(_highlightedTiles);
            }

            if (activeCard == null || activeCard.GetComponent<CardView>() == null)
                return;

            GameLoop.Instance.OnCardTileFocused(_model, true);
        }
        public override void OnPointerExitTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
        {
            _board.UnHighlight(_highlightedTiles);
            var activeCard = eventData.pointerDrag;
            if (activeCard == null || activeCard.GetComponent<CardView>() == null)
                return;

            GameLoop.Instance.OnCardTileFocused(_model, false);
        }
    }
}
