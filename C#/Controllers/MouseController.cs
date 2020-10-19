using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool IsClickEnabled { set; get; }
    public bool IsMoveEnabled { set; get; }

    private Vector3 mousePosition;
    private Vector3 previousPosition;

    public delegate void OnMousePositionChanged(Vector3 position);
    public event OnMousePositionChanged OnMousePositionChangedEvent = delegate { };

    public delegate void OnMousePositionClicked(Vector3 position);
    public event OnMousePositionClicked OnMousePositionClickedEvent = delegate { };

    private void Awake()
    {
        //default isEnable values
        IsClickEnabled = true;
        IsMoveEnabled = true;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateMouseHandlers();
    }

    private void UpdateMouseHandlers()
    {
        if (IsClickEnabled)
        {
            if(mousePosition != previousPosition)
            {
                OnMousePositionChangedEvent.Invoke(mousePosition);
                previousPosition = mousePosition;
            }
        }

        if (IsMoveEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMousePositionClickedEvent.Invoke(mousePosition);
            }
        }
    }
}
