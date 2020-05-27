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
    private int[] BCStore = new int[10];
    private string[] FinalOrder = new string[10];
    private string[] BNames = {"K","B","C","G","M","O","P","R","W","Y"};
    private string[] PStorage;
    public Light[] Lightarray;
    public Color[] Colors;
	private int[] TempGarbage = new int[100];
	private int[] J = new int[3];
	private int[] K = new int[3];
	private int[] L = new int[3];
	private int[] M = new int[3];
	private int[] N = new int[3];
	private int X;
	private int Stage;
	static readonly private int[][] LEDTable = new int[14][]{
	new int[14]{37,18,07,58,24,72,95,01,54,73,88,13,64,83},
	new int[14]{66,48,50,19,41,22,84,78,90,34,03,63,29,14},
	new int[14]{95,23,57,98,36,75,81,42,04,32,07,91,60,11},
	new int[14]{47,86,73,00,16,46,97,59,26,81,77,39,65,92},
	new int[14]{70,24,53,30,27,06,85,44,69,38,76,49,62,99},
	new int[14]{28,63,14,52,90,15,02,87,29,71,45,51,94,37},
	new int[14]{08,33,61,20,22,34,11,89,65,12,67,04,78,91},
	new int[14]{40,82,98,25,95,10,56,69,44,79,96,09,40,31},
	new int[14]{47,03,66,93,35,85,43,91,18,55,78,14,05,60},
	new int[14]{74,95,21,68,02,26,90,42,17,13,80,75,99,53},
	new int[14]{32,17,56,74,91,58,70,92,85,30,64,72,89,13},
	new int[14]{41,93,35,88,11,01,23,65,49,00,43,63,87,12},
	new int[14]{34,71,50,06,39,27,33,92,03,52,77,71,49,10},
	new int[14]{47,18,94,83,62,14,86,09,54,17,89,24,16,08},
	};
	static readonly private int[][] ButtonTable = new int[10][]{
	new int[10]{43,88,59,25,46,07,91,70,63,14},
	new int[10]{31,52,00,94,38,11,27,62,77,83},
	new int[10]{86,35,19,16,32,55,74,80,04,67},
	new int[10]{61,97,72,99,58,47,18,30,78,51},
	new int[10]{02,15,41,40,82,33,65,60,44,08},
	new int[10]{17,68,57,28,22,93,23,24,03,10},
	new int[10]{79,26,64,42,73,39,50,20,87,56},
	new int[10]{49,76,01,53,48,37,92,06,69,29},
	new int[10]{21,36,84,75,34,71,54,85,89,45},
	new int[10]{98,96,05,90,66,95,12,13,81,09},
	};
	private int spiniis;
	private string[] Rotations = {"XZ","ZX","XY","YX","ZY","YZ"};
	private string[] CNames = {"Azure","Black","Blue","Cyan","Green","Jade","Lime","Magenta","Orange","Red","Rose","Violet","White","Yellow"};
	private int maxStage;
	private bool solved;
	private bool intro;
	private string[] IgnoreList = {"<PLACEHOLDER>","14"," Bamboozling Time Keeper"," Brainf---"," Forget Enigma"," Forget Everything"," Forget It Not"," Forget Me Not"," Forget Me Later"," Forget Perspective"," Forget The Colors"," Forget Them All"," Forget This"," Forget Us Not"," Iconic"," Organization"," Purgatory"," RPS Judging"," Simon Forgets"," Simon's Stages"," Souvenir"," Tallordered Keys"," The Time Keeper"," The Troll"," The Twin"," The Very Annoying Button"," Timing Is Everything"," Turn The Key"," Ultimate Custom Night","Übermodule"};
	private string Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ````````````````````````````";
	private string[] _ignore;
    static private int _moduleIdCounter = 1;
	private int _moduleId;
	
	private int[] StageStorage = new int [100];
	
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
		PStorage = new string[maxStage+10];
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
		StageStorage[3]=Rnd.Range(0,36)*36+Rnd.Range(0,36);
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
		StageStorage[9] = Array.IndexOf(Base64.ToArray(),Numbers[0].text[0])*64+Array.IndexOf(Base64.ToArray(),Numbers[0].text[1]);
		StageStorage[6] = Int32.Parse(Convert.ToString(StageStorage[3], 8));
		StageStorage[6] = StageStorage[6] % 1000;
		StageStorage[9] = StageStorage[9] % 1000;
		StageStorage[7] = LEDTable[StageStorage[5]][StageStorage[4]];
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: After converting to Base 8 and moduloing by 1000, the new number is {1}.", _moduleId, StageStorage[6]);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The combined LED value is {1}.", _moduleId, StageStorage[7]);
		switch (Stage%5){
			case 0:StageStorage[8] = StageStorage[6] - StageStorage[7]; break;
			case 1:StageStorage[8] = 2*StageStorage[7] + 7; break;
			case 2:StageStorage[8] = (StageStorage[7]+(StageStorage[7]%2)+(StageStorage[6]+(StageStorage[6]%2)))/2; break;
			case 3:StageStorage[8] = (3*StageStorage[6])-(2*StageStorage[7])-42; break;
			case 4:StageStorage[8] = 75-StageStorage[7]+2*(StageStorage[6]-StageStorage[7]); break;
		}
		StageStorage[8] = Mod(StageStorage[8], 100);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The E value is {1}.", _moduleId, StageStorage[8]);
		X = StageStorage[6];
		int D = StageStorage[9];
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The D value is {1}.", _moduleId, D);
		int I = StageStorage[6];
		int E = StageStorage[8];
		for(int n=1;n<4;n++){
			if((Stage%5)==0){
				switch(StageStorage[n-1]){
					case 0: X=X+I+E; break;
					case 1: X=E-X; break;
					case 2: X=X+2*E; break;
					case 3: X=I-(99-E)+X; break;
					case 4: X=(X-Mod(X,2))/2+D; break;
					case 5: X=999-2*X; break;
				}
				X=Mod(X,1000);
				J[n-1] = X;
			}
			else if((Stage%5)==1){
				switch(StageStorage[n-1]){
					case 0: X=X-I-J[n-1]; break;
					case 1: X=999-X-J[0]; break;
					case 2: X=J[n-1]+D-X; break;
					case 3: X=E+D+X-J[1]; break;
					case 4: X=2*D-X+J[n-1]; break;
					case 5: X=J[2]-X; break;
				}
				X=Mod(X,1000);
				K[n-1] = X;
			}
			else if((Stage%5)==2){
				switch(StageStorage[n-1]){
					case 0: X=n*X-K[0]; break;
					case 1: X=D-(X+E)+K[0]; break;
					case 2: X=X+J[n-1]+K[n-1]; break;
					case 3: X=X*(Mod(J[n-1],6)+1); break;
					case 4: X=3*D-K[n-1]+X; break;
					case 5: X=J[2]+K[2]-X; break;
				}
				X=Mod(X,1000);
				L[n-1] = X;
			}
			else if((Stage%5)==3){
				switch(StageStorage[n-1]){
					case 0: X=3*X-4*D+5*n; break;
					case 1: X=K[n-1]+(X+Mod(X,2))/2; break;
					case 2: X=L[n-1]-K[n-1]-J[n-1]+X; break;
					case 3: X=L[1]-X*(Mod(I,4)+1); break;
					case 4: X=n-L[2]-X+D; break;
					case 5: X=n*(X+E-D); break;
				}
				X=Mod(X,1000);
				M[n-1] = X;
			}
			else if((Stage%5)==4){
				switch(StageStorage[n-1]){
					case 0: X=999-4*X-9*n+M[2]; break;
					case 1: X=I-2*n+K[0]+(X-5*n); break;
					case 2: X=X-M[n-1]+L[n-1]-K[n-1]+J[n-1]; break;
					case 3: X=J[2]+15*n-(X-Mod(X,2))/2; break;
					case 4: X=5*X-10*n+3*D-E; break;
					case 5: X=333-L[1]+X-E; break;
				}
				X=Mod(X,1000);
				N[n-1] = X;
			}
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: X is now {1}.", _moduleId, X);
		}
		StageStorage[10] = Mod(X,100);
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The number being used for the button is {1}.", _moduleId, StageStorage[10]);
		StageStorage[11] = ButtonTable[StageStorage[10]%10][StageStorage[10]/10];
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The value from the table is {1}.", _moduleId, StageStorage[11]);
		PStorage[Stage] = FinalOrder[StageStorage[11]/10] + StageStorage[11]%10;
		Debug.LogFormat("[<PLACEHOLDER> #{0}]: The correct input for stage {1} is {2}.", _moduleId, Stage,PStorage[Stage]);

	}
	void ButtonReorder(){
		string[] Temp = new string[10];
		for(int i=0;i<10;i++)
			Temp[i] = BNames[i];
		if(FinalOrder[(Array.IndexOf(FinalOrder,"W")+1)%10]=="K"||FinalOrder[(Array.IndexOf(FinalOrder,"W")+9)%10]=="K")
			Temp = Temp.Reverse().Select(x => x.ToString()).ToArray();
		if(FinalOrder[(Array.IndexOf(FinalOrder,"R")+5)%10]=="C"){
			Temp = Temp.Select(x => x.Replace("R", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("C", "R")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "C")).ToArray();
		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"G")+5)%10]=="M"){
			Temp = Temp.Select(x => x.Replace("G", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("M", "G")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "M")).ToArray();
		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"B")+5)%10]==""){
			Temp = Temp.Select(x => x.Replace("B", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("Y", "B")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "Y")).ToArray();
		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"O")+2)%10]=="P"||FinalOrder[(Array.IndexOf(FinalOrder,"O")+8)%10]=="P"){
			Temp = Temp.Select(x => x.Replace(FinalOrder[0], "-")).ToArray();
			Temp = Temp.Select(x => x.Replace(FinalOrder[9], FinalOrder[0])).ToArray();
			Temp = Temp.Select(x => x.Replace("-", FinalOrder[9])).ToArray();

		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"R")+1)%10]=="B"||FinalOrder[(Array.IndexOf(FinalOrder,"R")+7)%10]=="B"){
			string[] shifter = new string[12];
		for(int i=0;i<10;i++)
			shifter[i+2]=Temp[i];
		for(int i=0;i<10;i++)
			Temp[(i+2)%10]=shifter[i+2];
		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"W")+3)%10]=="K"||FinalOrder[(Array.IndexOf(FinalOrder,"W")+7)%10]=="K"){
			Temp = Temp.Select(x => x.Replace(FinalOrder[1], "-")).ToArray();
			Temp = Temp.Select(x => x.Replace(FinalOrder[8], FinalOrder[1])).ToArray();
			Temp = Temp.Select(x => x.Replace("-", FinalOrder[8])).ToArray();

		}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"Y")+1)%10]=="G"||FinalOrder[(Array.IndexOf(FinalOrder,"Y")+9)%10]=="G"){
			string[] shifotr = new string[13];
		for(int i=0;i<10;i++)
			shifotr[i+3]=Temp[i];
		for(int i=0;i<10;i++)
			Temp[(i+7)%10]=shifotr[i+3];
		}
		for(int i=0;i<10;i++){
		FinalOrder[i] = Temp[i];
		}
		Debug.Log(FinalOrder.Join(""));

	}
	IEnumerator ColorCycleer(){
		while(true){
			if(intro)
			yield return new WaitForSeconds(.03f);
			else
			yield return new WaitForSeconds(.5f);
			//todo this is dumb
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
		for(int i=0;i<10;i++){
		BCStore[i]=BCTrack[i];
		FinalOrder[i]=BNames[BCStore[i]];
		}
		ButtonReorder();
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
		private int Mod(int num, int mod)
    {
        while (true)
        {
            //modulation for negatives
            if (num < 0)
            {
                num += mod;
                continue;
            }

            //modulation for positives
            else if (num >= mod)
            {
                num -= mod;
                continue;
            }

            //once it reaches here, we know it's modulated and we can return it
            return num;
        }
    }
}