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

    [Header("경고 텍스트")]
    public TMP_Text m_WarnigText;


    private void Awake()
    {
        // 경고 텍스트 비활성화
        m_WarnigText.gameObject.SetActive(false);

        // 버튼 이벤트 설정
        m_StartButton.onClick.AddListener(OnStartButtonClicked);

        m_ClearButton.onClick.AddListener(OnClearButtonClicked);

    }

    /// <summary>
    /// 게임 시작 가능 여부를 반환합니다.
    /// </summary>
    /// <returns></returns>
    private bool IsStartable()
    {
        // 모든 InputField 에 문자열이 입력되었는지 확인합니다.
        foreach (TMP_InputField inputField in m_InputFields)
        {
            // 이 inputField 가 비어있음을 나타냅니다.
            bool isEmpty = string.IsNullOrEmpty(inputField.text);

            // 비어 있는 inputField 를 발견했다면 시작 불가능
            if (isEmpty) return false;
        }

        // 시작 가능
        return true;
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
        // 시작이 가능한 상태라면
        if (IsStartable())
        {
            // 입력 필드에 담긴 문자열들을 얻습니다.
            string[] itemStrings = GetInputFieldTextArray();

            // 룰렛 게임 시작
            m_RouletteGame.OnStartGame(itemStrings);
        }
        // 시작이 불가능한 상태라면
        else
        {
            // 경고 문자열 표시
            m_WarnigText.gameObject.SetActive(true);
        }
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

    /// <summary>
    /// SettingPanel 을 초기화합니다.
    /// </summary>
    public void InitializeSettingPanel()
    {
        m_WarnigText.gameObject.SetActive(false);
    }
}
