using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public IEnumerator FadeToCoroutine(SpriteRenderer sr, float a, float time = 0.1f)
    {
        float velocity = 0f;
        Color color;

        while (Mathf.Abs(a - sr.color.a) > 0.05f)
        {
            color = sr.color;
            color.a = Mathf.SmoothDamp(color.a, a, ref velocity, time);
            sr.color = color;
            yield return null;
        }

        color = sr.color;
        color.a = a;
        sr.color = color;
    }
}
