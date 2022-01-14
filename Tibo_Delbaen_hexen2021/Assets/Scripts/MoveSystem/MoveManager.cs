using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveSystem
{
    public class MoveCommandProviderChangedEventArgs<TPiece> : EventArgs where TPiece : class, IPiece
    {
        public IMoveCommandProvider<TPiece> MoveCommandProvider { get; }

        public MoveCommandProviderChangedEventArgs(IMoveCommandProvider<TPiece> moveCommandProvider)
        {
            MoveCommandProvider = moveCommandProvider;
        }
    }

    public class MoveManager<TPiece> where TPiece : class, IPiece
    {
        public event EventHandler<MoveCommandProviderChangedEventArgs<TPiece>> MoveCommandProviderChanged;

        Dictionary<string, IMoveCommandProvider<TPiece>> _providers = new Dictionary<string, IMoveCommandProvider<TPiece>>();
        Dictionary<TPiece, string> _pieceMovements = new Dictionary<TPiece, string>();

        IMoveCommandProvider<TPiece> _activeProvider;
        List<Tile> _validTiles = new List<Tile>();

        Board<TPiece> _board;

        public MoveManager(Board<TPiece> board)
        {
            _board = board;
        }

        public string MovementOf(TPiece piece)
        {
            return _pieceMovements[piece];
        }

        public void Register(string name, IMoveCommandProvider<TPiece> provider)
        {
            if (_providers.ContainsKey(name))
                return;

            _providers.Add(name, provider);
        }

        public void Register(TPiece piece, string name)
        {
            if (_pieceMovements.ContainsKey(piece))
                return;

            _pieceMovements.Add(piece, name);
        }

        public IMoveCommandProvider<TPiece> Provider(TPiece piece)
        {
            if (piece == null)
                return null;

            if (_pieceMovements.TryGetValue(piece, out var name))
            {
                if (_providers.TryGetValue(name, out var moveCommandProvider))
                    return moveCommandProvider;
            }
            return null;
        }

        public void ActivateFor(TPiece piece)
        {
            _activeProvider = Provider(piece);
            if (_activeProvider != null)
            {
                _validTiles = _activeProvider.Commands()
                    .Where((command) => command.CanExecute(_board, piece))
                    .SelectMany((command) => command.Tiles(_board, piece)).ToList();
            }
            else
                _validTiles.Clear();

            OnMoveCommandProviderChanged(new MoveCommandProviderChangedEventArgs<TPiece>(_activeProvider));
        }

        public void DeActivate()
        {
            _validTiles.Clear();
            _activeProvider = null;

            OnMoveCommandProviderChanged(new MoveCommandProviderChangedEventArgs<TPiece>(null));
        }

        public void Execute(TPiece piece, Tile tile)
        {
            var foundCommand = _activeProvider.Commands().Find((command) => command.Tiles(_board, piece).Contains(tile));

            if (_validTiles.Contains(tile))
            {
                if (foundCommand != null)
                {
                    foundCommand.Execute(_board, piece, tile);

                    _activeProvider = null;
                }
            }
        }

        public List<Tile> Tiles()
        {
            return _validTiles;
        }

        protected virtual void OnMoveCommandProviderChanged(MoveCommandProviderChangedEventArgs<TPiece> arg)
        {
            EventHandler<MoveCommandProviderChangedEventArgs<TPiece>> handler = MoveCommandProviderChanged;
            handler?.Invoke(this, arg);
        }
    }
}
