using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInfo : MonoBehaviour
{
    public static BlockInfo instance;
    [SerializeField] private Text blockInfoGradeText;
    [SerializeField] private Text blockInfoDomainText;
    [SerializeField] private Text blockInfoClusterText;
    [SerializeField] private Text blockInfoStandardIDText;
    [SerializeField] private Text blockInfoStandardDescriptionText;
 

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetBlockInfoText(Block block)
    {
        blockInfoGradeText.text=block.grade+" : ";
        blockInfoDomainText.text=block.domain;
        blockInfoClusterText.text=block.cluster;
        blockInfoStandardIDText.text = block.standardid+" : ";
        blockInfoStandardDescriptionText.text = block.standarddescription;
        
    } 
}
