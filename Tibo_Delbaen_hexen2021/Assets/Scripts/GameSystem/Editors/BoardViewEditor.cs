using GameSystem.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BoardSystem.Editor
{
    [CustomEditor(typeof(BoardView))]
    public class BoardViewEditor : UnityEditor.Editor
    {
        string _radius = "3";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _radius = GUILayout.TextField(_radius, 3);
            // Hex board
            if (GUILayout.Button("Create hexenboard"))
            {
                CreateBoard();
            }
        }

        private void CreateBoard()
        {
            var boardView = target as BoardView;

            var tileViewFactorySp = serializedObject.FindProperty("_tileViewFactory");
            var tileViewFactory = tileViewFactorySp.objectReferenceValue as TileViewFactory;

            var game = GameLoop.Instance;

            game.CreateBoard(int.Parse(_radius));
            var board = game.Board;

            foreach (var tile in board.Tiles)
            {
                tileViewFactory.CreateTileView(board, tile, boardView.transform);
            }
        }
    }
}
