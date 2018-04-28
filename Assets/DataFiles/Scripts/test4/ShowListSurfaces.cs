using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowListSurfaces : MonoBehaviour {

	public GameObject panel;
	public GameObject scroll;

	bool trigger_list = false;

	void Start(){
		panel.SetActive (trigger_list);
		scroll.SetActive (trigger_list);
	}

	public void ShowHideList(){
		trigger_list = !trigger_list;
		panel.SetActive (trigger_list);
		scroll.SetActive (trigger_list);
	}
}
