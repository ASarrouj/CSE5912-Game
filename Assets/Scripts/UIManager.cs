using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject MechHUD, SwitcherHUD, NavHUD, MenuHUD;
    private RectTransform slotHighlightTransform;
    private List<GameObject> slotImages;
    public GameObject slotImageTemplate;
    private Vector3 slotPos, slotPosDelta;

    // Start is called before the first frame update
    void Start()
    {
        MechHUD = transform.Find("MechHUD").gameObject;
        SwitcherHUD = transform.Find("SwitcherHUD").gameObject;
        NavHUD = transform.Find("NavHUD").gameObject;
        MenuHUD = transform.Find("MenuHUD").gameObject;
        slotHighlightTransform = SwitcherHUD.transform.GetChild(0).GetComponent<RectTransform>();

        slotImages = new List<GameObject>();
        slotPos = slotHighlightTransform.anchoredPosition;
        slotPosDelta = new Vector3(80, 0, 0);

        MechHUD.SetActive(false);
        SwitcherHUD.SetActive(true);
        NavHUD.SetActive(false);
        MenuHUD.SetActive(false);

        Cursor.visible = false;
    }

    public void CreateSwitcherUI(PlayerInput input)
    {
        slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
        slotImages[0].GetComponent<RectTransform>().anchoredPosition = slotPos;
        slotImages[0].transform.GetChild(0).GetComponent<Text>().text = "1";
        slotPos += slotPosDelta;

        for (int i = 1; i < input.slots.Count + 1; i++)
        {
            slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
            slotImages[i].GetComponent<RectTransform>().anchoredPosition = slotPos;
            slotImages[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            slotPos += slotPosDelta;
        }
    }

    public void MechUI()
    {
        MechHUD.SetActive(true);
        NavHUD.SetActive(true);
    }

    public void WeaponUI()
    {
        
    }

    public void MenuUI()
    {
        MenuHUD.SetActive(!MenuHUD.activeSelf);
        //Cursor.visible = !Cursor.visible;
    }

    public void DisableDynamicUI()
    {
        MechHUD.SetActive(false);
        NavHUD.SetActive(false);
    }

    public void ChangeSlotHighlight(int index)
    {
        index += 1;
        slotHighlightTransform.anchoredPosition = slotImages[index].GetComponent<RectTransform>().anchoredPosition;
        DisableDynamicUI();
    }

    void Update()
    {

    }
}
