using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteObject : MonoBehaviour
{
    [Header("�귿 ���� ��ü")]
    public RouletteGame m_RouletteGame;

    /// <summary>
    /// ������ �ؽ�Ʈ���� ��Ÿ���ϴ�.
    /// </summary>
    [Header("������ ���ڿ�")]
    public TMP_Text[] m_ItemTexts;

    /// <summary>
    /// ����� ǥ���� �ؽ�Ʈ ������Ʈ�Դϴ�.
    /// </summary>
    [Header("��� �ؽ�Ʈ")]
    public TMP_Text m_ResultText;

    [Header("�귿 ������Ʈ")]
    public GameObject m_RouletteObject;

    [Header("�ٽ��ϱ� ��ư")]
    public Button m_RestartButton;

    /// <summary>
    /// z �� ȸ�� �ӵ��Դϴ�.
    /// 0 �� ��� ȸ������ �ʽ��ϴ�.
    /// </summary>
    private float _ZRotationVelocity;

    /// <summary>
    /// �������Դϴ�.
    /// �ش� ��ġ��ŭ �� �����Ӹ��� ȸ�� �ӵ��� ���ҽ�ŵ�ϴ�.
    /// </summary>
    private float _BrakingForce = 1500.0f;

    /// <summary>
    /// �귿 ������Ʈ ȸ���� ����Ű�� ���� ����
    /// </summary>
    private bool _IsRotatingAllowed;

    private void Awake()
    {
        // ��ư �̺�Ʈ ���ε�
        m_RestartButton.onClick.AddListener(OnRestartButtonClicked);

        // ��� ������ �ؽ�Ʈ�� ��Ȱ��ȭ
        SetActiveAllItemText(false);

        // �ٽ��ϱ� ��ư ��Ȱ��ȭ
        m_RestartButton.gameObject.SetActive(false);

        // ��� ���ڿ��� ��Ȱ��ȭ
        m_ResultText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // ����� ����մϴ�.
        ShowResult();

        // �귿�� ȸ����ŵ�ϴ�.
        RotationRoulette();

    }

    /// <summary>
    /// �귿�� ȸ����ŵ�ϴ�.
    /// </summary>
    private void RotationRoulette()
    {
        // ȸ���� ������ �ʾ��� ��� �޼��� ȣ���� �����մϴ�.
        if (!_IsRotatingAllowed) return;

        // �귿�� ���� ȸ������ ��� ���� ȸ���� �����մϴ�.
        Vector3 rouletteRotation =
            m_RouletteObject.transform.localEulerAngles;

        // ȸ�� �ӵ��� �������� ����
        _ZRotationVelocity -= _BrakingForce * Time.deltaTime;

        // ȸ�� �ӵ��� 0 �̸��̶�� ȸ���� ����Ű�� �ʰ�
        // �޼��� ȣ���� �����մϴ�.
        if (_ZRotationVelocity < 0.0f)
        {
            m_RestartButton.gameObject.SetActive(true);

            // ȸ���� ������� �ʽ��ϴ�.
            _IsRotatingAllowed = false;

            // �޼��� ȣ���� ����
            return;
        }

        // z�� �������� ȸ����ŵ�ϴ�.
        rouletteRotation.z += _ZRotationVelocity * Time.deltaTime;

        // ȸ������ �����ŵ�ϴ�.
        m_RouletteObject.transform.localEulerAngles = rouletteRotation;

    }


    /// <summary>
    /// ȸ�� �ӵ��� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void InitializeRotationVelocity()
    {
        _ZRotationVelocity = Random.Range(2000.0f, 3000.0f);

    }

    /// <summary>
    /// ����� ǥ���մϴ�.
    /// </summary>
    private void ShowResult()
    {
        // �ɰ� ���� ����� �������� ��Ÿ���� ���� ����
        TMP_Text nearestText = null;

        // �ɰ� ���� ����� �������� ������ �����ϱ� ���� ����
        float nearestAngle = 360.0f;

        foreach (TMP_Text textComponent in m_ItemTexts)
        {
            // �귿�� �߾Ӻ��� �ɱ����� ���а�
            // �귿�� �߾Ӻ��� ������ �ؽ�Ʈ ��ġ������ ���� ������ ������ ���մϴ�.
            float angle = Vector2.SignedAngle(textComponent.transform.up, Vector2.up);

            // ������ ���ϱ� ���Ͽ� ������ ���մϴ�.
            angle = Mathf.Abs(angle);

            // �� ���� ���� ã�� ���
            if(nearestAngle > angle)
            {
                // �ؽ�Ʈ ������Ʈ ����
                nearestText = textComponent;

                // ���� ����
                nearestAngle = angle;
            }
        }
        // ����� ǥ���մϴ�.
        m_ResultText.text = nearestText.text;
    }

    /// <summary>
    /// ��� ������ �ؽ�Ʈ���� Ȱ��ȭ ���¸� �����մϴ�.
    /// </summary>
    /// <param name="active">������ų Ȱ��ȭ ���¸� �����մϴ�.</param>
    private void SetActiveAllItemText(bool active)
    {
        foreach (TMP_Text textComponent in m_ItemTexts)
        {
            textComponent.gameObject.SetActive(active);
        }    
    }

    /// <summary>
    /// �ٽ��ϱ� ��ư Ŭ�� �� ȣ��˴ϴ�.
    /// </summary>
    private void OnRestartButtonClicked()
    {
        // ȸ�� �ӵ��� �ʱ�ȭ �մϴ�.
        _ZRotationVelocity = 0.0f;

        // �귿�� ȸ������ �ʱ�ȭ �մϴ�.
        m_RouletteObject.transform.localEulerAngles = Vector3.zero;

        // �ٽ��ϱ� ��ư ��Ȱ��ȭ
        m_RestartButton.gameObject.SetActive(false);

        // ��� ���ڿ� ��Ȱ��ȭ
        m_ResultText.gameObject.SetActive(false);

        // �귿 ������ �ؽ�Ʈ���� ��� ��Ȱ��ȭ ��ŵ�ϴ�
        SetActiveAllItemText(false);

        // ���� �г� �ʱ�ȭ
        m_RouletteGame.InitialSettingPanel();


    }

    /// <summary>
    /// �귿 ������Ʈ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="itemStrings">������ų ���ڿ����� �����մϴ�.</param>
    public void InitializeRoulette(string[] itemStrings)
    {
        // ������ ���ڿ��� ��� �����մϴ�.
        for (int i = 0; i < itemStrings.Length; ++i)
        {
            // �ؽ�Ʈ ������Ʈ�� �迭���� ����ϴ�.
            TMP_Text textComponent = m_ItemTexts[i];

            // ������ų ���ڿ��� �迭���� ����ϴ�.
            string itemString = itemStrings[i];

            // �ؽ�Ʈ ������Ʈ�� ���ڿ��� �����մϴ�.
            textComponent.text = itemString;
        }

        // ȸ�� �ӵ��� �ʱ�ȭ �մϴ�.
        InitializeRotationVelocity();

        // ��� ������ �ؽ�Ʈ Ȱ��ȭ
        SetActiveAllItemText(true);

        // ��� ���ڿ� Ȱ��ȭ
        m_ResultText.gameObject.SetActive(true);

        // ȸ���� ����մϴ�.
        _IsRotatingAllowed = true;

    }

    /// <summary>
    /// ���� ��� ���� �׻� �׸����� �մϴ�.
    /// OnDrawGizmos �޼���� �����Ϳ����� ��� �����ϸ�, ��ó������ ���� 
    /// ���� ���Ŀ����� �۵����� �ʵ��� �ؾ� �մϴ�.
    /// ������Ʈ�� ���õǾ��� ��쿡�� �׸����� �Ϸ��� 
    /// OnDrawGizmosSelected �޼��带 �����ؾ� �մϴ�.
    /// </summary>
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ����� ������ �����մϴ�.
        Gizmos.color = Color.red;
        // Gizmos : �� �信�� �ð��� ������� ���� Ŭ���� �Դϴ�.
        // OnDrawGizmos �� OnDrawGizmosSelected �޼��� ���ο����� ��� �����մϴ�.

        // �׸� �� ���� ��ǥ ��� ������ ����ϴ�. 
        Matrix4x4 screenMatrix = (transform.parent.transform as RectTransform).localToWorldMatrix;
        // - as : C# ���� ��� ���迡 ���� Ŭ������ ���� ��ȯ�� ���� �������Դϴ�.
        //  [��ü as ��ǥ����] ���� ���� ��ȯ�� ������ �� �ֽ��ϴ�.

        // �׷��� ��ǥ��� ������ �����մϴ�.
        Gizmos.matrix = screenMatrix;
        UnityEditor.Handles.matrix = screenMatrix;

        // �귿 ������Ʈ�� RectTransform ������Ʈ
        RectTransform rouletteRectTransform = m_RouletteObject.transform as RectTransform;

        // ���� ���� ���� ��Ÿ�� ����
        float smallAngle = 360.0f;

        // ���� ���� ���� �̷�� ���� �׸��� ���� ����, ����
        Vector2 lineStartPosition = default;
        Vector2 lineEndPosition = new();

        foreach (TMP_Text textComponent in m_ItemTexts)
        {
            // ���� ������
            Vector2 lineStart = rouletteRectTransform.anchoredPosition;

            // ���� ����
            Vector2 lineEnd = textComponent.rectTransform.up * 400.0f;


            // ���� ����
            Vector2 upVector = Vector2.up;

            // ������� �׸����� �մϴ�.
            Gizmos.color = Color.white;

            // �������� ������ �����Ͽ� ���� �׸��ϴ�.
            Gizmos.DrawLine(lineStart, lineEnd);


            // ������ ���ϰ�, ���� ���� ���� �����ϴ� ����� ���� ���������� ǥ���غ�����.

            // �귿�� �߾Ӻ��� �ɱ����� ���а�
            // �귿�� �߾Ӻ��� ������ �ؽ�Ʈ ��ġ������ ���� ������ ����
            float angle = Mathf.Abs(Vector2.SignedAngle(upVector, textComponent.rectTransform.up));

            // �� ���� ���� ã�Ҵٸ�
            if(smallAngle > angle)
            {
                lineStartPosition = lineStart;
                lineEndPosition = lineEnd;
                smallAngle = angle;
            }

            // �� ���� ���ڿ��� ����մϴ�.
            UnityEditor.Handles.Label(textComponent.transform.up * 500.0f, angle.ToString());

            
            
            // Mathf.Abs(value) : value �� ���� ������ ��ȯ�մϴ�.

            //Vector2.SignedAngle(from,to)
            // Vector2.SignedAngle(from,to) : from �� to ���� ������ ������ ��ȯ�մϴ�.
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStartPosition, lineEndPosition);


    }
#endif

}
