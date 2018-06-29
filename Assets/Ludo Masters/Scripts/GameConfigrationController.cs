using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigrationController : MonoBehaviour
{

    public GameObject TitleText;
    public GameObject bidText;
	public GameObject bidPersentText;
    public GameObject MinusButton;
    public GameObject PlusButton;
    public GameObject[] Toggles;
    private int currentBidIndex = 0;

	public GameObject MinusButtonpersent;
	public GameObject PlusButtonpersent;

    private MyGameMode[] modes = new MyGameMode[] { MyGameMode.Classic, MyGameMode.Quick, MyGameMode.Master };
    public GameObject privateRoomJoin;
    // Use this for initialization
    void Start()
    {
		ResetPlusMinus();
    }
	public Toggle check1,check2;
//	public	void ResetBtns()
//	{
//		//check1.interactable = true;
////		check2.interactable = true;
////		check1.isOn = false;
////		check2.isOn = false;
//		PlusButton.GetComponent<Button>().interactable = true;
//		PlusButtonpersent.GetComponent<Button>().interactable = true;
//		MinusButton.GetComponent<Button>().interactable = false;
//		MinusButtonpersent.GetComponent<Button>().interactable = false;
//	}

	public void ResetPlusMinus()
	{
		PlusButton.GetComponent<Button>().interactable = true;
		PlusButtonpersent.GetComponent<Button>().interactable = true;
		MinusButton.GetComponent<Button>().interactable = false;
		MinusButtonpersent.GetComponent<Button>().interactable = false;
	}

	public void topBtnClick()
	{
		PlusButtonpersent.GetComponent<Button>().interactable = false;
		MinusButtonpersent.GetComponent<Button>().interactable = false;
	}
	public void bottomBtnClick()
	{
		PlusButton.GetComponent<Button>().interactable = false;
		MinusButton.GetComponent<Button>().interactable = false;
	}


//	public void Toggle1BtnClick()
//	{
//		check1.isOn = true;
//		check1.interactable = false;
//		check2.isOn = false;
//		check2.interactable = false;
////		PlusButton.GetComponent<Button>().interactable = true;
//		UpdateBid(true);
//
//	}
//	public void Toggle2BtnClick()
//	{
//		check2.isOn =true;
//		check2.interactable = false;
//		check1.isOn = false;
//		check1.interactable = false;
////		PlusButtonpersent.GetComponent<Button>().interactable = true;
//		UpdatePersentBid(true);
//
//	}

    // Update is called once per frame
    void Update()
    {

    }


    void OnEnable()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            int index = i;
            Toggles[i].GetComponent<Toggle>().onValueChanged.AddListener((value) =>
                {
                    ChangeGameMode(value, modes[index]);
                }
            );
        }

        currentBidIndex = 0;
//        UpdateBid(true);

        Toggles[0].GetComponent<Toggle>().isOn = true;
        GameManager.Instance.mode = MyGameMode.Classic;

        switch (GameManager.Instance.type)
        {
            case MyGameType.TwoPlayer:
                TitleText.GetComponent<Text>().text = "Two Players";
                break;
            case MyGameType.FourPlayer:
                TitleText.GetComponent<Text>().text = "Four Players";
                break;
            case MyGameType.Private:
                TitleText.GetComponent<Text>().text = "Private Room";
                privateRoomJoin.SetActive(true);
                break;
        }

    }

    void OnDisable()
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            int index = i;
            Toggles[i].GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
        }

        privateRoomJoin.SetActive(false);
        currentBidIndex = 0;
//        UpdateBid(false);
        Toggles[0].GetComponent<Toggle>().isOn = true;
        Toggles[1].GetComponent<Toggle>().isOn = false;
        Toggles[2].GetComponent<Toggle>().isOn = false;
    }

    public void setCreatedProvateRoom()
    {
        GameManager.Instance.JoinedByID = false;
    }

    public void startGame()
    {
        if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
        {
            if (GameManager.Instance.type != MyGameType.Private)
            {
                GameManager.Instance.facebookManager.startRandomGame();
            }
            else
            {
                if (GameManager.Instance.JoinedByID)
                {
                    Debug.Log("Joined by id!");
                    GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
                }
                else
                {
                    Debug.Log("Joined and created");
                    GameManager.Instance.playfabManager.CreatePrivateRoom();
                    GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
                }

            }
        }
        else
        {
            GameManager.Instance.dialog.SetActive(true);
        }
    }

    private void ChangeGameMode(bool isActive, MyGameMode mode)
    {
        if (isActive)
        {
            GameManager.Instance.mode = mode;
        }
    }



    public void IncreaseBid()
    {
        if (currentBidIndex < StaticStrings.bidValues.Length - 1)
        {
            currentBidIndex++;
            UpdateBid(true);
        }
    }

    public void DecreaseBid()
    {
        if (currentBidIndex > 0)
        {
            currentBidIndex--;
            UpdateBid(true);
        }
    }


	public void IncreaseBidPersent()
	{
		if (currentBidIndex < StaticStrings.bidValues.Length - 1)
		{
			currentBidIndex++;
			UpdatePersentBid(true);
		}
	}

	public void DecreaseBidpersent()
	{
		if (currentBidIndex > 0)
		{
			currentBidIndex--;
			UpdatePersentBid(true);
		}
	}


    private void UpdateBid(bool changeBidInGM)
    {
        bidText.GetComponent<Text>().text = StaticStrings.bidValuesStrings[currentBidIndex];

        if (changeBidInGM)
            GameManager.Instance.payoutCoins = StaticStrings.bidValues[currentBidIndex];

        if (currentBidIndex == 0) MinusButton.GetComponent<Button>().interactable = false;
        else MinusButton.GetComponent<Button>().interactable = true;

        if (currentBidIndex == StaticStrings.bidValues.Length - 1) PlusButton.GetComponent<Button>().interactable = false;
        else PlusButton.GetComponent<Button>().interactable = true;
    }

	private void UpdatePersentBid(bool changeBidInGM)
	{
		bidPersentText.GetComponent<Text>().text = StaticStrings.bidValuesStrings[currentBidIndex];

		if (changeBidInGM)
			GameManager.Instance.payoutCoins = StaticStrings.bidValues[currentBidIndex];

		if (currentBidIndex == 0) MinusButtonpersent.GetComponent<Button>().interactable = false;
		else MinusButtonpersent.GetComponent<Button>().interactable = true;

		if (currentBidIndex == StaticStrings.bidValues.Length - 1) PlusButtonpersent.GetComponent<Button>().interactable = false;
		else PlusButtonpersent.GetComponent<Button>().interactable = true;
	}

    public void HideThisScreen()
    {
        gameObject.SetActive(false);
    }
}
