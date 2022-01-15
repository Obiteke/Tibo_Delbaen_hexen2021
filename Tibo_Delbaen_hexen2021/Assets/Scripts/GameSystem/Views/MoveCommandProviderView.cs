using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

namespace GameSystem.Views
{
    [RequireComponent(typeof(ObjectPool))]
    public class MoveCommandProviderView : MonoBehaviour
    {
        List<MoveCommandView> _moveCommandViews = new List<MoveCommandView>();
        private ObjectPool _pool;

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameInitialized;
            _pool = GetComponent<ObjectPool>();
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var moveManager = GameLoop.Instance.MoveManager;
            moveManager.MoveCommandProviderChanged += OnManagerProviderChanged;
        }

        private void OnManagerProviderChanged(object sender, MoveCommandProviderChangedEventArgs<HexenPiece> e)
        {
            foreach (var moveCommandView in _moveCommandViews)
            {
                moveCommandView.gameObject.SetActive(false);
            }

            _moveCommandViews.Clear();

            var moveCommandProvider = e.MoveCommandProvider;
            if (e.MoveCommandProvider != null)
            {
                var moveCommands = moveCommandProvider.Commands();
                foreach (var moveCommand in moveCommands)
                {
                    var go = _pool.GetPooledObject();
                    var view = go.GetComponent<MoveCommandView>();
                    view.Model = moveCommand;
                    _moveCommandViews.Add(view);
                }
            }
        }
    }
}
