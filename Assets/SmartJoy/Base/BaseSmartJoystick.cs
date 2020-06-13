using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartJoy
{
    public class BaseSmartJoystick : BaseJoystick
    {
        private const float SmartModeDuration = 1f;
        private const float SmartModeZoneRadius = 1.6f;

        private float _releaseDuration;

        private Vector2 _lastInput;
        private Vector2 _lastBasePosition;
        private Vector2 _lastTouchPosition;

        protected override void Release()
        {
            base.Release();

            _releaseDuration = 0f;
        }

        protected override Vector2 GetStartPosition(PointerEventData eventData)
        {
            if (_releaseDuration > SmartModeDuration) return eventData.position;

            var distanceFromLastPos = _lastBasePosition - eventData.position;

            var distanceMagnitude = distanceFromLastPos.magnitude;
            var limit = Limit.magnitude;
            
            if (distanceMagnitude <= limit) return _lastBasePosition;

            if (distanceMagnitude > limit * SmartModeZoneRadius) return eventData.position;

            var maxPointInThisDirection = GetInput(eventData.position) * Limit + StartPosition;

            return _lastBasePosition + (eventData.position - maxPointInThisDirection);
        }

        private void Update()
        {
            if (!IsReleased) return;

            _releaseDuration += Time.unscaledDeltaTime;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _lastBasePosition = StartPosition;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _lastInput = Input;
            _lastTouchPosition = eventData.position;
        }
    }
}