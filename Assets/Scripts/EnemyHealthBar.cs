using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public int maxValue;
    public int value;

    public RectTransform bar;
    public RectTransform redBar;

    public float fullWidth;
    private float targetWidth => value * fullWidth / maxValue;

    private Coroutine AdjustBarCoroutine;

    private void Awake()
    {
        SetBar();
    }

    private IEnumerator AdjustBar(int amount)
    {
        var redChange = amount >= 0 ? bar : redBar;
        var whiteChange = amount >= 0 ? redBar : bar;

        redChange.sizeDelta = new Vector2(targetWidth, redChange.rect.height);

        while(Mathf.Abs(redChange.rect.width - whiteChange.rect.width) > 1f)
        {
            whiteChange.sizeDelta = new Vector2(Mathf.Lerp(whiteChange.rect.width, targetWidth, Time.deltaTime * 4f), whiteChange.rect.height);
            yield return null;
        }
        whiteChange.sizeDelta = new Vector2(targetWidth, whiteChange.rect.height);
    }

    public void Change(int amount)
    {
        value = Mathf.Clamp(value - amount, 0, maxValue);
        if(AdjustBarCoroutine != null)
        {
            StopCoroutine(AdjustBarCoroutine);
        }
        AdjustBarCoroutine = StartCoroutine(AdjustBar(amount));
    }

    public void SetBar()
    {
        value = maxValue;
        fullWidth = 72;
        //fullWidth = redBar.rect.width;
    }

}
