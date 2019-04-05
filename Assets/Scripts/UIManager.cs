using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject self;
    private GameObject MechHUD, SwitcherHUD, NavHUD, MenuHUD, ScoreHUD, GunHUD;
    private RectTransform slotHighlightTransform;
    private List<GameObject> slotImages;
    public GameObject slotImageTemplate;
    private Vector3 slotPos, slotPosDelta;
    public Sprite mechSprite, repairSprite, machineGunSprite, artillerySprite;

    // Start is called before the first frame update
    void Start()
    {
        MechHUD = transform.Find("MechHUD").gameObject;
        SwitcherHUD = transform.Find("SwitcherHUD").gameObject;
        NavHUD = transform.Find("NavHUD").gameObject;
        MenuHUD = transform.Find("MenuHUD").gameObject;
        ScoreHUD = transform.Find("ScoreHUD").gameObject;
        GunHUD = transform.Find("GunHUD").gameObject;
        slotHighlightTransform = SwitcherHUD.transform.GetChild(0).GetComponent<RectTransform>();

        slotImages = new List<GameObject>();
        slotPos = slotHighlightTransform.anchoredPosition;
        slotPosDelta = new Vector3(80, 0, 0);

        MechHUD.SetActive(false);
        SwitcherHUD.SetActive(true);
        NavHUD.SetActive(false);
        MenuHUD.SetActive(false);
        GunHUD.SetActive(false);
        ScoreboardUI(false);

        self.SetActive(false);
    }

    public void CreateSwitcherUI(PlayerInput input)
    {
        slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
        slotImages[0].GetComponent<RectTransform>().anchoredPosition = slotPos;
        slotImages[0].transform.Find("SlotNum").GetComponent<Text>().text = "1";
        slotImages[0].transform.Find("SlotSprite").GetComponent<Image>().sprite = mechSprite;
        slotPos += slotPosDelta;

        for (int i = 1; i < input.slots.Count + 1; i++)
        {
            slotImages.Add(Instantiate(slotImageTemplate, SwitcherHUD.transform));
            slotImages[i].GetComponent<RectTransform>().anchoredPosition = slotPos;
            slotImages[i].transform.Find("SlotNum").GetComponent<Text>().text = (i + 1).ToString();

            if (input.slots[i - 1].tag == "Weapon")
            {
                slotImages[i].transform.Find("SlotSprite").GetComponent<Image>().sprite = machineGunSprite;
            }
            else if (input.slots[i - 1].tag == "RepairTool")
            {
                slotImages[i].transform.Find("SlotSprite").GetComponent<Image>().sprite = repairSprite;
            }

            slotPos += slotPosDelta;
        }
        self.SetActive(false);
    }

    public void MechUI()
    {
        MechHUD.SetActive(true);
        NavHUD.SetActive(true);
        GunHUD.SetActive(false);
    }

    public void WeaponUI()
    {
        MechHUD.SetActive(true);
        NavHUD.SetActive(false);
        GunHUD.SetActive(true);
    }

    public void MenuUI()
    {
        MenuHUD.SetActive(!MenuHUD.activeSelf);
        Cursor.visible = MenuHUD.activeSelf;
        GunHUD.SetActive(false);
    }

    public void ScoreboardUI(bool show)
    {
        ScoreHUD.GetComponent<Canvas>().enabled = show;
    }

    public void DisableDynamicUI()
    {
        MechHUD.SetActive(false);
        NavHUD.SetActive(false);
        GunHUD.SetActive(false);
    }

    public void ChangeSlotHighlight(int index)
    {
        index += 1;
        slotHighlightTransform.anchoredPosition = slotImages[index].GetComponent<RectTransform>().anchoredPosition;
        DisableDynamicUI();
    }
}
