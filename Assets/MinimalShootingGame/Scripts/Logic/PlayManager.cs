using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MinimalShooting
{
    public class PlayManager : SingletonMonobehaviour<PlayManager>
    {
        [SerializeField]
        Camera gameCamera;


        public void GameOver()
        {
            this.gameCamera.GetComponent<CameraShake>().enabled = true;
            StartCoroutine(ReloadCurrentScene());
        }


        IEnumerator ReloadCurrentScene()
        {
            yield return new WaitForSeconds(3.0f);

            SceneManager.LoadScene(0);
        }
    }
}
