using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("�귿 ���� ��ü")]
    public RouletteGame m_RouletteGame;

    /// <summary>
    /// �׸��� �Է¹��� InputField ������Ʈ���� ��Ÿ���ϴ�
    /// </summary> 
    [Header("�Է� �ʵ�")]
    public TMP_InputField[] m_InputFields;

    [Header("���� ��ư")]
    public Button m_ClearButton;

    [Header("�����ϱ� ��ư")]
    public Button m_StartButton;

    [Header("��� �ؽ�Ʈ")]
    public TMP_Text m_WarnigText;


    private void Awake()
    {
        // ��� �ؽ�Ʈ ��Ȱ��ȭ
        m_WarnigText.gameObject.SetActive(false);

        // ��ư �̺�Ʈ ����
        m_StartButton.onClick.AddListener(OnStartButtonClicked);

        m_ClearButton.onClick.AddListener(OnClearButtonClicked);

    }

    /// <summary>
    /// ���� ���� ���� ���θ� ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    private bool IsStartable()
    {
        // ��� InputField �� ���ڿ��� �ԷµǾ����� Ȯ���մϴ�.
        foreach (TMP_InputField inputField in m_InputFields)
        {
            // �� inputField �� ��������� ��Ÿ���ϴ�.
            bool isEmpty = string.IsNullOrEmpty(inputField.text);

            // ��� �ִ� inputField �� �߰��ߴٸ� ���� �Ұ���
            if (isEmpty) return false;
        }

        // ���� ����
        return true;
    }

    /// <summary>
    /// �Է� �ʵ��� �ؽ�Ʈ�� �迭�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns>���ڿ��� �迭�� ��� ��ȯ�մϴ�.</returns>
    private string[] GetInputFieldTextArray()
    {
        // �迭 �޸� �Ҵ�
        string[] itemStrings = new string[6];

        // �Է� �ʵ��� ���ڿ��� �迭�� ����ϴ�.
        for (int i = 0; i < itemStrings.Length; ++i)
        {
            // �迭 ��ҿ� ������ �Է� �ʵ� ������Ʈ�� ����ϴ�.
            TMP_InputField inputField = m_InputFields[i];

            // �Է� �ʵ� ���ڿ��� ����ϴ�.
            string inputFieldString = inputField.text;

            // �Է� �ʵ� ���ڿ��� �迭 ��ҿ� �����մϴ�.
            itemStrings[i] = inputFieldString;

        }

        // �迭�� ��ȯ�մϴ�.
        return itemStrings;
    }

    /// <summary>
    /// �����ϱ� ��ư�� Ŭ���Ǿ��� ��� ȣ��Ǵ� �޼����Դϴ�.
    /// Awake() �޼��忡�� ��ư �̺�Ʈ�� �����մϴ�.
    /// </summary>
    private void OnStartButtonClicked()
    {
        // ������ ������ ���¶��
        if (IsStartable())
        {
            // �Է� �ʵ忡 ��� ���ڿ����� ����ϴ�.
            string[] itemStrings = GetInputFieldTextArray();

            // �귿 ���� ����
            m_RouletteGame.OnStartGame(itemStrings);
        }
        // ������ �Ұ����� ���¶��
        else
        {
            // ��� ���ڿ� ǥ��
            m_WarnigText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ����� ��ư�� Ŭ���Ǿ��� ��� ȣ��Ǵ� �޼����Դϴ�.
    /// Awake() �޼��忡�� ��ư �̺�Ʈ�� �����մϴ�.
    /// </summary>
    private void OnClearButtonClicked()
    {
        foreach (TMP_InputField inputField in m_InputFields)
        {
            inputField.text = null;
        }
    }

    /// <summary>
    /// SettingPanel �� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void InitializeSettingPanel()
    {
        m_WarnigText.gameObject.SetActive(false);
    }
}
