using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    Image meterOut;
    Image meterPreview;
    public float meterOutPercent;
    private void Start()
    {
        meterPreview = transform.GetChild(0).GetComponent<Image>();
        meterOut = meterPreview.transform.GetChild(0).GetComponent<Image>();
        meterPreview.fillAmount = 0f;
        meterOut.fillAmount = 0f;
        meterOutPercent = 0f;
    }

    public float UpdateMeterPreview(float percent)
    {
        meterPreview.fillAmount = meterOutPercent + percent;
        if (meterPreview.fillAmount > 1)
        {
            meterPreview.fillAmount = 1;
        }
        return meterPreview.fillAmount - meterOutPercent;
    }

    public void UpdateMeterOut(float percent)
    {
        meterOutPercent += percent;
        meterOut.fillAmount = meterOutPercent;
    }

    public void GainMeter(float percent)
    {
        meterOutPercent -= percent;
    }
}
