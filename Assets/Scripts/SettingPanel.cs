using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("룰렛 게임 객체")]
    public RouletteGame m_RouletteGame;

    /// <summary>
    /// 항목을 입력받을 InputField 컴포넌트들을 나타냅니다
    /// </summary> 
    [Header("입력 필드")]
    public TMP_InputField[] m_InputFields;

    [Header("비우기 버튼")]
    public Button m_ClearButton;

    [Header("시작하기 버튼")]
    public Button m_StartButton;

    private void Awake()
    {
        // 버튼 이벤트 설정
        m_StartButton.onClick.AddListener(OnStartButtonClicked);

        m_ClearButton.onClick.AddListener(OnClearButtonClicked);

    }

    /// <summary>
    /// 입력 필드의 텍스트를 배열로 반환합니다.
    /// </summary>
    /// <returns>문자열을 배열에 담아 반환합니다.</returns>
    private string[] GetInputFieldTextArray()
    {
        // 배열 메모리 할당
        string[] itemStrings = new string[6];

        // 입력 필드의 문자열을 배열에 담습니다.
        for (int i = 0; i < itemStrings.Length; ++i)
        {
            // 배열 요소에 설정된 입력 필드 컴포넌트를 얻습니다.
            TMP_InputField inputField = m_InputFields[i];

            // 입력 필드 문자열을 얻습니다.
            string inputFieldString = inputField.text;

            // 입력 필드 문자열을 배열 요소에 저장합니다.
            itemStrings[i] = inputFieldString;

        }

        // 배열을 반환합니다.
        return itemStrings;
    }

    /// <summary>
    /// 시작하기 버튼이 클릭되었을 경우 호출되는 메서드입니다.
    /// Awake() 메서드에서 버튼 이벤트로 설정합니다.
    /// </summary>
    private void OnStartButtonClicked()
    {
        // 입력 필드에 담긴 문자열들을 얻습니다.
        string[] itemStrings = GetInputFieldTextArray();

        // 룰렛 게임 시작
        m_RouletteGame.OnStartGame(itemStrings);

    }

    /// <summary>
    /// 지우기 버튼이 클릭되었을 경우 호출되는 메서드입니다.
    /// Awake() 메서드에서 버튼 이벤트로 설정합니다.
    /// </summary>
    private void OnClearButtonClicked()
    {
        foreach (TMP_InputField inputField in m_InputFields)
        {
            inputField.text = null;
        }
    }
}
