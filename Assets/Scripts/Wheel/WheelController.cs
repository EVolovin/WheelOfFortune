using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Game
{
    public class WheelController : MonoBehaviour, IWheelController
    {
        private struct WheelStop
        {
            public long Value;
            public float Angle;
            public TextMeshProUGUI Text;
        }
        
        const float FULL_CIRCLE = 360f;
        
        [SerializeField] TextMeshProUGUI[] _valueTexts;
        [SerializeField] Button _spinButton;
        [SerializeField] Transform _wheelRoot;
        [SerializeField] [Range(0f, 10f)] float _spinDuration;
        [SerializeField] float _initialRotationAngleOffset;
        
        WheelStop[] _stops;
        bool _isSpinning;
        
        public event Action SpinStart;
        public event Action<long> SpinComplete;
        
        
        public bool IsSpinning
        {
            get => _isSpinning;
            private set
            {
                if (_isSpinning == value)
                    return;
                
                _isSpinning = value;
                _spinButton.interactable = !value;
            }
        }
        
        
        void Awake()
        {
            name = name.RemoveCloneLabel();
        }
        
        public void Initialize(WheelConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("Config is null");
            
            _stops = new WheelStop[_valueTexts.Length];
            
            // Could be hard-coded inside the prefab but this way it's more flexible e.g. if number of segments varies
            float step = FULL_CIRCLE / _valueTexts.Length;
            
            for (int i = 0; i < _valueTexts.Length; i++)
            {
                float angle = step * i + _initialRotationAngleOffset;
                _valueTexts[i].transform.parent.RotateAround(transform.position, Vector3.forward, -angle);
                _stops[i] = new WheelStop { Angle = angle, Text = _valueTexts[i] };
            }
            
            InitializeValues(config.RangeMin, config.RangeMax);
            
            _spinButton.onClick.AddListener(SpinWheel);
        }

        void InitializeValues(long rangeMin, long rangeMax)
        {
            if (rangeMin >= rangeMax)
                throw new ArgumentException("RangeMax must be greater than RangeMin");
            if (rangeMin % 100 != 0)
                throw new ArgumentException("RangeMin must be divisible by 100");
            if (rangeMax % 100 != 0)
                throw new ArgumentException("RangeMax must be divisible by 100");
            
            long min = rangeMin / 100;
            long max = rangeMax / 100;
            
            var distinctValues = new HashSet<long>();
            var values = new long[_stops.Length];
            
            int index = 0;
            while (distinctValues.Count < _stops.Length)
            {
                long value = (long) UnityEngine.Random.Range(min, max) * 100;
                
                if (!distinctValues.Add(value))
                    continue;
                
                values[index] = value;
                index++;
            }
            
            for (int i = 0; i < _stops.Length; i++)
            {
                _stops[i].Value = values[i];
                _stops[i].Text.text = StringUtil.GetFormattedNumber(values[i], false);
            }
        }
        
        void SpinWheel()
        {
            if (_isSpinning)
                return;
            
            int targetStopIndex = UnityEngine.Random.Range(0, _stops.Length);
            var angle = new Vector3(0f, 0f, _stops[targetStopIndex].Angle + FULL_CIRCLE * 5);
            
            IsSpinning = true;
            SpinStart?.Invoke();
            
            var tween = _wheelRoot.DORotate(angle, _spinDuration, RotateMode.FastBeyond360);
            tween.onComplete += () =>
            {
                IsSpinning = false;
                SpinComplete?.Invoke(_stops[targetStopIndex].Value);
            };
        }
    }
}
