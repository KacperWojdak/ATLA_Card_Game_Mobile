using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILeanTween : MonoBehaviour
{
    [SerializeField] GameObject avatarLogo;
    [SerializeField] GameObject cardGameText;
    [SerializeField] GameObject clickToBeginText;

    private void Start()
    {
        LeanTween.alpha(avatarLogo.GetComponent<RectTransform>(), 1f, 1f).setDelay(.5f);
    }


}
