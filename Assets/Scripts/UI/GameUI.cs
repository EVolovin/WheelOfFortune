using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using WheelOfFortune.Sound;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UI
{
    public class GameUI : MonoBehaviour
    {
        private class CoroutineContainer
        {
            // making the fields public in this case is acceptable
            // because the class is private nested
            public Coroutine updateScoreCoroutine;
            public Sequence winTextCounter;
            public Sequence totalWinTextCounter;
        }
        
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _winText;
        
        [SerializeField] GameObject _trailOne;
        [SerializeField] GameObject _trailTwo;
        [SerializeField] Transform _midPointOne;
        [SerializeField] Transform _midPointTwo;
        
        
        long _targetValue;
        CoroutineContainer _coroutineContainer;

        public ISoundManager SoundManager { get; set; }
        
        
        public void SetScore(long value)
        {
            _scoreText.text = StringUtil.GetFormattedNumber(value);
        }
        
        public void UpdateScore(long winValue, long totalWinValue)
        {
            if (_coroutineContainer != null)
                ResetCoroutine();
            
            _targetValue = totalWinValue;
            
            _coroutineContainer = new CoroutineContainer();
            _coroutineContainer.updateScoreCoroutine = StartCoroutine(UpdateScoreCoroutine(winValue, totalWinValue));
        }

        IEnumerator UpdateScoreCoroutine(long winValue, long totalWinValue)
        {
            var wait = new WaitForSeconds(1f);
            
            _coroutineContainer.winTextCounter = DOTweenUtil.DoTextValueCounting(_winText, 0, winValue, true, false);
            SoundManager.PlaySound(SoundManager.Sounds.Win);
            yield return wait;
            
            LaunchTrails();
            yield return wait;
            
            _coroutineContainer.totalWinTextCounter = DOTweenUtil.DoTextValueCounting(_scoreText, totalWinValue - winValue, totalWinValue, false, true);
            SoundManager.PlaySound(SoundManager.Sounds.CoinsLoop);
            yield return wait;
            
            _coroutineContainer = null;
            
            _scoreText.text = StringUtil.GetFormattedNumber(_targetValue);
            _winText.text = string.Empty;
        }
        
        void LaunchTrails()
        {
            _trailOne.transform.position = _winText.transform.position;
            _trailTwo.transform.position = _winText.transform.position;
            
            _trailOne.transform.localScale = Vector3.one * 0.5f;
            _trailTwo.transform.localScale = Vector3.one * 0.5f;
            
            _trailOne.SetActive(true);
            _trailTwo.SetActive(true);
            
            DOTweenUtil.MoveObject(_trailOne.transform, _scoreText.transform.position, _midPointOne.position, 1f)
                .OnComplete(() => _trailOne.SetActive(false));
            DOTweenUtil.MoveObject(_trailTwo.transform, _scoreText.transform.position, _midPointTwo.position, 1f)
                .OnComplete(() => _trailTwo.SetActive(false));
            
            SoundManager.PlaySound(SoundManager.Sounds.Trails);
        }
        
        void ResetCoroutine()
        {
            StopCoroutine(_coroutineContainer.updateScoreCoroutine);
            _coroutineContainer.winTextCounter.Kill();
            _coroutineContainer.totalWinTextCounter.Kill();
            
            _coroutineContainer = null;
            
            _scoreText.text = StringUtil.GetFormattedNumber(_targetValue);
            _winText.text = string.Empty;
        }
    }
}
