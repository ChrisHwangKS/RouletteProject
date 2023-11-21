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

    [Header("룰렛 오브젝트")]
    public GameObject m_RouletteObject;

    /// <summary>
    /// z 축 회전 속도입니다.
    /// 0 일 경우 회전하지 않습니다.
    /// </summary>
    private float _ZRotationVelocity;

    /// <summary>
    /// 제동력입니다.
    /// 해당 수치만큼 매 프레임마다 회전 속도를 감소시킵니다.
    /// </summary>
    private float _BrakingForce = 1500.0f;

    private void Update()
    {
        // 룰렛을 회전시킵니다.
        RotationRoulette();

    }

    /// <summary>
    /// 룰렛을 회전시킵니다.
    /// </summary>
    private void RotationRoulette()
    {
        // 룰렛의 현재 회전값을 얻어 다음 회전을 설정합니다.
        Vector3 rouletteRotation =
            m_RouletteObject.transform.localEulerAngles;

        // 회전 속도에 제동력을 적용
        _ZRotationVelocity -= _BrakingForce * Time.deltaTime;

        // 회전 속도가 0 미만이라면 회전을 허용시키지 않고
        // 메서드 호출을 종료합니다.
        if (_ZRotationVelocity < 0.0f)
        {
            // 메서드 호출을 종료
            return;
        }

        // z축 기준으로 회전시킵니다.
        rouletteRotation.z += _ZRotationVelocity * Time.deltaTime;

        // 회전값을 적용시킵니다.
        m_RouletteObject.transform.localEulerAngles = rouletteRotation;
    }


    /// <summary>
    /// 회전 속도를 초기화합니다.
    /// </summary>
    private void InitializeRotationVelocity()
    {
        _ZRotationVelocity = Random.Range(2000.0f, 3000.0f);

    }

    /// <summary>
    /// 룰렛 오브젝트를 초기화합니다.
    /// </summary>
    /// <param name="itemStrings">설정시킬 문자열들을 전달합니다.</param>
    public void InitializeRoulette(string[] itemStrings)
    {
        // 아이템 문자열을 모두 설정합니다.
        for (int i = 0; i < itemStrings.Length; ++i)
        {
            // 텍스트 컴포넌트를 배열에서 얻습니다.
            TMP_Text textComponent = m_ItemTexts[i];

            // 설정시킬 문자열을 배열에서 얻습니다.
            string itemString = itemStrings[i];

            // 텍스트 컴포넌트의 문자열을 설정합니다.
            textComponent.text = itemString;
        }

        // 회전 속도를 초기화 합니다.
        InitializeRotationVelocity();

    }

    /// <summary>
    /// 씬에 어떠한 것을 항상 그리도록 합니다.
    /// OnDrawGizmos 메서드는 에디터에서만 사용 가능하며, 전처리문을 통해 
    /// 빌드 이후에서는 작동하지 않도록 해야 합니다.
    /// 오브젝트가 선택되었을 경우에만 그리도록 하려면 
    /// OnDrawGizmosSelected 메서드를 정의해야 합니다.
    /// </summary>
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 기즈모 색상을 설정합니다.
        Gizmos.color = Color.red;
        // Gizmos : 씬 뷰에서 시각적 디버깅을 위한 클래스 입니다.
        // OnDrawGizmos 나 OnDrawGizmosSelected 메서드 내부에서만 사용 가능합니다.

        // 그릴 때 사용될 좌표 행렬 정보를 얻습니다. 
        Matrix4x4 screenMatrix = (transform.parent.transform as RectTransform).localToWorldMatrix;
        // - as : C# 에서 상속 관계에 속한 클래스의 형식 변환을 위한 연산자입니다.
        //  [객체 as 목표형식] 으로 형식 변환을 진행할 수 있습니다.

        // 그려질 좌표행렬 정보를 설정합니다.
        Gizmos.matrix = screenMatrix;
        UnityEditor.Handles.matrix = screenMatrix;

        // 룰렛 오브젝트의 RectTransform 컴포넌트
        RectTransform rouletteRectTransform = m_RouletteObject.transform as RectTransform;

        foreach (TMP_Text textComponent in m_ItemTexts)
        {
            // 선의 시작점
            Vector2 lineStart = rouletteRectTransform.anchoredPosition;

            // 선의 끝점
            Vector2 lineEnd = textComponent.rectTransform.up * 400.0f;


            // 비교할 방향
            Vector2 upVector = Vector2.up;

            Gizmos.color = Color.white;

            // 시작점과 끝점을 지정하여 선을 그립니다.
            Gizmos.DrawLine(lineStart, lineEnd);


            // 각도를 구하고, 가장 작은 각을 형성하는 요소의 선을 빨간색으로 표시해보세요.

            float angle = Mathf.Abs(Vector2.SignedAngle(upVector, textComponent.rectTransform.up));

            // Mathf.Abs(value) : value 에 대한 절댓값을 반환합니다.

            //Vector2.SignedAngle(from,to)
            // Vector2.SignedAngle(from,to) : from 과 to 벡터 사이의 각도를 반환합니다.
        }

    }
#endif

}
