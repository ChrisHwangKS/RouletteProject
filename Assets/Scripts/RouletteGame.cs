using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteGame : MonoBehaviour
{
    [Header("룰렛 오브젝트")]
    public RouletteObject m_RouletteObject;

    [Header("세팅 패널")]
    public SettingPanel m_SettingPanel;

    /// <summary>
    /// 게임이 시작되었을 때 호출됩니다.
    /// </summary>
    /// <param name="itemStrings">룰렛에 설정시킬 문자열들을 전달합니다.</param>
    public void OnStartGame(string[] itemStrings)
    {
        // 룰렛 오브젝트 초기화
        m_RouletteObject.InitializeRoulette(itemStrings);

        // 룰렛 세팅 패널을 비활성화 시킵니다.
        m_SettingPanel.gameObject.SetActive(false);

    }

}
