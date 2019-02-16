using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject MechHUD, SwitcherHUD, NavHUD;
    private RectTransform SlotHighlightTransform;
    private List<GameObject> slotImages;
    public GameObject slotImageTemplate;
    private Vector3 slotPos, slotPosDelta;

    // Start is called before the first frame update
    void Start()
    {
        MechHUD = transform.Find("MechHUD").gameObject;
        SwitcherHUD = transform.Find("SwitcherHUD").gameObject;
        NavHUD = transform.Find("NavHUD").gameObject;
        SlotHighlightTransform = SwitcherHUD.transform.GetChild(0).GetComponent<RectTransform>();

        slotImages = new List<GameObject>();
        slotPos = SlotHighlightTransform.anchoredPosition;
        slotPosDelta = new Vector3(80, 0, 0);

        MechHUD.SetActive(false);
        SwitcherHUD.SetActive(true);
        NavHUD.SetActive(false);
    }

    public void CreateSwitcherUI(PlayerInput input)
    {
        slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
        slotImages[0].GetComponent<RectTransform>().anchoredPosition = slotPos;
        slotPos += slotPosDelta;

        for (int i = 1; i < input.weapons.Count + 1; i++)
        {
            slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
            slotImages[i].GetComponent<RectTransform>().anchoredPosition = slotPos;
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

    public void DisableDynamicUI()
    {
        MechHUD.SetActive(false);
        NavHUD.SetActive(false);
    }

    public void ChangeSlotHighlight(int index)
    {
        index += 1;
        SlotHighlightTransform.anchoredPosition = slotImages[index].GetComponent<RectTransform>().anchoredPosition;
        DisableDynamicUI();
    }

    void Update()
    {

    }
}
