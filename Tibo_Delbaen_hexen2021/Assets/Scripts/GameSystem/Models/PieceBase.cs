using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Models
{
    public class PieceMovedEventArgs : EventArgs
    {
        public Tile From { get; }
        public Tile To { get; }

        public PieceMovedEventArgs(Tile from, Tile to)
        {
            From = from;
            To = to;
        }
    }
    public abstract class PieceBase : IPiece
    {
        public event EventHandler<PieceMovedEventArgs> PieceMoved;
        public event EventHandler PieceTaken;

        void IPiece.Moved(Tile fromTile, Tile toTile)
        {
            OnPieceMoved(new PieceMovedEventArgs(fromTile, toTile));
        }

        void IPiece.Taken()
        {
            OnPieceTaken(EventArgs.Empty);
        }
        protected virtual void OnPieceMoved(PieceMovedEventArgs args)
        {
            EventHandler<PieceMovedEventArgs> handler = PieceMoved;

            handler?.Invoke(this, args);
        }

        protected virtual void OnPieceTaken(EventArgs arg)
        {
            EventHandler handler = PieceTaken;

            handler?.Invoke(this, arg);
        }
    }
}
