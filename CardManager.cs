using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardManager : MonoBehaviour
{
    
	public GameObject[] card1;
	GameObject[] p1 = {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
	GameObject[] p2 = {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
	int[] cardOrder = {7, 21, 2, 24, 17, 5, 13, 0, 8, 9, 10, 23, 12, 6, 14, 25, 16, 4, 18, 19, 20, 1, 22, 11, 3, 15};
	int cardnum = 26;
	public int p1num = 0;
	public int p2num = 0;
	public float[] p1cards = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
	public float[] p2cards = {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
	public int top1 = 0;
	public int top2 = 0;
	int over = 0;
	
	float pos1 = 0.0f, pos2 = 0.0f;
	
	int p1score = 0, p2score = 0;
	public Text p1Text, p2Text, winText;
	
	int shuffeled = 0;
	public GameObject shufflebutton;
	public GameObject Drawbutton;
	public GameObject welcomepanel;
	
	
    void Start()
    {
		int x = cardnum-1, temp;
		
		System.Random r = new System.Random();
		
		for(int k = 0; k < 12; k++){
		x = cardnum-1;
			for(int j = 0; j < 13; ){
			temp = cardOrder[j];
			cardOrder[j] = cardOrder[x];
			cardOrder[x] = temp;
			int rInt = r.Next(1, 15); 
			j = j + rInt;
			x = x - rInt;
			}
		}
		
		
		for(int i = 0; i < cardnum/2; i++){
			if(cardOrder[i]<13) p1cards[i] = cardOrder[i];
			else p1cards[i] = cardOrder[i] - 13 + 0.1f;
			p1[i] = Instantiate(card1[cardOrder[i]], new Vector3(0, pos1, 0), Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
			pos1 = pos1 + 0.1f;
		}
		
		for(int i = cardnum/2; i < cardnum; i++){
			if(cardOrder[i]<13) p2cards[i-(cardnum/2)] = cardOrder[i];
			else p2cards[i-(cardnum/2)] = cardOrder[i] - 13 + 0.1f;
			p2[i-(cardnum/2)] = Instantiate(card1[cardOrder[i]], new Vector3(0, pos2, 0), Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
			pos2 = pos2 + 0.1f;
		}
		
		p1num = cardnum/2;
		p2num = cardnum/2;
		
    }

    void Update()
    {
        
    }
	
	public void shuffle(){
			
			if(shuffeled==0){
			for(int i = 0; i < p1num; i++){
				p1[i].transform.Translate(new Vector3(-10, 0, 0));
		}
		
		for(int i = 0; i < p2num; i++){
				p2[i].transform.Translate(new Vector3(10, 0, 0));
		}
		shufflebutton.SetActive(false);
		Drawbutton.SetActive(true);
			}
		
		shuffeled = 1;
		
	}
	
	public void Draw(){
		StartCoroutine(Drawer());
	}
	
	public void StartG(){
		welcomepanel.SetActive(false);
	}
	
	public IEnumerator RotateImage(GameObject myObject)
 {
     float moveSpeed = 15f;
     for(int i = 0; i < 15; i++)
     {
         myObject.transform.rotation = Quaternion.Lerp(myObject.transform.rotation, Quaternion.Euler(0, 0, 0), moveSpeed * Time.deltaTime);
         yield return null;
     }
     yield return null;
 }
	
	
	public IEnumerator Drawer(){
		
		Drawbutton.SetActive(false);
			
		//p1[top1].transform.rotation = Quaternion.Euler(90, 180, 0);
		//p2[top2].transform.rotation = Quaternion.Euler(90, 180, 0);
		p1[top1].transform.position = new Vector3(-10f, p1[top1].transform.position.y + 0.2f*(p1num), 0);
		p2[top2].transform.position = new Vector3(10f, p2[top2].transform.position.y + 0.2f*(p2num), 0);
		StartCoroutine(RotateImage(p1[top1]));
		StartCoroutine(RotateImage(p2[top2]));

		
		
		yield return new WaitForSeconds(2.0f);
		
		if(p1cards[top1]>p2cards[top2]){
			p1[top1].transform.rotation = Quaternion.Euler(0, 0, 180);
			p2[top2].transform.rotation = Quaternion.Euler(0, 0, 180);
			
			p1[top1].transform.position = new Vector3(-10f, pos1, 0);
			pos1 = pos1 + 0.1f;
			p1[(top1+p1num)%cardnum] = p1[top1];
			p1cards[(top1+p1num)%cardnum] = p1cards[top1];
			p1[top1] = null;
			p1cards[top1] = -1;
			top1 = (top1 + 1)% cardnum;
			
			p2[top2].transform.position = new Vector3(-10f, pos1, 0);
			pos1 = pos1 + 0.1f;
			p1[(top1+p1num)%cardnum] = p2[top2];
			p1cards[(top1+p1num)%cardnum] = p2cards[top2];
			p2[top2] = null;
			p2cards[top2] = -1;
			top2 = (top2 + 1)% cardnum;
			p1num = (p1num + 1)% cardnum;;
			p2num = (p2num - 1);	
				
			p1score++;
			p1Text.text = p1score.ToString();
			
			if((p1score - p2score)==(cardnum/2)) {
			winText.text = "Player 1 Wins with a score of " + p1score;
			over = 1;
			}
			
		}
		else{
			
			p1[top1].transform.rotation = Quaternion.Euler(0, 0, 180);
			p2[top2].transform.rotation = Quaternion.Euler(0, 0, 180);
			
			p2[top2].transform.position = new Vector3(10f, pos2, 0);
			pos2 = pos2 + 0.1f;
			p2[(top2+p2num)%cardnum] = p2[top2];
			p2cards[(top2+p2num)%cardnum] = p2cards[top2];
			p2[top2] = null;
			p2cards[top2] = -1;
			top2 = (top2 + 1)% cardnum;
			
			p1[top1].transform.position = new Vector3(10f, pos2, 0);
			pos2 = pos2 + 0.1f;
			p2[(top2+p2num)%cardnum] = p1[top1];
			p2cards[(top2+p2num)%cardnum] = p1cards[top1];
			p1[top1] = null;
			p1cards[top1] = -1;
			top1 = (top1 + 1)% cardnum;
			p2num = (p2num + 1)% cardnum;;
			p1num = (p1num - 1);	
			
			p2score++;
			p2Text.text = p2score.ToString();
			
			if((p2score - p1score)==(cardnum/2)) {
			winText.text = "Player 2 Wins with a score of " + p2score;
			over = 1;
			}
			
		}
		if(over==0) Drawbutton.SetActive(true);
		yield return null;
		
	}
	
	
}
