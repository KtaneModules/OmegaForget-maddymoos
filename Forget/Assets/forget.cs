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
    public Material[] BColours;
	public Renderer[] BCChanger;
    private int[] BCTrack = {0,1,2,3,4,5,6,7,8,9};
    public Light[] Lightarray;
    public Color[] Colors;
	private int[] TempGarbage = new int[10];
	private int Stage;
	private int spiniis;
	private string[] Rotations = {"XZ","ZX","XY","YX","ZY","YZ"};
	private string[] CNames = {"Azure","Black","Blue","Cyan","Green","Jade","Lime","Magenta","Orange","Red","Rose","Violet","White","Yellow"};
	private int maxStage;
	private bool solved;
	private bool intro;
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
	void FixedUpdate () {
	}
	
	void PleaseDoRNGThings(){
		if(Stage!=0){
		for(int i=0;i<3;i++)
			StageStorage[i]=Rnd.Range(0,6);
		StageStorage[4]=Rnd.Range(0,14);
		StageStorage[5]=Rnd.Range(0,14);
		StageStorage[3]=Rnd.Range(0,36)*10+Rnd.Range(0,36);
		Numbers[0].text = "";
		Numbers[0].text += Base36[StageStorage[3]/36];
		Numbers[0].text += Base36[StageStorage[3]%36];
		ColorChanger[0].material = Lights[StageStorage[4]];
		ColorChanger[1].material = Lights[StageStorage[5]];
		Lightarray[0].color = Colors[StageStorage[4]];
		Lightarray[1].color = Colors[StageStorage[5]];
		Numbers[1].text = "";
		}
		StartCoroutine(Godospin());
	}
	void StageMath(){
		StageStorage[6] = Int32.Parse(Convert.ToString(StageStorage[3], 8));
		StageStorage[6] = StageStorage[6] % 1000;
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: After converting to Base 8 and moduloing by 1000, the new number is {1}.", _moduleId, StageStorage[6]);
	}
	IEnumerator ColorCycleer(){
		while(true){
			if(intro)
			yield return new WaitForSeconds(.03f);
			else
			yield return new WaitForSeconds(.5f);
			TempGarbage[0]=BCTrack[9];
			TempGarbage[1]=BCTrack[0];
			TempGarbage[2]=BCTrack[1];
			TempGarbage[3]=BCTrack[2];
			TempGarbage[4]=BCTrack[3];
			TempGarbage[5]=BCTrack[4];
			TempGarbage[6]=BCTrack[5];
			TempGarbage[7]=BCTrack[6];
			TempGarbage[8]=BCTrack[7];
			TempGarbage[9]=BCTrack[8];
			for(int i=0;i<10;i++)
			BCTrack[i] = TempGarbage[i];
		for(int i=0;i<10;i++){
			BCChanger[i].material = BColours[BCTrack[i]];
		}
		}
	}
	IEnumerator Godospin(){
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: -----STAGE {1}-----",_moduleId,Stage);
		if(Stage == 0){
				Audio.PlaySoundAtTransform("Startup", Buttons[2].transform);
				intro = true;
				yield return new WaitForSeconds(1f);
				for(int i=0;i<10;i++){
			BCChanger[i].material = BColours[BCTrack[i]];
			}
			StartCoroutine(ColorCycleer());
			Numbers[1].text = "OHNO";
				yield return new WaitForSeconds(3.5f);
				for(int i=0;i<3;i++)
			StageStorage[i]=Rnd.Range(0,6);
		StageStorage[4]=Rnd.Range(0,14);
		StageStorage[5]=Rnd.Range(0,14);
		StageStorage[3]=Rnd.Range(0,36)*10+Rnd.Range(0,36);
		Numbers[0].text = "";
		Numbers[0].text += Base36[StageStorage[3]/36];
		Numbers[0].text += Base36[StageStorage[3]%36];
		ColorChanger[0].material = Lights[StageStorage[4]];
		ColorChanger[1].material = Lights[StageStorage[5]];
		Lightarray[0].color = Colors[StageStorage[4]];
		Lightarray[1].color = Colors[StageStorage[5]];
		Numbers[1].text = "";
		BCTrack = Enumerable.Range(0,10).ToList().Shuffle().ToArray();
		intro = false;
		}
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The number in Base 36 is {1}.", _moduleId, Numbers[0].text);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The rotations are {1}, {2}, and {3}.", _moduleId, Rotations[StageStorage[0]],Rotations[StageStorage[1]],Rotations[StageStorage[2]]);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The LED colors are {1} and {2}.", _moduleId,CNames[StageStorage[4]],CNames[StageStorage[5]]);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: In Base 10, the number displayed is {1}.", _moduleId,StageStorage[3]);
		StageMath();
		while(true){
			Numbers[1].text = Stage.ToString();
			while(Numbers[1].text.Length!=4)
				Numbers[1].text = "-" + Numbers[1].text;
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
					//YX
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
			       Audio.PlaySoundAtTransform("Stage_Generated", Buttons[2].transform);
					Stage++;
					PleaseDoRNGThings();
					yield break;
				}

			}

		}
}