using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDance : MonoBehaviour
{
    [SerializeField] private GameObject _bladeDance;
    [SerializeField] private GameObject _bladeDanceLastHit;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(UltSkill());
        }
    }

    IEnumerator UltSkill()
    {
        yield return new WaitForSeconds(1f);
        _bladeDance.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _bladeDance.SetActive(false);
        _bladeDanceLastHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        _bladeDanceLastHit.SetActive(false);
        StopCoroutine(UltSkill());
    }
}
