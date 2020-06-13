using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartJoy
{
    public class BaseJoystick : MonoBehaviour, IJoystick, IDragHandler, IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IEndDragHandler
    {
        public event Action<Vector2> OnStartDrag = delegate { };
        public event Action<Vector2> OnEndDrag = delegate { };
        public event Action<Vector2> OnInputChanged = delegate { };

        public Vector2 Input { get; private set; }

        [SerializeField] private RectTransform stick;
        [SerializeField] private RectTransform @base;

        [SerializeField] [Space(15)] private CanvasGroup canvasGroup;

        protected bool IsReleased;
        private const float MaxRadius = 1f;

        protected Vector2 StartPosition;
        protected Vector2 Limit;

        private void Start()
        {
            Limit = @base.rect.size - stick.rect.size * 3.2f * MaxRadius;

            StartPosition = @base.transform.position;
            
            Release();
        }

        private void SetInput(Vector2 value)
        {
            Input = value;

            OnInputChanged(Input);
        }

        protected virtual Vector2 GetStartPosition(PointerEventData eventData)
        {
            return eventData.position;
        }

        protected Vector2 GetInput(Vector2 position)
        {
            var direction = (position - StartPosition) / Limit;

            var input = direction.magnitude > 1.0f ? direction.normalized : direction;

            return input;
        }
        
        protected virtual void Release()
        {
            IsReleased = true;

            SetInput(Vector2.zero);

            stick.transform.localPosition = Vector3.zero;
        }

        private void RefreshInput(PointerEventData eventData)
        {
            var input = GetInput(eventData.position);

            SetInput(input);

            stick.transform.position = input * Limit + StartPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RefreshInput(eventData);

            OnStartDrag(Input);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RefreshInput(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnEndDrag(Input);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            IsReleased = false;

            // StartPosition = GetStartPosition(eventData);

            // @base.anchoredPosition = StartPosition;

            RefreshInput(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Release();
        }
    }
}