using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBlood : MonoBehaviour
{
    [Header("Blood")]
    public List<Transform> SummonedBloodDots;
    public List<float> SummonedBloodDotsPercent;
    [Range(0,2)]public float AnimationSpeed = 1;
    public Transform BloodPrefabOrb;
    public Transform BloodDespawnPrefabEffect;
    [Header("Settings")]
    [Tooltip("Int (1 to 4) that determins the number of curve arcs")] [Range(1, 4)] public int CurvePairs = 1;//Curve pair length
    [Tooltip("Float (0 to 1) which is based on the CurvePairs length")] [Range(0, 1)] public float CurvePercent;//0 to 1 Percent that determins RequestCurrentPercent()
    [Header("Internal Data")]
    [Tooltip("Optional Empty that is moved around based on CurvePairs and CurvePercent")]public Transform AnimatedObject;
    [Tooltip("List of the transforms that will be used in the curve calculation")] public Transform[] PointsT;//Ref to the needed transforms

    public void Update()
    {
        if(SummonedBloodDots.Count > 0)
        {
            for (int i = 0; i < SummonedBloodDots.Count; i++)
            {
                if(SummonedBloodDotsPercent[i] < 1)
                {
                    UpdateAnimatedPointsPosition(SummonedBloodDotsPercent[i], SummonedBloodDots[i]);
                    SummonedBloodDotsPercent[i] += Time.deltaTime* AnimationSpeed;
                }
                else
                {
                    if(BloodDespawnPrefabEffect != null)
                    {
                        var newBloodEffect =Instantiate(BloodDespawnPrefabEffect, SummonedBloodDots[i].transform.position,Quaternion.identity);
                        Destroy(newBloodEffect, 3);
                    }
                    var bloodT = SummonedBloodDots[i];
                    SummonedBloodDots.RemoveAt(i);
                    SummonedBloodDotsPercent.RemoveAt(i);
                    Destroy(bloodT.gameObject);
                    continue;
                }
            }
        }
    }
    public void SpawnBlood()
    {
        var NewBlood = Instantiate(BloodPrefabOrb, transform.position-(Vector3.up*10), Quaternion.identity);
        SummonedBloodDots.Add(NewBlood);
        SummonedBloodDotsPercent.Add(0);
    }
    public void ClearBlood()
    {
        foreach (var item in SummonedBloodDots)
        {
            Destroy(item.gameObject);
        }
        SummonedBloodDots.Clear();
        SummonedBloodDotsPercent.Clear();
    }
    #region API
    public void UpdateAnimatedPointsPosition(float percent = 0, Transform animObject=null)
    {
        if (animObject == null) animObject = AnimatedObject;
        animObject.transform.position = CalculateCubicBezierPoint(percent*0.99f);
        animObject.transform.LookAt(CalculateCubicBezierPoint(percent), Vector3.up);
    }
    public Vector3 GetCurrentPercentPosition()
    {
        return CalculateCubicBezierPoint(CalculateScaledCurveDecimal(), CalculateScaledCurveFloor());
    }
    #endregion
    #region Main Code Block
    private float CalculateScaledCurveDecimal()
    {
        return CurvePercent * CurvePairs - CalculateScaledCurveFloor();
    }
    private int CalculateScaledCurveFloor()
    {
        return Mathf.RoundToInt(Mathf.Floor(CurvePercent * CurvePairs));
    }
    private Vector3 CalculateCubicBezierPoint(float Percent, int SelectedLoop = 0)
    {
        Vector3 PointAtPercent = Vector3.zero;
        if ((SelectedLoop * 3) == PointsT.Length-1) return PointsT[PointsT.Length-1].position;
        PointAtPercent =
            PointsT[(SelectedLoop * 3) + 0].position * Mathf.Pow((1 - Percent), 3) +
            PointsT[(SelectedLoop * 3) + 1].position * Mathf.Pow((1 - Percent), 2) * 3 * Percent +
            PointsT[(SelectedLoop * 3) + 2].position * Mathf.Pow(Percent, 2)       * 3 * (1 - Percent)  +
            PointsT[(SelectedLoop * 3) + 3].position * Mathf.Pow((Percent), 3);
        return PointAtPercent;
    }
    private void ShowHidePointsBasedOnCurvePairs()
    {
        var desiredPairs = Mathf.Clamp(1 + (3 * CurvePairs), 0, PointsT.Length);
        for (int i = 0; i < PointsT.Length; i++)
        {
            PointsT[i].gameObject.SetActive(i < desiredPairs);
        }
    }
    #endregion
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
       // UpdateAnimatedPointsPosition(CurvePercent);
        ShowHidePointsBasedOnCurvePairs();
        for (int i = 0; i < CurvePairs; i++)
        {
            if(i == 0) Gizmos.color = Color.green;
            if(i == 1) Gizmos.color = Color.blue;
            if(i == 2) Gizmos.color = Color.yellow;
            if(i == 3) Gizmos.color = Color.red;

            for (int j = 0; j < 10; j++)
            {
                Gizmos.DrawLine(
                    CalculateCubicBezierPoint((j * 0.1f), i),
                    CalculateCubicBezierPoint(((j + 1) * 0.1f), i));
            }
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(PointsT[(i * 3) + 0].position, PointsT[(i * 3) + 1].position);
            Gizmos.DrawLine(PointsT[(i * 3) + 2].position, PointsT[(i * 3) + 3].position);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CalculateCubicBezierPoint(CalculateScaledCurveDecimal(), CalculateScaledCurveFloor()), 0.1f);
    }
    #endif
}
