using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RouletteObject : MonoBehaviour
{
    /// <summary>
    /// ������ �ؽ�Ʈ���� ��Ÿ���ϴ�.
    /// </summary>
    [Header("������ ���ڿ�")]
    public TMP_Text[] m_ItemTexts;

    [Header("�귿 ������Ʈ")]
    public GameObject m_RouletteObject;

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

    private void Update()
    {
        // �귿�� ȸ����ŵ�ϴ�.
        RotationRoulette();

    }

    /// <summary>
    /// �귿�� ȸ����ŵ�ϴ�.
    /// </summary>
    private void RotationRoulette()
    {
        // �귿�� ���� ȸ������ ��� ���� ȸ���� �����մϴ�.
        Vector3 rouletteRotation =
            m_RouletteObject.transform.localEulerAngles;

        // ȸ�� �ӵ��� �������� ����
        _ZRotationVelocity -= _BrakingForce * Time.deltaTime;

        // ȸ�� �ӵ��� 0 �̸��̶�� ȸ���� ����Ű�� �ʰ�
        // �޼��� ȣ���� �����մϴ�.
        if (_ZRotationVelocity < 0.0f)
        {
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

        float minAngle = 90.0f;

        foreach (TMP_Text textComponent in m_ItemTexts)
        {
            // ���� ������
            Vector2 lineStart = rouletteRectTransform.anchoredPosition;

            // ���� ����
            Vector2 lineEnd = textComponent.rectTransform.up * 400.0f;


            // ���� ����
            Vector2 upVector = Vector2.up;

            Gizmos.color = Color.white;

            // �������� ������ �����Ͽ� ���� �׸��ϴ�.
            Gizmos.DrawLine(lineStart, lineEnd);


            // ������ ���ϰ�, ���� ���� ���� �����ϴ� ����� ���� ���������� ǥ���غ�����.

            float angle = Mathf.Abs(Vector2.SignedAngle(upVector, textComponent.rectTransform.up));
            float temp = angle;
            if(temp <= minAngle) minAngle = temp;

            lineEnd = new Vector2(Mathf.Sin(minAngle), Mathf.Cos(minAngle))*400.0f;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(lineStart, lineEnd);
            
            // Mathf.Abs(value) : value �� ���� ������ ��ȯ�մϴ�.

            //Vector2.SignedAngle(from,to)
            // Vector2.SignedAngle(from,to) : from �� to ���� ������ ������ ��ȯ�մϴ�.
        }

        
    }
#endif

}
