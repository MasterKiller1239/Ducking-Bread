using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    public RenderPipelineAsset[] qualitylevels;
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualitylevels[value];
    }
}
