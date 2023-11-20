using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteObject : MonoBehaviour
{
    /// <summary>
    /// ������ �ؽ�Ʈ���� ��Ÿ���ϴ�.
    /// </summary>
    [Header("������ ���ڿ�")]
    public TMP_Text[] m_ItemTexts;

    /// <summary>
    /// �귿 ������Ʈ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="itemStrings">������ų ���ڿ����� �����մϴ�.</param>
    public void InitializeRoulette(string[] itemStrings)
    {
        // ������ ���ڿ��� ��� �����մϴ�.
        for(int i = 0; i < itemStrings.Length; ++i) 
        {
            // �ؽ�Ʈ ������Ʈ�� �迭���� ����ϴ�.
            TMP_Text textComponent = m_ItemTexts[i];

            // ������ų ���ڿ��� �迭���� ����ϴ�.
            string itemString = itemStrings[i];

            // �ؽ�Ʈ ������Ʈ�� ���ڿ��� �����մϴ�.
            textComponent.text = itemString;
        }
    }

}
