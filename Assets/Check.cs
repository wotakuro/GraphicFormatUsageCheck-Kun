using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    [SerializeField]
    private Text result;


    [SerializeField]
    private ScrollRect scroll;
    [SerializeField]
    private Dropdown dropdown;
    private StringBuilder stringBuilder;
    private List<FormatUsage> usagesList;

    private void InitDropDownMenu()
    {
        usagesList = new List<FormatUsage>();
        dropdown.ClearOptions();
        List<Dropdown.OptionData> datas = new List<Dropdown.OptionData>();
        var vals = System.Enum.GetValues(typeof(FormatUsage));
        foreach (var val in vals) {
            Dropdown.OptionData option = new Dropdown.OptionData(val.ToString());
            datas.Add(option);
            usagesList.Add((FormatUsage)val);
        }
        dropdown.options = datas;
        dropdown.onValueChanged.AddListener( OnChangeDropdown);
        OnChangeDropdown(0);
    }
    private void OnChangeDropdown(int idx)
    {
        const int lineSize = 18;
        stringBuilder.Clear();
        int num = GetFormats(stringBuilder, usagesList[idx]);
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, num * lineSize);
        result.rectTransform.sizeDelta = new Vector2(result.rectTransform.sizeDelta.x, num * lineSize);
        result.text = this.stringBuilder.ToString();
    }

    private void Awake()
    {
        stringBuilder = new StringBuilder(512);
        InitDropDownMenu();
    }

    int GetFormats(StringBuilder sb,FormatUsage usage)
    {
        int num = 0;
        var vals = System.Enum.GetValues(typeof(GraphicsFormat));
        foreach (var val in vals)
        {
            var t = (GraphicsFormat)val;
            bool isSupportReadPixel = SystemInfo.IsFormatSupported(t, usage);
            if (isSupportReadPixel)
            {
                sb.Append(t).Append('\n');
                ++num;
            }
        }
        return num;
    }
        
}
