using System.Collections;
using System;
using System.Collections.Generic;
using Hexen.Commons;
using UnityEngine;

namespace Hexen.BoardSystem
{
    public class PiecePlacedEventArgs<TPiece, TPosition> : EventArgs
    {
        public TPiece Piece { get; }

        public TPosition AtPosition { get; }

        public PiecePlacedEventArgs(TPiece piece, TPosition atPosition)
        {
            Piece = piece;
            AtPosition = atPosition;
        }
    }

    public class PieceMovedEventArgs<TPiece, TPosition>
    {
        public TPiece Piece { get; }

        public TPosition FromPosition { get; }

        public TPosition ToPosition { get; }

        public PieceMovedEventArgs(TPiece piece, TPosition fromPosition, TPosition toPosition)
        {
            Piece = piece;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }

    public class PieceTakenEventArgs<TPiece, TPosition>
    {
        public TPiece Piece { get; }
        public TPosition FromPosition { get; }

        public PieceTakenEventArgs(TPiece piece, TPosition fromPosition)
        {
            Piece = piece;
            FromPosition = fromPosition;
        }
    }

    public class Board<TPiece, TPosition>
    {
        public event EventHandler<PiecePlacedEventArgs<TPiece, TPosition>> PiecePlaced;
        public event EventHandler<PieceMovedEventArgs<TPiece, TPosition>> PieceMoved;
        public event EventHandler<PieceTakenEventArgs<TPiece, TPosition>> PieceTaken;

        private BidirectionalDictionary<TPiece, TPosition> _pieces
            = new BidirectionalDictionary<TPiece, TPosition>();

        public bool TryGetPiece(TPosition position, out TPiece piece)
            => _pieces.TryGetKey(position, out piece);

        public bool TryGetPosition(TPiece piece, out TPosition position)
            => _pieces.TryGetValue(piece, out position);


        public void Place(TPiece piece, TPosition position)
        {
            if (_pieces.ContainsKey(piece))
                return;

            if (_pieces.ContainsValue(position))
                return;

            _pieces.Add(piece, position);
            OnPiecePlaced(new PiecePlacedEventArgs<TPiece, TPosition>(piece, position));
        }

        public void Move(TPiece piece, TPosition toPosition)
        {
            if (!TryGetPosition(piece, out var fromPosition))
                return;

            if (TryGetPiece(toPosition, out _))
                return;

            if (!_pieces.Remove(piece))
                return;

            _pieces.Add(piece, toPosition);
            OnPieceMoved(new PieceMovedEventArgs<TPiece, TPosition>(piece, fromPosition, toPosition));
        }

        public void Take(TPiece piece)
        {
            if (!TryGetPosition(piece, out var fromPosition))
                return;

            if (_pieces.Remove(piece))
                OnPieceTaken(new PieceTakenEventArgs<TPiece, TPosition>(piece, fromPosition));

        }

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece, TPosition> eventArgs)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece, TPosition> eventArgs)
        {
            var handler = PieceMoved;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece, TPosition> eventArgs)
        {
            var handler = PieceTaken;
            handler?.Invoke(this, eventArgs);
        }
    }
}
