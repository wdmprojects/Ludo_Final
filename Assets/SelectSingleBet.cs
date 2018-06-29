using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectSingleBet : MonoBehaviour {

	public Button panel1plusBtn;
	public Button panel2plusBtn;

	public Toggle toggle1,toggle2;
	// Use this for initialization
	void Start () {
		

		toggle1.isOn = false;
		toggle1.interactable = true;
		toggle2.isOn = false;
		toggle2.interactable = true;
	}
	public	void ResetBtns()
	{
		
		toggle1.interactable = true;
		toggle2.interactable = true;
		toggle1.isOn = false;
		toggle2.isOn = false;

		panel1plusBtn.interactable = false;
		panel2plusBtn.interactable = false;
	}

	public void Toggle1BtnClick()
	{
		toggle1.isOn = true;
		toggle1.interactable = false;
		toggle2.isOn = false;
		toggle2.interactable = false;
		panel1plusBtn.interactable = true;

	}
	public void Toggle2BtnClick()
	{
		toggle2.isOn =true;
		toggle2.interactable = false;
		toggle1.isOn = false;
		toggle1.interactable = false;
		panel2plusBtn.interactable = true;

	}
}
