using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public enum Action { Play, Quit, Credit, Return, Options};
    public Action action;
    public AudioClip soundEffect;
    public AudioSource source;

    private bool ignore = false;

    void OnMouseUp() {
        if (!ignore) {
            ignore = true;
            StartCoroutine(TakeAction());
        }
    }

    IEnumerator TakeAction() {
        source.PlayOneShot(soundEffect);
        yield return new WaitForSeconds(0.2f);
        ignore = true;
        switch (action) {
            case Action.Play:
                StartCoroutine(Play());
                break;
            case Action.Quit:
                QuitGame();
                break;
            case Action.Options:
                SceneManager.LoadScene(5);
                break;
            case Action.Credit:
                CreditScreen();
                break;
            case Action.Return:
                ReturnToMain();
                break;
            default:
                break;
        }
        yield return null;
    }

    IEnumerator Play() {
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
        Application.Quit();
    }

    private void CreditScreen() {
        GameStateManager.QuickLoadState(4);
    }

    private void ReturnToMain()
    {
        GameStateManager.QuickLoadState(2);
    }

}
