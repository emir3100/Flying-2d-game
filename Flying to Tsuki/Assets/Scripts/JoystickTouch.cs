using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouch : EventTrigger
{
    public static int? FirstTriggerId;

    public override void OnPointerEnter(PointerEventData data)
    {
        if (FirstTriggerId == null && data.pointerId != -1 && EventSystem.current.IsPointerOverGameObject(data.pointerId))
            FirstTriggerId = data.pointerId;
    }

    public override void OnPointerUp(PointerEventData data)
    {
        if (FirstTriggerId != null && data.pointerId == FirstTriggerId.Value)
            FirstTriggerId = null;
    }
}
