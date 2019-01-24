using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public enum Action { Play, Quit, Back, Options };
    public Action action;
    public AudioClip soundEffect;
    public AudioSource source;

    private bool ignore = false;

    void OnMouseUp() {
        if (!ignore) {
            ignore = true;
            TakeAction();
        }
    }

    public void TakeAction() {
        ignore = true;
        switch (action) {
            case Action.Play:
                StartCoroutine(Play());
                break;
            case Action.Quit:
                QuitGame();
                break;
            case Action.Back:
                SceneManager.LoadScene(2);
                break;
            case Action.Options:
                SceneManager.LoadScene(4);
                break;
            default:
                break;
        }
    }

    IEnumerator Play() {
        source.PlayOneShot(soundEffect);
        GameObject blackScreen = GameObject.CreatePrimitive(PrimitiveType.Plane);
        blackScreen.transform.SetPositionAndRotation(new Vector3(0, 0, -5), Quaternion.Euler(270, 0, 0));
        blackScreen.transform.localScale = new Vector3(1.8f, 1, 1.2f);
        blackScreen.layer = 1;
        Material m = blackScreen.GetComponent<Renderer>().material;
        m.SetFloat("_Mode", 3);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.DisableKeyword("_ALPHABLEND_ON");
        m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
        for (float i = 0f; i <= 3; i += Time.deltaTime) {
            m.color = Color.Lerp(Color.clear, Color.black, i / 3);
            yield return null;
        }
        GameStateManager.LoadState(3);
    }

    private void QuitGame() {
        source.PlayOneShot(soundEffect);
        Application.Quit();
    }
}
