using DAE.GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    //[SerializeField]
    //private GameLoop _loop;

    private Position _model;

    public Position Model
    {
        set
        {
            if (_model != null)
            {
                _model.Activated -= PositionActivated;
                _model.Deactivated -= PositionDeactivated;
            }

            _model = value;

            if (_model != null)
            {
                _model.Activated += PositionActivated;
                _model.Deactivated += PositionDeactivated;
            }

        }
        get
        {
            return _model;
        }
    }

    private void PositionDeactivated(object sender, EventArgs e)
        => OnDeactivate.Invoke();

    private void PositionActivated(object sender, EventArgs e)
        => OnActivate.Invoke();

    public void OnPointerClick(PointerEventData eventData)
    {
        //_loop.DebugPosition(this);
    }
}
