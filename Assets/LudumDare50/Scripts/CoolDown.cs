using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CoolDown : MonoBehaviour
{
    [SerializeField] Image CoolDownImage;
    [SerializeField] TMP_Text number;

    public UnityEvent EndCoolDown;

    public void Cooldown(float WaitTime)
    {
        StartCoroutine(WaitDown(WaitTime));
    }
    private IEnumerator WaitDown(float WaitTime)
    {
        CoolDownImage.gameObject.SetActive(true);
        CoolDownImage.fillAmount = 1;
        number.text = WaitTime.ToString();
        float WaitAmount = WaitTime;
        while (0 < WaitTime)
        {
            yield return new WaitForSeconds(0.1f);
            WaitTime -= 0.1f;
            CoolDownImage.fillAmount = WaitTime / WaitAmount;
            number.text = WaitTime.ToString("F1");
        }
        CoolDownImage.gameObject.SetActive(false);
        EndCoolDown.Invoke();
    }
}
