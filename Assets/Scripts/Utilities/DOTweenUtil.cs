using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace WheelOfFortune.Utilities
{
    public static class DOTweenUtil
    {
        public static Sequence DoTextValueCounting(TextMeshProUGUI text, long startValue, long endValue, bool doScale, bool shortenTextValue)
        {
            var sequence = DOTween.Sequence();
            
            if (doScale)
            {
                text.transform.localScale = Vector3.zero;
                text.transform.DOScale(Vector3.one, 0.5f);
            }
            
            sequence.Append(DOTween.To(value => text.text = StringUtil.GetFormattedNumber(value, shortenTextValue), startValue, endValue, 1f));
            
            if (doScale)
            {
                sequence.AppendInterval(0.5f);
                sequence.Append(text.transform.DOScale(Vector3.zero, 0.5f));
            }
            
            return sequence;
        }
        
        public static Sequence MoveObject(Transform goTransform, Vector3 targetPoint, Vector3 midPoint, float duration)
        {
            var path = new List<Vector3>();
            path.AddRange(CreateParabolicCurve(goTransform.position, midPoint));
            path.AddRange(CreateParabolicCurve(targetPoint, midPoint, true));
            
            var doMove     = goTransform.DOPath(path.ToArray(), duration);
            // var doScale = goTransform.DOScale(Vector3.one * 0.2f, duration);
            
            var sequence = DOTween.Sequence();
            sequence.Join(doMove);
            // sequence.Join(doScale);
            
            return sequence;
        }
        
        
        static Vector3[] CreateParabolicCurve(Vector3 p0, Vector3 p1, bool reverse = false, int steps = 20)
        {
            // algorithm created and written by myself
            // creates a parabolic curve between two points with a given precision
            // the more steps, the smoother the curve is but too many steps may cause an unwanted overhead when used by DOTween
            
            float diffX = p1.x - p0.x;
            float diffY = p1.y - p0.y;
            
            if (Mathf.Approximately(diffX, 0f) || Mathf.Approximately(diffY, 0f))
                return new[] { p0, p1 };
            
            var path = new Vector3[steps];
            
            float incrementX = diffX / steps;
            float a = diffY / (diffX * diffX);
            float z = p0.z;
            
            path[0] = reverse ? p1 : p0;
            path[path.Length - 1] = reverse ? p0 : p1;
            
            steps--;
            
            for (int i = 1; i < steps; i++)
            {
                float x = p0.x + incrementX * i;
                float y = a * (x - p0.x) * (x - p0.x) + p0.y;
                
                var point = new Vector3(x, y, z);
                
                if (reverse)
                    path[path.Length - i - 1] = point;
                else
                    path[i] = point;
            }
            
            return path;
        }
    }
}
