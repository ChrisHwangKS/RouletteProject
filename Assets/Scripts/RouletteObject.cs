using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteObject : MonoBehaviour
{
    /// <summary>
    /// 아이템 텍스트들을 나타냅니다.
    /// </summary>
    [Header("아이템 문자열")]
    public TMP_Text[] m_ItemTexts;

    /// <summary>
    /// 룰렛 오브젝트를 초기화합니다.
    /// </summary>
    /// <param name="itemStrings">설정시킬 문자열들을 전달합니다.</param>
    public void InitializeRoulette(string[] itemStrings)
    {
        // 아이템 문자열을 모두 설정합니다.
        for(int i = 0; i < itemStrings.Length; ++i) 
        {
            // 텍스트 컴포넌트를 배열에서 얻습니다.
            TMP_Text textComponent = m_ItemTexts[i];

            // 설정시킬 문자열을 배열에서 얻습니다.
            string itemString = itemStrings[i];

            // 텍스트 컴포넌트의 문자열을 설정합니다.
            textComponent.text = itemString;
        }
    }

}
