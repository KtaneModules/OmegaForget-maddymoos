using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using KModkit;
using System.Text.RegularExpressions;
using Rnd = UnityEngine.Random;
public class forget : MonoBehaviour {

	public KMAudio Audio;
    public KMBombModule Module;
    public KMBossModule Boss;
    public KMBombInfo Bomb;
    public Transform[] Orbs;
    public TextMesh[] Numbers;
    public Renderer[] ColorChanger;
    public KMSelectable[] Buttons;
    public Material[] Lights;
    public Light[] Lightarray;
    public Color[] Colors;
	private int Stage;
	private int maxStage;
	private bool solved;
	private string[] IgnoreList = {"<PLACEHOLDER>","14"," Bamboozling Time Keeper"," Brainf---"," Forget Enigma"," Forget Everything"," Forget It Not"," Forget Me Not"," Forget Me Later"," Forget Perspective"," Forget The Colors"," Forget Them All"," Forget This"," Forget Us Not"," Iconic"," Organization"," Purgatory"," RPS Judging"," Simon Forgets"," Simon's Stages"," Souvenir"," Tallordered Keys"," The Time Keeper"," The Troll"," The Twin"," The Very Annoying Button"," Timing Is Everything"," Turn The Key"," Ultimate Custom Night","Übermodule"};
	private string Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private string[] _ignore;
    static private int _moduleIdCounter = 1;
	private int _moduleId;
	
	private int[] StageStorage = new int [7];
	
	void Awake() {
		_moduleId = _moduleIdCounter++;
	    string[] ignoredModules = Boss.GetIgnoredModules(Module, _ignore);
		if (ignoredModules != null)
            _ignore = ignoredModules;

	}
	// Use this for initialization
	void Start () {
		if (!Application.isEditor)
            maxStage = Bomb.GetSolvableModuleNames().Where(a => !_ignore.Contains(a)).Count();
        Debug.LogFormat("[<PLACEHOLDER> #{0}]: On this bomb we will go through {1} stages.", _moduleId, maxStage+1);
		PleaseDoRNGThings();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void PleaseDoRNGThings(){
		for(int i=0;i<3;i++)
			StageStorage[i]=Rnd.Range(0,6);
		StageStorage[4]=Rnd.Range(0,14);
		StageStorage[5]=Rnd.Range(0,14);
		StageStorage[3]=Rnd.Range(0,36)*10+Rnd.Range(0,36);
		Numbers[0].text = "";
		Numbers[0].text += Base36[StageStorage[3]/36];
		Numbers[0].text += Base36[StageStorage[3]%36];
		Debug.Log(Numbers[0].text);
		Debug.Log(StageStorage[3]);
		ColorChanger[0].material = Lights[StageStorage[4]];
		ColorChanger[1].material = Lights[StageStorage[5]];
		Lightarray[0].color = Colors[StageStorage[4]];
		Lightarray[1].color = Colors[StageStorage[5]];
		Numbers[1].text = Stage.ToString();
		while(Numbers[1].text.Length!=4)
			Numbers[1].text = "-" + Numbers[1].text;
		StartCoroutine(Godospin());
	}
	IEnumerator Godospin(){
		while(true){
			yield return new WaitForSeconds(1f);
			for(int i=0;i<3;i++){
				if(StageStorage[i]==0){
					//XZ
					float tim=0;while(tim<=.5f){
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					yield return new WaitForSeconds(.02f);tim+=.025f;}}

				else if(StageStorage[i]==1){
					//ZX
					float tim=0;while(tim<=.5f){
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					yield return new WaitForSeconds(.02f);tim+=.025f;}}
				else if(StageStorage[i]==2){
					//XY
					float tim=0;while(tim<=.5f){	
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					yield return new WaitForSeconds(.02f);tim+=.025f;}}
				else if(StageStorage[i]==3){
					//XY
					float tim=0;while(tim<=.5f){	
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
				yield return new WaitForSeconds(.02f);tim+=.025f;}}
				else if(StageStorage[i]==4){
					//ZY
					float tim=0;while(tim<=.5f){	
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
				yield return new WaitForSeconds(.02f);tim+=.025f;}}
				else if(StageStorage[i]==5){
					//YZ
					float tim=0;while(tim<=.5f){	
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3(-.25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3( .25f,0f,-.25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3( -.4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3(  .4f,0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3(-.25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3( .25f,0f, .25f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3( -.4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3(  .4f,0f,  .4f), tim * tim * (3.0f - 2.0f * tim));
				yield return new WaitForSeconds(.02f);tim+=.025f;}}
				
				Orbs[1].localPosition = new Vector3(-.25f,0f, .25f);
				Orbs[2].localPosition = new Vector3( .25f,0f, .25f);
				Orbs[3].localPosition = new Vector3(-.25f,0f,-.25f);
				Orbs[4].localPosition = new Vector3( .25f,0f,-.25f);
				Orbs[5].localPosition = new Vector3( -.4f,0f,  .4f);
				Orbs[6].localPosition = new Vector3(  .4f,0f,  .4f);
				Orbs[7].localPosition = new Vector3( -.4f,0f, -.4f);
				Orbs[8].localPosition = new Vector3(  .4f,0f, -.4f);
				Orbs[0].localEulerAngles = new Vector3(0,0,0);
				}
				if (Stage < Bomb.GetSolvedModuleNames().Where(a => !_ignore.Contains(a)).Count() && !solved)
				{
					Stage++;
					PleaseDoRNGThings();
					yield break;
				}

			}

		}
}