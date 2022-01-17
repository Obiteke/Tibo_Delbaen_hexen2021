using System.Collections.Generic;
using UnityEngine;
using BoardSystem;
using Utils;
using GameSystem.Views;
using GameSystem.Models;
using MoveSystem;
using GameSystem.MoveCommandProviders;
using System.Linq;
using System;
using System.Collections;
using ReplaySystem;
using StateSystem;
using GameSystem.States;
using DeckSystem;
using GameSystem.Models.Cards;

public class GameLoop : SingletonMonoBehaviour<GameLoop>
{
    #region Events

    public event EventHandler Initialized;

    #endregion

    #region Fields

    [SerializeField]
    PositionHelper _positionHelper = null;

    StateMachine<GameStateBase> _stateMachine;

    private CardBase _activeCard;

    private PlayerView _playerView;

    private List<Tile> _highlightedTiles = new List<Tile>();

    #endregion

    #region Properties

    public Board<HexenPiece> Board { get; private set; }
    public Deck<CardBase> Deck { get; private set; }
    public Hand<CardBase> Hand { get; private set; }
    public MoveManager<HexenPiece> MoveManager { get; internal set; }
    //public List<EnemyView> Enemies { get; } = new List<EnemyView>();

    //public List<GameObject> PieceViews = new List<GameObject>();
    #endregion

    #region Methods

    #region Initializations

    private void Awake()
    {
        if (Board == null)
            CreateBoard(3);

        CreateDeck();

        Deck.Shuffle(3);

        Hand = Deck.DealHand(5);

        _stateMachine = new StateMachine<GameStateBase>();

        var replayManager = new ReplayManager();

        MoveManager = new MoveManager<HexenPiece>(Board);

        var playGameState = new PlayGameState(Board, MoveManager);
        _stateMachine.RegisterState(GameStates.Start, playGameState);
        _stateMachine.RegisterState(GameStates.Play, playGameState);
        _stateMachine.RegisterState(GameStates.End, playGameState);
        _stateMachine.RegisterState(GameStates.Replay, new ReplayGameState(replayManager));
        _stateMachine.MoveTo(GameStates.Start);

        // Manual hexpiece click movement
        MoveManager.Register(PlayerMoveCommandProvider.Name, new PlayerMoveCommandProvider(playGameState, replayManager));
        MoveManager.Register(EnemyMoveCommandProvider.Name, new EnemyMoveCommandProvider(playGameState, replayManager));


    }

    private void Start()
    {
        //_stateMachine = new StateMachine<GameStateBase>();
        //
        //var replayManager = new ReplayManager();
        //
        //MoveManager = new MoveManager<HexenPiece>(Board);
        
        ConnectViewsToModel();
        
        FindPlayer();

        //var playGameState = new PlayGameState(Board, MoveManager);
        //_stateMachine.RegisterState(GameStates.Play, playGameState);
        //_stateMachine.RegisterState(GameStates.Replay, new ReplayGameState(replayManager));
        ////_stateMachine.RegisterState(GameStates.Player, new PlayerGameState(Board, _playerView.Model, Deck, Hand));
        ////_stateMachine.RegisterState(GameStates.Player, new PlayerGameState(Board, _playerView.Model, Deck, Hand, PlayerMaterial, EnemyMaterial));
        ////_stateMachine.RegisterState(GameStates.Enemy, new EnemyGameState(Board, _playerView.Model));
        ////_stateMachine.MoveTo(GameStates.Enemy);
        //
        //// Manual hexpiece click movement
        //MoveManager.Register(PlayerMoveCommandProvider.Name, new PlayerMoveCommandProvider(playGameState, replayManager));
        //MoveManager.Register(EnemyMoveCommandProvider.Name, new EnemyMoveCommandProvider(playGameState, replayManager));

        StartCoroutine(OnPostStart());
    }

    #endregion

    #region Instantiations

    private void CreateDeck()
    {
        Deck = new Deck<CardBase>();
    
        Dictionary<string, CardBase> _cards = new Dictionary<string, CardBase>() {
            { "Charge", new ChargeCard(Board) },
            { "Push", new PushCard(Board) },
            { "Swipe", new SwipeCard(Board) },
            { "Teleport", new TeleportCard(Board) },
            { "Bomb", new BombCard(Board) }
        };
    
        for (int i = 0; i < _cards.Count; i++)
        {
            Deck.RegisterCard(_cards.Keys.ElementAt(i), _cards.Values.ElementAt(i));
        }
    }

    public void CreateBoard(int radius)
    {
        Board = new Board<HexenPiece>(radius);
    }

    #endregion

    #region Manipulations


    public void Select(HexenPiece hexenPiece)
    {
        _stateMachine.CurrentState.Select(hexenPiece);
    }
    
    public void Select(Tile tile)
    {
        _stateMachine.CurrentState.Select(tile);
    }
    
    public void Select(IMoveCommand<HexenPiece> moveCommand)
    {
        _stateMachine.CurrentState.Select(moveCommand);
    }
    
    public void Play()
    {
        _stateMachine.MoveTo(GameStates.Play);
    }
    public void End()
    {
        _stateMachine.MoveTo(GameStates.End);
    }
    //public void OnCardDragStart(string card)
    //{
    //    _stateMachine.CurrentState.OnCardDragStart(card);
    //}
    //
    //public void OnCardReleased(Tile hoverTile, string card)
    //{
    //    _stateMachine.CurrentState.OnCardReleased(hoverTile, card);
    //}
    //
    //public void OnCardTileFocused(Tile hoverTile, bool entered)
    //{
    //    _stateMachine.CurrentState.OnCardTileFocused(hoverTile, entered);
    //}
    //
    //public void OnPointerEnterTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
    //{
    //    _stateMachine.CurrentState.OnPointerEnterTile(eventData, _model);
    //}
    //
    //public void OnPointerExitTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
    //{
    //    _stateMachine.CurrentState.OnPointerExitTile(eventData, _model);
    //}

    #endregion

    #region Playbacks

    public void Forward()
    {
        _stateMachine.CurrentState.Forward();
    }
    public void Backward()
    {
        _stateMachine.CurrentState.Backward();
    }
    #endregion

    #region Triggers

    internal void OnCardReleased(Tile focusedTile, string card)
    {
        if (_activeCard == null)
            return;

        Board.UnHighlight(_highlightedTiles);
        if (_highlightedTiles.Contains(focusedTile))
        {
            Tile tile = Board.TileOf(_playerView.Model);
            _activeCard.OnMouseReleased(tile, focusedTile);
            Hand.RemoveCard(card);
            Hand.FillHand();
        }
        else
            _activeCard = null;

        Debug.Log(_highlightedTiles);
        _highlightedTiles.Clear();
    }
    internal void OnCardDragStart(string card)
    {
        _activeCard = Deck.GetCardAction(card);
    }
    internal void OnCardTileFocused(Tile focusedTile, bool entered)
    {
        if (_activeCard == null)
            return;

        if (!entered)
        {
            Board.UnHighlight(_highlightedTiles);
            _highlightedTiles.Clear();
            return;
        }

        Tile _playerTile = Board.TileOf(_playerView.Model);
        _highlightedTiles = _activeCard.Tiles(_playerTile, focusedTile);
        Board.Highlight(_highlightedTiles);
    }


    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
    }
    
    IEnumerator OnPostStart()
    {
        yield return new WaitForEndOfFrame();
    
        OnInitialized(EventArgs.Empty);
    }
    
    #endregion

    #region Post-init single-fire methods

    private void ConnectViewsToModel()
    {
        var playerPieceViews = FindObjectsOfType<PlayerView>();
        foreach (var pieceView in playerPieceViews)
        {
            var boardPosition = _positionHelper.ToBoardPosition(pieceView.transform.localPosition);
    
            var tile = Board.TileAt(boardPosition);
    
            var piece = new HexenPiece();
    
            Board.Place(tile, piece);
            MoveManager.Register(piece, pieceView.MovementName);
    
            pieceView.Model = piece;

            //Board.Pieces.Add(piece);

            //PieceViews.Add(pieceView.gameObject);
        }
        var enemyPieceViews = FindObjectsOfType<EnemyView>();
        foreach (var pieceView in enemyPieceViews)
        {
            var boardPosition = _positionHelper.ToBoardPosition(pieceView.transform.localPosition);
    
            var tile = Board.TileAt(boardPosition);
    
            var piece = new HexenPiece();
    
            Board.Place(tile, piece);
            MoveManager.Register(piece, pieceView.MovementName);
    
            pieceView.Model = piece;
    
            Board.Enemies.Add(piece);

            //Board.Pieces.Add(piece);


            // Add enemy views here to keep it out of board
            //Enemies.Add(pieceView);

            //PieceViews.Add(pieceView.gameObject);
        }
    }
    private void FindPlayer()
    {
        _playerView = FindObjectOfType<PlayerView>();
    }
    #endregion

    #endregion
}
