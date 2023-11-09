using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private Animator _doorAnim;
    [SerializeField] private PhysicsCallbackProvider _distanceTrigger;
    [SerializeField] private string _heroTag = "HeroSphere";

    private void OnEnable()
    {
        _distanceTrigger.triggerEnter += GameWinCheck;
    }

    private void OnDisable()
    {
        _distanceTrigger.triggerEnter -= GameWinCheck;
    }

    private void GameWinCheck(Collider collider)
    {
        if (collider.gameObject.CompareTag(_heroTag))
        {
            _doorAnim.SetTrigger("Open");
            Destroy(collider.gameObject, 2f);

            StartCoroutine(LoadWinScene());
        }
    }

    private IEnumerator LoadWinScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Constants.WinScene);
    }
}
