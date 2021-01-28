using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : MonoBehaviour
{
    public float lerpVar;
    public float xPos;
    public float yPos;
    public float xScale;
    public float yScale;
    private RectTransform _rectTransform;
    private Vector2 finalScale;
    private Vector2 finalPos;
    private Vector2 initScale;
    private Vector2 initPos;
    private float lerpCounter;

    private bool bimbadinha;
    private bool rebimba;
    public float alturaBimbadinha;

    public float timeToSpawnButtons;
    private Transform buttonContainer;

    private bool finish;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        finalPos = _rectTransform.anchoredPosition;
        finalScale = _rectTransform.rect.size;
        _rectTransform.anchoredPosition += new Vector2(xPos, yPos);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, xScale);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, yScale);
        initPos = _rectTransform.anchoredPosition;
        initScale = _rectTransform.rect.size;
        buttonContainer = transform.parent.Find("ButtonContainer");
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.anchoredPosition = Vector2.Lerp(initPos, finalPos, lerpCounter);
        Vector2 scaleTemp = Vector2.Lerp(initScale, finalScale, lerpCounter);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scaleTemp.x);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scaleTemp.y);
        
        
        if(lerpCounter < 1 && !bimbadinha)
        {
            lerpCounter += lerpVar;
            if (rebimba)
            {
                lerpVar += lerpVar / 3;
            }
        }
        else
        {
            bimbadinha = true;
        }


        if (bimbadinha && !rebimba)
        {
            if (lerpCounter > alturaBimbadinha / 100)
            {
                lerpCounter -= lerpVar;
                if(lerpVar > 0.005f)
                {
                    lerpVar /= 2f;
                }
            }
            else
            {
                bimbadinha = false;
                rebimba = true;
            }
        }
        
        if(bimbadinha && rebimba)
        {
            lerpCounter = 1;
            if (name == "Title" && !finish)
            {
                StartCoroutine(SpawnButtons(timeToSpawnButtons));
                finish = true;
            }
        }
    }

    private IEnumerator SpawnButtons(float time)
    {
        buttonContainer.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(time/2);
        buttonContainer.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(time/3);
        buttonContainer.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(time/4);
        buttonContainer.GetChild(3).gameObject.SetActive(true);
    }
}
