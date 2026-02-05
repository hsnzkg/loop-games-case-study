using System;
using Project.Thirdparty.Joystick_Pack.Scripts.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Thirdparty.Joystick_Pack.Scripts.Joysticks
{
    public class DynamicJoystick : Joystick
    {
        public float GetMoveThreshold()
        {
            return moveThreshold;
        }

        public void SetMoveThreshold(float value)
        {
            moveThreshold = Mathf.Abs(value);
        }

        public event Action OnInput; 

        [SerializeField] private float moveThreshold = 1;

        protected override void Start()
        {
            SetMoveThreshold(moveThreshold);
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            base.OnPointerDown(eventData);
            OnInput?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            base.OnPointerUp(eventData);
            OnInput?.Invoke();
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > moveThreshold)
            {
                Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }
            base.HandleInput(magnitude, normalised, radius, cam);
            OnInput?.Invoke();
        }
    }
}