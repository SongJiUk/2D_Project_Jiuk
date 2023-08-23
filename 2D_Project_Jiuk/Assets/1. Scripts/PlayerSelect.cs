using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    const int PlayerNum = 4;
    [SerializeField] Animator[] anim;
    [SerializeField] GameObject[] p1_img;
    [SerializeField] GameObject[] Player_pick_img;
    [SerializeField] GameObject[] Player_Select_img;
    [SerializeField] GameObject[] Player_IMG;


    int picknum = 0;
    void Start()
    {
        StartCoroutine(SelectPlayer());
    }

    IEnumerator SelectPlayer()
    {
        while(true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (picknum < 3) picknum++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (picknum > 0) picknum--;
            }


            for (int i = 0; i < PlayerNum; i++)
            {
                if (picknum == i)
                {
                    p1_img[i].SetActive(true);
                    Player_pick_img[i].SetActive(true);

                }
                else
                {
                    p1_img[i].SetActive(false);
                    Player_pick_img[i].SetActive(false);

                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Player_Select_img[picknum].SetActive(true);
                Player_IMG[picknum].SetActive(true);
                Invoke("PlayAnim", 0.5f);
                break;
            }

        }
    }

    void PlayAnim()
    {
        anim[picknum].SetTrigger("StartAnim");
        Invoke("NextScene", 3f);
    }

    void NextScene()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
