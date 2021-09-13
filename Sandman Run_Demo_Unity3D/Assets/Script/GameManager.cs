using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Limbs limbs = new Limbs();

    private bool _youLose;
    private Animator animatorOf3DModel; // Animator that attached to "ybot@Idle"
    private GameObject _3DModel;// 3D model is "ybot@Idle"

    private void Start()
    {
        animatorOf3DModel = GameObject.Find("ybot@Idle").GetComponent<Animator>();
        _3DModel = GameObject.Find("ybot@Idle");

        InvokeRepeating("SetAnimationBasedOnAvailability", 0, 0.1f);
        limbs.SetInitialCountsAndSortListst();
    }

    private void Update()
    {
        limbs.CollapseAllLimbsIfChestOrAllLimbsThatMovesCollapses();
    }

    private void SetAnimationBasedOnAvailability()
    {
        limbs.CalculateAllAvailabilitiesByCount();

        if(limbs.rightLegAvailability > 0.7f && limbs.leftLegAvailability > 0.7f)
        {
            Debug.Log("Run");
            animatorOf3DModel.SetTrigger("Run");
        }
        else if(limbs.rightLegAvailability > 0.7f)
        {
            Debug.Log("RunWithRightLeg");
            animatorOf3DModel.SetTrigger("RunWithRightLeg");
        }
        else if(limbs.leftLegAvailability > 0.7f)
        {
            Debug.Log("RunWithLeftLeg");
            animatorOf3DModel.SetTrigger("RunWithLeftLeg");
        }
        else if(limbs.leftArmAvailability > 0.3f && limbs.rightArmAvailability > 0.3f)
        {
            Debug.Log("Crawl");
            animatorOf3DModel.SetTrigger("Crawl");
        }
        else if(limbs.rightArmAvailability > 0.3f)
        {
            Debug.Log("CrawlWithRightArm");
            animatorOf3DModel.SetTrigger("CrawlWithRightArm");
        }
        else if(limbs.leftArmAvailability > 0.3f)
        {
            Debug.Log("CrawlWithLeftArm");
            animatorOf3DModel.SetTrigger("CrawlWithLeftArm");
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        limbs = new Limbs();
    }
}
