using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteGame : MonoBehaviour
{
    [Header("�귿 ������Ʈ")]
    public RouletteObject m_RouletteObject;

    [Header("���� �г�")]
    public SettingPanel m_SettingPanel;

    /// <summary>
    /// ������ ���۵Ǿ��� �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="itemStrings">�귿�� ������ų ���ڿ����� �����մϴ�.</param>
    public void OnStartGame(string[] itemStrings)
    {
        // �귿 ������Ʈ �ʱ�ȭ
        m_RouletteObject.InitializeRoulette(itemStrings);

        // �귿 ���� �г��� ��Ȱ��ȭ ��ŵ�ϴ�.
        m_SettingPanel.gameObject.SetActive(false);

    }

}
