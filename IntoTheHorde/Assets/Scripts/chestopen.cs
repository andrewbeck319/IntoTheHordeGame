using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestopen : MonoBehaviour
{
	[SerializeField] private Animator Animationcontroller; 
   
   private void OnTriggerEnter(Collider other){
	   if(other.CompareTag("Player")){
		   
	   Animationcontroller.SetBool("open",true);
	   }
   }
   private void OnTriggerExit(Collider other){
	   if(other.CompareTag("Player")){
		   
	   Animationcontroller.SetBool("close",true);
	   }
   }
   
}
