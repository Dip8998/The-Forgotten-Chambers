using UnityEngine;
using Unity.Cinemachine;

public class CinemachineTargetSetter : MonoBehaviour
{
    private CinemachineCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        StartCoroutine(WaitAndSetTarget());
    }

    private System.Collections.IEnumerator WaitAndSetTarget()
    {
        GameObject player = null;

        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }

        vcam.Follow = player.transform;
    }
}
