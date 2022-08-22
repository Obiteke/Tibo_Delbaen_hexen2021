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

    public GameObject uIHandPlayer1;
    public GameObject uIHandPlayer2;

    private CardBase _activeCard;

    private PlayerView _playerView;

    private List<Tile> _highlightedTiles = new List<Tile>();

    private List<HexenPiece> _playerPieces = new List<HexenPiece>();

    #endregion

    #region Properties

    public Board<HexenPiece> Board { get; private set; }
    public Deck<CardBase> Deck { get; private set; }
    public Hand<CardBase> HandPlayer1 { get; private set; }
    public Hand<CardBase> HandPlayer2 { get; private set; }
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

        HandPlayer1 = Deck.DealHand(5);
        HandPlayer2 = Deck.DealHand(5);

        _stateMachine = new StateMachine<GameStateBase>();

        var replayManager = new ReplayManager();

        MoveManager = new MoveManager<HexenPiece>(Board);


    }

    private void Start()
    {
        //_stateMachine = new StateMachine<GameStateBase>();
        //
        //var replayManager = new ReplayManager();
        //
        //MoveManager = new MoveManager<HexenPiece>(Board);
        
        ConnectViewsToModel();


        var player1GameState = new Player1GameState(Board, Board.Players[0], Deck, HandPlayer1, this);
        var player2GameState = new Player2GameState(Board, Board.Players[1], Deck, HandPlayer2, this);
        _stateMachine.RegisterState(GameStates.Player1, player1GameState);
        _stateMachine.RegisterState(GameStates.Player2, player2GameState);
        _stateMachine.MoveTo(GameStates.Player1);
        uIHandPlayer1.SetActive(true);
        uIHandPlayer2.SetActive(false);

        
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
            { "Rain", new RainCard(Board) }
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
    
    public void OnCardDragStart(string card)
    {
        _stateMachine.CurrentState.OnCardDragStart(card);
    }
    
    public void OnCardReleased(Tile hoverTile, string card)
    {
        _stateMachine.CurrentState.OnCardReleased(hoverTile, card);
    }
    
    public void OnCardTileFocused(Tile hoverTile, bool entered)
    {
        _stateMachine.CurrentState.OnCardTileFocused(hoverTile, entered);
    }
    
    public void OnPointerEnterTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
    {
        _stateMachine.CurrentState.OnPointerEnterTile(eventData, _model);
    }
    
    public void OnPointerExitTile(UnityEngine.EventSystems.PointerEventData eventData, Tile _model)
    {
        _stateMachine.CurrentState.OnPointerExitTile(eventData, _model);
    }
    
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

    public void UIChange()
    {
        if (!uIHandPlayer1.gameObject.active)
        {
            uIHandPlayer1.SetActive(true);
            uIHandPlayer2.SetActive(false);
        }
        else
        {
            uIHandPlayer1.SetActive(false);
            uIHandPlayer2.SetActive(true);
        }
    }
    
    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
    }
    
    IEnumerator OnPostStart()
    {
        yield return new WaitForEndOfFrame();

        Board.OnStart();
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

            Board.Players.Add(piece);
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
        }
    }
    #endregion

    #endregion
}
