using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Hexen.CardSystem
{
    public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private GameObject _cardToDrag;

        public RectTransform DragArea
        {
            get;
            internal set;
        }
        private void DragCard()
        {
            _cardToDrag = new GameObject("DraggedCard");

            // Keep created card 
            _cardToDrag.transform.SetParent(DragArea, false);

            // Add image component to dragged card gameobject
            Image component = _cardToDrag.AddComponent<Image>();

            // Turn off raycast on card to detect underlying tile.
            component.raycastTarget = false;

            // Set sprite of dragged card
            component.sprite = GetComponent<Image>().sprite;

            // Pixel perfect
            component.SetNativeSize();
        }
        private void MoveCard(PointerEventData eventData)
        {
            RectTransform _cardTransform = _cardToDrag.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(DragArea, eventData.position + new Vector2(0f, -125f), eventData.pressEventCamera, out Vector3 vector3))
            {
                _cardTransform.position = vector3;
                _cardTransform.rotation = DragArea.rotation;
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            DragCard();
            MoveCard(eventData);
        }


        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            this.transform.position = eventData.position;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(_cardToDrag);
        }
    }
}
