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
    public AudioSource NerfSolve;
    public AudioClip NerfSound;
    public Transform[] Orbs;
    public TextMesh[] Numbers;
    public TextMesh[] RecoveryNumbers;
    public Renderer[] ColorChanger;
    public KMSelectable[] Buttons;
    public Material[] Lights;
    public Material[] BColours;
    public Renderer[] BCChanger;
    private int[] BCTrack = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int[] BCStore = new int[10];
    private string[] FinalOrder = new string[12];
    private string[] BNames = { "K", "B", "C", "G", "M", "O", "P", "R", "W", "Y" };
    private string[] PStorage;
    private string[] AStorage;
    private int CycleHelper = 1;
    private int SubSegment;
    private int Inputnum;
    private int ASTracker;
    int[] RecoverRotations = new int[1000000];
    int[] RecoverCruelRotations = new int[1000000];
    int[] RecoverMoreRotations = new int[1000000];
    int[] RecoverLights = new int[1000000];
    int[] RecoverOtherLights = new int[1000000];
    string[] RecoverDoubleDigit = new string[1000000];
    string PLEASEHELPME;
    int RecoverStages = 12048546;
    bool RecoveryModeActive;
    int StageBeingRecovered = 8341753;
    public Light[] Lightarray;
    public Color[] Colors;
    bool GoodOne;
    bool StopTheMusic;
    bool FlashOrNot;
    bool flashblack;
    private int[] TempGarbage = new int[100];
    private int[] J = new int[4];
    private int[] K = new int[4];
    private int[] L = new int[4];
    private int[] M = new int[4];
    private int[] N = new int[4];
    private int X;
    private int Y;
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
    new int[14]{34,71,50,06,39,27,33,92,03,52,77,77,49,10},
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
    private string[] Rotations = { "XZ", "ZX", "XY", "YX", "ZY", "YZ" };
    private string[] CNames = { "Azure", "Black", "Blue", "Cyan", "Green", "Jade", "Lime", "Magenta", "Orange", "Red", "Rose", "Violet", "White", "Yellow" };
    private int maxStage;
    private bool solved;
    private bool intro;
    private bool submission = false;
    private bool Checking = false;
    private string[] ignoredModules = { "AlphaForget", "OmegaForget", "14", "501", "42", "Bamboozling Time Keeper", "Brainf---", "Busy Beaver", "Forget Enigma", "Forget Everything", "Forget It Not", "Forget Me Not", "Forget Me Later", " Forget Perspective", "Forget The Colors", "Forget Them All", "Forget This", "Forget Us Not", "Iconic", "Organization", "Purgatory", "RPS Judging", "Simon Forgets", "Simon's Stages", "Souvenir", "Tallordered Keys", "The Time Keeper", "The Troll", "The Twin", "The Very Annoying Button", "Timing Is Everything", "Turn The Key", "Ultimate Custom Night", "Übermodule" };
    private string Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string Base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ``````````````````````````0123456789+-";
    static private int _moduleIdCounter = 1;
    private int _moduleId;
    bool CruelActivation;
    bool MistakesWereMade;

    private int[] StageStorage = new int[100];

    private OmegaSettings Settings;
    sealed class OmegaSettings
    {
        public bool TRUEOMEGAFORGET = false;
        public bool EveryStageSounds = false;
    }
    bool Autosolving;

    void Awake()
    {
        _moduleId = _moduleIdCounter++;
        string[] ingore = Boss.GetIgnoredModules(Module, ignoredModules);
        if (ingore != null)
            ignoredModules = ingore;

        for (byte i = 0; i < Buttons.Length; i++)
        {
            KMSelectable btn = Buttons[i];
            btn.OnInteract += delegate
            {
                HandlePress(btn);
                return false;
            };
        }
        var modConfig = new ModConfig<OmegaSettings>("OmegaForget");
        Settings = modConfig.Settings;
        modConfig.Settings = Settings;
        for (int i = 0; i < 10; i++)
            RecoveryNumbers[i].text = "";
    }
    // Use this for initialization
    void Start() {
        if (Application.isEditor)
        {
            maxStage = 21;
            StartCoroutine(LiterallyJustForTesting());
        }
        else
            maxStage = Bomb.GetSolvableModuleNames().Where(a => !ignoredModules.Contains(a)).Count();
        if (maxStage == 0) {
            Debug.LogFormat("[OmegaForget #{0}]: No available modules. Autosolving.", _moduleId);
            ColorChanger[0].material = Lights[4];
            ColorChanger[1].material = Lights[4];
            Lightarray[0].color = Colors[4];
            Lightarray[1].color = Colors[4];
            Numbers[1].text = "NVMD";
            solved = true;
            Module.HandlePass();
        }
        else {
            PStorage = new string[maxStage];
            AStorage = new string[maxStage];
            Debug.LogFormat("[OmegaForget #{0}]: On this bomb we will go through {1} stages.", _moduleId, maxStage);
            PleaseDoRNGThings();
        }
    }
    void HandlePress(KMSelectable btn) {
        int aly = Array.IndexOf(Buttons, btn);
        Buttons[aly].AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Buttons[aly].transform);
        if (RecoveryModeActive)
        {
            if (((SubSegment * 10 + aly) == StageBeingRecovered))
            {
                RecoveryModeActive = false;
                for (int i = 0; i < 10; i++)
                    RecoveryNumbers[i].text = "";
                FlashOrNot = false;
                MistakesWereMade = false;
                Checking = false;
                Numbers[0].text = "~~";
                Numbers[1].text = "INPT";
                ColorChanger[0].material = Lights[1];
                ColorChanger[1].material = Lights[1];
                Lightarray[0].color = Colors[1];
                Lightarray[1].color = Colors[1];
                StageBeingRecovered = 135876;
                Inputnum = 0;
            }
            else if  ((SubSegment * 10 + aly) < Stage)
            {
                StageBeingRecovered = SubSegment * 10 + aly;
                if (RecoverStages > 10000)
                    StartCoroutine(RotationRecovery());
            }
        }
        else if (!GoodOne)
        {
            if (solved || Checking)
            {
                return;
            }
            if (!submission)
            {
                Module.HandleStrike();
                return;
            }
            AStorage[SubSegment * 10 + Inputnum] = BNames[BCTrack[aly]] + aly.ToString();
            Inputnum++;
            if (SubSegment * 10 + Inputnum == maxStage)
            {
                FlashOrNot = true;
                Checking = true;
                StartCoroutine(FinalCheck());
                Numbers[0].text = "~~";
            }
            else if (Inputnum == 10 && !Autosolving)
            {
                FlashOrNot = true;
                Checking = true;
                StartCoroutine(Check());
                Numbers[0].text = "~~";
            }
            else
            {
                Numbers[0].text = Mod(SubSegment * 10 + Inputnum, 100).ToString();
                if (Numbers[0].text.Length == 1)
                    Numbers[0].text = "-" + Numbers[0].text;
            }
        }
        else if (StopTheMusic)
        {
            NerfSolve.Stop();
            StopTheMusic = false;
        }
            
    }
    void SubmissionMode() {
        for (int i = 0; i < 12; i++)
            StageStorage[i] = 0;
        submission = true;
            Numbers[0].text = "~~";
            Numbers[1].text = "INPT";
            ColorChanger[0].material = Lights[1];
            ColorChanger[1].material = Lights[1];
            Lightarray[0].color = Colors[1];
            Lightarray[1].color = Colors[1];
            Debug.LogFormat("[OmegaForget #{0}]: The full answer is {1}.", _moduleId, PStorage.Join(", "));
    }

    void PleaseDoRNGThings() {
        if (Stage != 0) {
            for (int i = 0; i < 3; i++)
                StageStorage[i] = Rnd.Range(0, 6);
            RecoverRotations[Stage] = StageStorage[0];
            if (Settings.TRUEOMEGAFORGET)
            {
                RecoverCruelRotations[Stage] = StageStorage[1];
                RecoverMoreRotations[Stage] = StageStorage[2];
            }
            StageStorage[4] = Rnd.Range(0, 14);
            StageStorage[5] = Rnd.Range(0, 14);
            StageStorage[3] = Rnd.Range(0, 36) * 36 + Rnd.Range(0, 36);
            Numbers[0].text = "";
            Numbers[0].text += Base36[StageStorage[3] / 36];
            Numbers[0].text += Base36[StageStorage[3] % 36];
            RecoverDoubleDigit[Stage] = Base36[StageStorage[3] / 36].ToString() + Base36[StageStorage[3] % 36].ToString();
            ColorChanger[0].material = Lights[StageStorage[4]];
            RecoverLights[Stage] = StageStorage[4];
            ColorChanger[1].material = Lights[StageStorage[5]];
            RecoverOtherLights[Stage] = StageStorage[5];
            Lightarray[0].color = Colors[StageStorage[4]];
            Lightarray[1].color = Colors[StageStorage[5]];
            Numbers[1].text = "";
        }
        StartCoroutine(Godospin());
    }
    void StageMath() {
        StageStorage[9] = Array.IndexOf(Base64.ToArray(), Numbers[0].text[0]) * 64 + Array.IndexOf(Base64.ToArray(), Numbers[0].text[1]);
        StageStorage[6] = Int32.Parse(Convert.ToString(StageStorage[3], 8));
        StageStorage[6] = StageStorage[6] % 1000;
        StageStorage[9] = StageStorage[9] % 1000;
        StageStorage[7] = LEDTable[StageStorage[5]][StageStorage[4]];
        Debug.LogFormat("[OmegaForget #{0}]: After converting to Base 8, modulo 1000, the new number is {1}.", _moduleId, StageStorage[6]);
        Debug.LogFormat("[OmegaForget #{0}]: The combined LED value is {1}.", _moduleId, StageStorage[7]);
            switch (Stage % 5)
            {
                case 0: StageStorage[8] = StageStorage[6] - StageStorage[7]; break;
                case 1: StageStorage[8] = 2 * StageStorage[7] + 7; break;
                case 2: StageStorage[8] = (StageStorage[7] + (StageStorage[7] % 2) + (StageStorage[6] + (StageStorage[6] % 2))) / 2; break;
                case 3: StageStorage[8] = (3 * StageStorage[6]) - (2 * StageStorage[7]) - 42; break;
                case 4: StageStorage[8] = 75 - StageStorage[7] + 2 * StageStorage[6]; break;
            }
        StageStorage[8] = Mod(StageStorage[8], 100);
        Debug.LogFormat("[OmegaForget #{0}]: The E value is {1}.", _moduleId, StageStorage[8]);
        int D = StageStorage[9];
        Debug.LogFormat("[OmegaForget #{0}]: The D value is {1}.", _moduleId, D);
        int I = StageStorage[6];
        int E = StageStorage[8];
        X = I;
        for (int n = 1; n < 4; n++)
        {
            if (!Settings.TRUEOMEGAFORGET)
            {
                if ((Stage % 5) == 0)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = 2 * I + E; break;
                        case 1: X = E - I; break;
                        case 2: X = I + 2 * E; break;
                        case 3: Y = 99 - E; X = I - Y; break;
                        case 4: Y = I % 2; if (Y == -1) Y = 1; Y = I - Y; X = Y / 2 + D; break;
                        case 5: X = 999 - 2 * I; break;
                    }
                    X = X % 1000;
                    J[1] = X;
                }
                else if ((Stage % 5) == 1)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = I - J[1] + D; break;
                        case 1: X = 999 - I - J[1]; break;
                        case 2: X = J[1] + D - I; break;
                        case 3: X = E + D + I - J[1]; break;
                        case 4: X = 2 * D - I + J[1]; break;
                        case 5: X = J[1] - I; break;
                    }
                    X = X % 1000;
                    K[1] = X;
                }
                else if ((Stage % 5) == 2)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = I - K[1]; break;
                        case 1: Y = I + E; X = D - Y + K[1]; break;
                        case 2: X = I + J[1] + K[1]; break;
                        case 3: Y = J[1] % 6 + 1; if (Y < 1) Y = Y + 6; X = I * Y; break;
                        case 4: X = 3 * D - K[1] + I; break;
                        case 5: X = J[1] + K[1] - I; break;
                    }
                    X = X % 1000;
                    L[1] = X;
                }
                else if ((Stage % 5) == 3)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = 3 * I - 4 * D; break;
                        case 1: Y = I % 2; if (Y == -1) Y = 1; Y = I + Y; X = K[1] + Y / 2; break;
                        case 2: X = L[1] - K[1] - J[1] + I; break;
                        case 3: Y = I % 4 + 1; if (Y < 1) Y = Y + 4; X = L[1] - I * Y; break;
                        case 4: X = (-1) * L[1] - I + D; break;
                        case 5: X = I + E - D; break;
                    }
                    X = X % 1000;
                    M[1] = X;
                }
                else if ((Stage % 5) == 4)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = 999 - 4 * I + M[1]; break;
                        case 1: X = I + K[1] - M[1]; break;
                        case 2: X = X - M[1] + L[1] - K[1] + J[1]; break;
                        case 3: Y = I % 2; if (Y == -1) Y = 1; Y = I - Y; X = J[1] + 15 - Y / 2; break;
                        case 4: X = 5 * I - L[1] + 3 * D - E; break;
                        case 5: X = 333 - L[1] + I - E; break;
                    }
                    X = X % 1000;
                    N[1] = X;
                }
                n = 4;
                Debug.LogFormat("[OmegaForget #{0}]: X is {1}.", _moduleId, X);
            }
            else
            {
                if ((Stage % 5) == 0)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = X + I + E; break; //XZ
                        case 1: X = E - X; break; //ZX
                        case 2: X = X + 2 * E; break; //XY
                        case 3: Y = 99 - E; X = I - Y + X; break; //YX
                        case 4: Y = X % 2; if (Y == -1) Y = 1; Y = X - Y; X = Y / 2 + D; break; //ZY
                        case 5: X = 999 - 2 * X; break; //YZ
                    }
                    X = X % 1000;
                    J[n] = X;
                }
                else if ((Stage % 5) == 1)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = X - I - J[n]; break;
                        case 1: X = 999 - X - J[1]; break;
                        case 2: X = J[n] + D - X; break;
                        case 3: X = E + D + X - J[2]; break;
                        case 4: X = 2 * D - X + J[n]; break;
                        case 5: X = J[3] - X; break;
                    }
                    X = X % 1000;
                    K[n] = X;
                }
                else if ((Stage % 5) == 2)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = n * X - K[1]; break;
                        case 1: Y = X + E; X = D - Y + K[1]; break;
                        case 2: X = X + J[n] + K[n]; break;
                        case 3: Y = J[n] % 6 + 1; if (Y < 1) Y = Y + 6; X = X * Y; break;
                        case 4: X = 3 * D - K[n] + X; break;
                        case 5: X = J[3] + K[3] - X; break;
                    }
                    X = X % 1000;
                    L[n] = X;
                }
                else if ((Stage % 5) == 3)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = 3 * X - 4 * D + 5 * n; break;
                        case 1: Y = X % 2; if (Y == -1) Y = 1; Y = X + Y; X = K[n] + Y / 2; break;
                        case 2: X = L[n] - K[n] - J[n] + X; break;
                        case 3: Y = I % 4 + 1; if (Y < 1) Y = Y + 4; X = L[2] - X * Y; break;
                        case 4: X = n - L[3] - X + D; break;
                        case 5: Y = X + E - D; X = n * Y; break;
                    }
                    X = X % 1000;
                    M[n] = X;
                }
                else if ((Stage % 5) == 4)
                {
                    switch (StageStorage[n - 1])
                    {
                        case 0: X = 999 - 4 * X - 9 * n + M[3]; break;
                        case 1: Y = X - 5 * n;  X = I - 2 * n - K[1] + Y; break;
                        case 2: X = X - M[n] + L[n] - K[n] + J[n]; break;
                        case 3: Y = X % 2; if (Y == -1) Y = 1;  Y = X - Y; X = J[3] + 15 * n - Y / 2; break;
                        case 4: X = 5 * X - 10 * n + 3 * D - E; break;
                        case 5: X = 333 - L[2] + X - E; break;
                    }
                    X = X % 1000;
                    N[n] = X;
                }
                Debug.LogFormat("[OmegaForget #{0}]: X for calculation #{2} is {1}", _moduleId, X, n);
            }
        }
		StageStorage[10] = Mod(X,100);
		Debug.LogFormat("[OmegaForget #{0}]: The number being used for the button is {1}.", _moduleId, StageStorage[10]);
		StageStorage[11] = ButtonTable[StageStorage[10]%10][StageStorage[10]/10];
		Debug.LogFormat("[OmegaForget #{0}]: The value from the table is {1}.", _moduleId, StageStorage[11]);
		PStorage[Stage] = FinalOrder[StageStorage[11]/10] + StageStorage[11]%10;
		Debug.LogFormat("[OmegaForget #{0}]: The correct input for stage {1} is {2}.", _moduleId, Stage,PStorage[Stage]);

	}
    IEnumerator LiterallyJustForTesting()
    {
        while (Stage < (maxStage - 1))
        {
            yield return new WaitForSeconds(1f);
            Stage++;
            PleaseDoRNGThings();
        }
        yield return new WaitForSeconds(1f);
        SubmissionMode();
    }
	void ButtonReorder(){
        Debug.LogFormat("[OmegaForget #{0}] The order of the buttons on the module is {1}{2}{3}{4}{5}{6}{7}{8}{9}{10}.", _moduleId, FinalOrder[0], FinalOrder[1], FinalOrder[2], FinalOrder[3], FinalOrder[4], FinalOrder[5], FinalOrder[6], FinalOrder[7], FinalOrder[8], FinalOrder[9]);
        if (FinalOrder[0] == "W")
            flashblack = true;
        string[] Temp = new string[10];
		for(int i=0;i<10;i++)
			Temp[i] = BNames[i];
		if(FinalOrder[(Array.IndexOf(FinalOrder,"W")+1)%10]=="K"||FinalOrder[(Array.IndexOf(FinalOrder,"W")+9)%10]=="K"){
			Temp = Temp.Reverse().Select(x => x.ToString()).ToArray();
			Debug.LogFormat("[OmegaForget #{0}] Swap rule 1 applied. Reversing string.", _moduleId);
	}
		if(FinalOrder[(Array.IndexOf(FinalOrder,"R")+5)%10]=="C"){
			Temp = Temp.Select(x => x.Replace("R", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("C", "R")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "C")).ToArray();
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 2 applied. Swapping R and C.", _moduleId);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"G")+5)%10]=="M"){
			Temp = Temp.Select(x => x.Replace("G", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("M", "G")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "M")).ToArray();
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 3 applied. Swapping G and M", _moduleId);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"B")+5)%10]=="Y"){
			Temp = Temp.Select(x => x.Replace("B", "-")).ToArray();
			Temp = Temp.Select(x => x.Replace("Y", "B")).ToArray();
			Temp = Temp.Select(x => x.Replace("-", "Y")).ToArray();
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 4 applied. Swapping B and Y.", _moduleId);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"O")+2)%10]=="P"||FinalOrder[(Array.IndexOf(FinalOrder,"O")+8)%10]=="P"){
			Temp = Temp.Select(x => x.Replace(FinalOrder[0], "-")).ToArray();
			Temp = Temp.Select(x => x.Replace(FinalOrder[9], FinalOrder[0])).ToArray();
			Temp = Temp.Select(x => x.Replace("-", FinalOrder[9])).ToArray();
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 5 applied. Swapping the first and tenth positions on the buttons ({1} and {2}).", _moduleId, FinalOrder[0], FinalOrder[9]);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"R")+1)%10]=="B"||FinalOrder[(Array.IndexOf(FinalOrder,"R")+9)%10]=="B"){
			string[] shifter = new string[12];
		for(int i=0;i<10;i++)
			shifter[i+2]=Temp[i];
		for(int i=0;i<10;i++)
			Temp[(i+2)%10]=shifter[i+2];
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 6 applied. Shifting to the right twice.", _moduleId);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"W")+3)%10]=="K"||FinalOrder[(Array.IndexOf(FinalOrder,"W")+7)%10]=="K"){
			Temp = Temp.Select(x => x.Replace(FinalOrder[1], "-")).ToArray();
			Temp = Temp.Select(x => x.Replace(FinalOrder[8], FinalOrder[1])).ToArray();
			Temp = Temp.Select(x => x.Replace("-", FinalOrder[8])).ToArray();
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 7 applied. Swapping the second and ninth positions on the buttons ({1} and {2}).", _moduleId, FinalOrder[1], FinalOrder[8]);
        }
		if(FinalOrder[(Array.IndexOf(FinalOrder,"Y")+1)%10]=="G"||FinalOrder[(Array.IndexOf(FinalOrder,"Y")+9)%10]=="G"){
			string[] shifotr = new string[13];
		for(int i=0;i<10;i++)
			shifotr[i+3]=Temp[i];
		for(int i=0;i<10;i++)
			Temp[(i+7)%10]=shifotr[i+3];
            Debug.LogFormat("[OmegaForget #{0}] Swap rule 8 applied. Shifting to the left thrice.", _moduleId);
        }
        PLEASEHELPME = FinalOrder[0];
		for(int i=0;i<10;i++)
		FinalOrder[i] = Temp[i];
        Debug.LogFormat("[OmegaForget #{0}] Final sequence is {1}{2}{3}{4}{5}{6}{7}{8}{9}{10}.", _moduleId, FinalOrder[0], FinalOrder[1], FinalOrder[2], FinalOrder[3], FinalOrder[4], FinalOrder[5], FinalOrder[6], FinalOrder[7], FinalOrder[8], FinalOrder[9]);


    }
    IEnumerator JesusChristWhy()
    {
        CruelActivation = true;
        yield return new WaitForSeconds(6f);
        CruelActivation = false;
        Numbers[1].text = Stage.ToString();
        while (Numbers[1].text.Length != 4)
            Numbers[1].text = "-" + Numbers[1].text;
    }
    IEnumerator YOUMADEAMISTAKE()
    {
        MistakesWereMade = true;
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "--OH";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "SHIT";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "THIS";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "--IS";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "-NOT";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "GOOD";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "--AT";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "-ALL";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "WHAT";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "HAVE";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "-YOU";
        yield return new WaitForSeconds(0.2f);
        Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
        Numbers[1].text = "DONE";
        yield return new WaitForSeconds(0.2f);
        MistakesWereMade = false;
        Settings.TRUEOMEGAFORGET = true;
        PleaseDoRNGThings();
    }

    IEnumerator RotationRecovery()
    {
        while (true)
        {
            if (!RecoveryModeActive)
            {
                RecoverStages = 3895476;
                break;
            }
            RecoverStages = StageBeingRecovered;
            Numbers[1].text = RecoverStages.ToString();
            while (Numbers[1].text.Length != 4)
                Numbers[1].text = "-" + Numbers[1].text;
            ColorChanger[0].material = Lights[RecoverLights[RecoverStages]];
            ColorChanger[1].material = Lights[RecoverOtherLights[RecoverStages]];
            Lightarray[0].color = Colors[RecoverLights[RecoverStages]];
            Lightarray[1].color = Colors[RecoverOtherLights[RecoverStages]];
            Numbers[0].text = RecoverDoubleDigit[RecoverStages];
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 3; i++)
            {
                if ((RecoverRotations[RecoverStages] == 0 && i == 0) || (RecoverCruelRotations[RecoverStages] == 0 && i == 1) || (RecoverMoreRotations[RecoverStages] == 0 && i == 2))
                {
                    //XZ
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }

                else if ((RecoverRotations[RecoverStages] == 1 && i == 0) || (RecoverCruelRotations[RecoverStages] == 1 && i == 1) || (RecoverMoreRotations[RecoverStages] == 1 && i == 2))
                {
                    //ZX
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }
                else if ((RecoverRotations[RecoverStages] == 2 && i == 0) || (RecoverCruelRotations[RecoverStages] == 2 && i == 1) || (RecoverMoreRotations[RecoverStages] == 2 && i == 2))
                {
                    //XY
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }
                else if ((RecoverRotations[RecoverStages] == 3 && i == 0) || (RecoverCruelRotations[RecoverStages] == 3 && i == 1) || (RecoverMoreRotations[RecoverStages] == 3 && i == 2))
                {
                    //YX
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }
                else if ((RecoverRotations[RecoverStages] == 4 && i == 0) || (RecoverCruelRotations[RecoverStages] == 4 && i == 1) || (RecoverMoreRotations[RecoverStages] == 4 && i == 2))
                {
                    //ZY
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }
                else if ((RecoverRotations[RecoverStages] == 5 && i == 0) || (RecoverCruelRotations[RecoverStages] == 5 && i == 1) || (RecoverMoreRotations[RecoverStages] == 5 && i == 2))
                {
                    //YZ
                    float tim = 0; while (tim <= .5f)
                    {
                        Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition, new Vector3(-.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition, new Vector3(.25f, 0f, -.25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition, new Vector3(-.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition, new Vector3(.4f, 0f, -.4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition, new Vector3(-.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition, new Vector3(.25f, 0f, .25f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition, new Vector3(-.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition, new Vector3(.4f, 0f, .4f), tim * tim * (3.0f - 2.0f * tim));
                        yield return new WaitForSeconds(.02f); tim += .025f;
                    }
                }

                Orbs[1].localPosition = new Vector3(-.25f, 0f, .25f);
                Orbs[2].localPosition = new Vector3(.25f, 0f, .25f);
                Orbs[3].localPosition = new Vector3(-.25f, 0f, -.25f);
                Orbs[4].localPosition = new Vector3(.25f, 0f, -.25f);
                Orbs[5].localPosition = new Vector3(-.4f, 0f, .4f);
                Orbs[6].localPosition = new Vector3(.4f, 0f, .4f);
                Orbs[7].localPosition = new Vector3(-.4f, 0f, -.4f);
                Orbs[8].localPosition = new Vector3(.4f, 0f, -.4f);
                Orbs[0].localEulerAngles = new Vector3(0, 0, 0);
                if (!Settings.TRUEOMEGAFORGET)
                    i = 3;
            }
        }
    }
	IEnumerator ColorCycleer(){
		while(true){
            if (intro)
                yield return new WaitForSeconds(.03f);
            else
            {
                if ((CycleHelper >= 30) && (BNames[BCTrack[0]] == PLEASEHELPME))
                {
                    CycleHelper = CycleHelper - 30;
                        for (int i = 0; i < 2; i++)
                        {
                            if (flashblack)
                                BCChanger[0].material = BColours[0];
                            else
                                BCChanger[0].material = BColours[8];
                            yield return new WaitForSeconds(0.125f);
                            BCChanger[0].material = BColours[BCTrack[0]];
                            yield return new WaitForSeconds(0.125f);
                        }
                }
                else
                    yield return new WaitForSeconds(.5f);
            }
			if(!Checking){
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
                if (!FlashOrNot && !intro)
                    CycleHelper++;
                for (int i=0;i<10;i++)
			BCTrack[i] = TempGarbage[i];
		for(int i=0;i<10;i++){
			BCChanger[i].material = BColours[BCTrack[i]];
		}
		}
            if (MistakesWereMade)
            yield break;
		}
	}
	IEnumerator SolveAnimation(){
		StartCoroutine(SolveLights());
			Audio.PlaySoundAtTransform("Module_Solved", Numbers[0].transform);
		yield return new WaitForSeconds(19f);
		float tom=0;while(tom<=.5f){
					Orbs[1].localPosition = Vector3.Lerp(Orbs[1].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[2].localPosition = Vector3.Lerp(Orbs[2].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[3].localPosition = Vector3.Lerp(Orbs[3].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[4].localPosition = Vector3.Lerp(Orbs[4].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[5].localPosition = Vector3.Lerp(Orbs[5].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[6].localPosition = Vector3.Lerp(Orbs[6].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[7].localPosition = Vector3.Lerp(Orbs[7].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					Orbs[8].localPosition = Vector3.Lerp(Orbs[8].localPosition,new Vector3(0f,0f,0f), tom * tom * (3.0f - 2.0f * tom));
					yield return new WaitForSeconds(.02f);tom+=.1f;}
					ColorChanger[0].material = Lights[4];
			ColorChanger[1].material = Lights[4];
			Lightarray[0].color = Colors[4];
			Lightarray[1].color = Colors[4];
			Numbers[0].text="GG";
			Numbers[1].text="hype";
			for(int i=0;i<10;i++){
			BCChanger[i].material = BColours[3]; 
			}
        Module.HandlePass();
			solved = true;
	}
	IEnumerator FinalCheck(){
		Audio.PlaySoundAtTransform("Reveal_"+Rnd.Range(1,4), Numbers[0].transform);
		bool[] aaaaa = {false,false,false,false,false,false,false,false,false,false};
		bool E = true;
		for(int i=0;i<10;i++)
			BCChanger[i].material = BColours[0];
        if (Autosolving)
            while (Inputnum > 10)
                Inputnum = Inputnum - 10;
		for(int i=0;i<Inputnum;i++){
			if (PStorage[SubSegment*10+i] != AStorage[SubSegment*10+i])
				aaaaa[i] = false;
			else aaaaa[i] = true;
			BCChanger[i].material = BColours[3]; 
		}
		yield return new WaitForSeconds(0.8f);
		for(int i=0;i<Inputnum;i++){
			if (!aaaaa[i]){
				E = false;
				yield return new WaitForSeconds(.2f);
			Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
			BCChanger[i].material = BColours[7]; 
			}
		}
		if(E){
            FlashOrNot = true;
            if (Settings.TRUEOMEGAFORGET)
            {
                Audio.PlaySoundAtTransform("Ten_Stages_Passed", Numbers[0].transform);
                Debug.LogFormat("[OmegaForget #{0}]: Wait... did... did you really just... Yes, you did! You won! Congratulations!", _moduleId);
                SubSegment++;
                ColorChanger[0].material = Lights[4];
                ColorChanger[1].material = Lights[4];
                Lightarray[0].color = Colors[4];
                Lightarray[1].color = Colors[4];
                yield return new WaitForSeconds(0.7f);
                Checking = false;
                intro = true;
                yield return new WaitForSeconds(0.8f);
                StartCoroutine(SolveAnimation());
                yield return new WaitForSeconds(2.2f);
                Checking = true;
            }
            else
            {
                GoodOne = true;
                yield return new WaitForSeconds(0.1f);
                Audio.PlaySoundAtTransform("Reveal_"+Rnd.Range(1, 4), Numbers[0].transform);
                Numbers[1].text = "-YOU";
                ColorChanger[0].material = Lights[4];
                yield return new WaitForSeconds(0.8f);
                Audio.PlaySoundAtTransform("Reveal_"+Rnd.Range(1, 4), Numbers[0].transform);
                Numbers[1].text = "-WIN";
                ColorChanger[1].material = Lights[4];
                yield return new WaitForSeconds(0.7f);
                Audio.PlaySoundAtTransform("End", Numbers[0].transform);
                Debug.LogFormat("[OmegaForget #{0}]: THAT'S ALL SHE WROTE!", _moduleId);
                yield return new WaitForSeconds(1.5f);
                for (int i = 0; i < 10; i++)
                    BCChanger[i].material = BColours[3];
                Module.HandlePass();
                NerfSolve.clip = NerfSound;
                NerfSolve.Play();
                StopTheMusic = true;
                yield return new WaitForSeconds(10f);
                while(NerfSolve.isPlaying)
                {
                    Numbers[0].text = "";
                    Numbers[1].text = "PRES";
                        yield return new WaitForSeconds(1f);
                    Numbers[1].text = "BUTN";
                        yield return new WaitForSeconds(1f);
                    Numbers[0].text = "TO";
                    Numbers[1].text = "SKIP";
                        yield return new WaitForSeconds(2f);
                }
                Numbers[0].text = "";
                Numbers[1].text = "";
            }
        }
		else{
            yield return new WaitForSeconds(1.5f);
			Audio.PlaySoundAtTransform("Wrong_Answer_End", Numbers[0].transform);
            Debug.LogFormat("[OmegaForget #{0}]: Final set of inputs had at least one incorrect press. Try again.", _moduleId);
            StartCoroutine(Recovery());
			ColorChanger[0].material = Lights[9];
			ColorChanger[1].material = Lights[9];
			Lightarray[0].color = Colors[9];
			Lightarray[1].color = Colors[9];
			Module.HandleStrike();
			yield return new WaitForSeconds(1f);
		ColorChanger[0].material = Lights[1];
		ColorChanger[1].material = Lights[1];
		Lightarray[0].color = Colors[1];
		Lightarray[1].color = Colors[1];
            if (SubSegment == 0)
                FlashOrNot = false;
        }
		yield break;
	}
	IEnumerator Check(){
		Audio.PlaySoundAtTransform("Reveal_"+Rnd.Range(1,4), Numbers[0].transform);
		bool[] aaaaa = {false,false,false,false,false,false,false,false,false,false};
		bool E = true;
		for(int i=0;i<10;i++){
			if (PStorage[SubSegment*10+i] != AStorage[SubSegment*10+i])
				aaaaa[i] = false;
			else aaaaa[i] = true;
			BCChanger[i].material = BColours[3]; 
		}
		yield return new WaitForSeconds(0.8f);
		for(int i=0;i<10;i++){
			if (!aaaaa[i]){
				E = false;
				yield return new WaitForSeconds(.2f);
			Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
			BCChanger[i].material = BColours[7]; 
			}
		}
		if(E){
            Inputnum = 0;
            FlashOrNot = true;
			Audio.PlaySoundAtTransform("Ten_Stages_Passed", Numbers[0].transform);
            Debug.LogFormat("[OmegaForget #{0}]: This set of inputs was correct! Moving on to the next set.", _moduleId);
            GoodOne = true;
			SubSegment++;
			ColorChanger[0].material = Lights[4];
			ColorChanger[1].material = Lights[4];
			Lightarray[0].color = Colors[4];
			Lightarray[1].color = Colors[4];
		}
		else{
            yield return new WaitForSeconds(1.5f);
			Audio.PlaySoundAtTransform("Wrong_Answer_End", Numbers[0].transform);
            Debug.LogFormat("[OmegaForget #{0}]: Looks like some presses were incorrect. Strike.", _moduleId);
            StartCoroutine(Recovery());
			ColorChanger[0].material = Lights[9];
			ColorChanger[1].material = Lights[9];
			Lightarray[0].color = Colors[9];
			Lightarray[1].color = Colors[9];
			Module.HandleStrike();
            yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(0.7f);
		ColorChanger[0].material = Lights[1];
		ColorChanger[1].material = Lights[1];
		Lightarray[0].color = Colors[1];
		Lightarray[1].color = Colors[1];
        if (GoodOne)
        {
            Checking = false;
            intro = true;
            yield return new WaitForSeconds(3f);
            intro = false;
            GoodOne = false;
        }
		yield break;
	}
    IEnumerator Recovery()
    {
        RecoveryModeActive = true;
        for (int i = 0; i < Inputnum; i++)
        {
            RecoveryNumbers[i].text = i.ToString();
            Audio.PlaySoundAtTransform("Wrong_Answer_Reveal", Numbers[0].transform);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Godospin(){
        if (Stage == 0)
        {
            yield return new WaitForSeconds(2f);
            Audio.PlaySoundAtTransform("Startup", Buttons[2].transform);
            intro = true;
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 10; i++)
                BCChanger[i].material = BColours[BCTrack[i]];
            StartCoroutine(ColorCycleer());
            if (Settings.TRUEOMEGAFORGET) {
                Numbers[1].text = "FUCK";
                Debug.LogFormat("[OmegaForget #{0}]: TRUE OMEGAFORGET ACTIVATED!", _moduleId);
            }
        else
            Numbers[1].text = "OHNO";
			yield return new WaitForSeconds(3.5f);
            for (int i = 0; i < 3; i++)
                StageStorage[i] = Rnd.Range(0, 6);
        RecoverRotations[Stage] = StageStorage[0];
        if (Settings.TRUEOMEGAFORGET)
        {
            RecoverCruelRotations[Stage] = StageStorage[1];
            RecoverMoreRotations[Stage] = StageStorage[2];
        }
            StageStorage[4]=Rnd.Range(0,14);
		StageStorage[5]=Rnd.Range(0,14);
		StageStorage[3]=Rnd.Range(0,36)*10+Rnd.Range(0,36);
		Numbers[0].text = "";
		Numbers[0].text += Base36[StageStorage[3]/36];
		Numbers[0].text += Base36[StageStorage[3]%36];
        RecoverDoubleDigit[Stage] = Base36[StageStorage[3]/36].ToString() + Base36[StageStorage[3]%36].ToString();
		ColorChanger[0].material = Lights[StageStorage[4]];
        RecoverLights[Stage] = StageStorage[4];
		ColorChanger[1].material = Lights[StageStorage[5]];
        RecoverOtherLights[Stage] = StageStorage[5];
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
        Debug.LogFormat("[OmegaForget #{0}]: -----STAGE {1}-----", _moduleId, Stage);
        Debug.LogFormat("[OmegaForget #{0}]: The number in Base 36 is {1}.", _moduleId, Numbers[0].text);
        if (Settings.TRUEOMEGAFORGET)
            Debug.LogFormat("[OmegaForget #{0}]: The rotations, in order, for this stage are {1}, {2}, {3}.", _moduleId, Rotations[StageStorage[0]], Rotations[StageStorage[1]], Rotations[StageStorage[2]]);
        else
            Debug.LogFormat("[OmegaForget #{0}]: The rotation this stage is {1}.", _moduleId, Rotations[StageStorage[0]]);
        if ((Stage != 0)&&(Settings.EveryStageSounds))
        {
            Audio.PlaySoundAtTransform("Stage_Generated", Buttons[2].transform);
        }
		Debug.LogFormat("[OmegaForget #{0}]: The LED colors are {1} and {2}.", _moduleId,CNames[StageStorage[4]],CNames[StageStorage[5]]);
		Debug.LogFormat("[OmegaForget #{0}]: In Base 10, the number displayed is {1}.", _moduleId,StageStorage[3]);
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
                if (!Settings.TRUEOMEGAFORGET)
                    i = 3;
				}
            if (MistakesWereMade)
                yield break;
            else if (Stage < Bomb.GetSolvedModuleNames().Where(a => !ignoredModules.Contains(a)).Count() && !solved)
                {
                Stage++;
                if (Stage != maxStage)
                    PleaseDoRNGThings();
                else
                    SubmissionMode();
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
		IEnumerator SolveLights(){
			Lightarray[0].color = Colors[13];
			ColorChanger[0].material = Lights[13];
			Lightarray[1].color = Colors[0];
			ColorChanger[1].material = Lights[0];
			yield return new WaitForSeconds(1.17f);
			
			for(int i=1;i<13;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}

            BCChanger[9].material = BColours[0];
			ColorChanger[0].material = Lights[1];
			Lightarray[1].color = Colors[13];
			Lightarray[0].color = Colors[1];
			ColorChanger[1].material = Lights[13];
			yield return new WaitForSeconds(1.17f);
            BCChanger[8].material = BColours[0];
			
			for(int i=0;i<4;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}

            BCChanger[7].material = BColours[0];
			ColorChanger[0].material = Lights[10];
			Lightarray[0].color = Colors[10];
			Lightarray[1].color = Colors[4];
			ColorChanger[1].material = Lights[4];
			yield return new WaitForSeconds(1.17f);
            BCChanger[6].material = BColours[0];
			
			for(int i=5;i<9;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}
            BCChanger[5].material = BColours[0];
			ColorChanger[0].material = Lights[5];
			Lightarray[0].color = Colors[5];
			Lightarray[1].color = Colors[9];
			ColorChanger[1].material = Lights[9];
			yield return new WaitForSeconds(1.17f);
            BCChanger[4].material = BColours[0];
			
			for(int i=6;i<14;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}
            BCChanger[3].material = BColours[0];
			for(int i=0;i<4;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}
            BCChanger[2].material = BColours[0];
			ColorChanger[0].material = Lights[9];
			Lightarray[0].color = Colors[9];
			Lightarray[1].color = Colors[5];
			ColorChanger[1].material = Lights[5];
			yield return new WaitForSeconds(1.17f);
            BCChanger[1].material = BColours[0];	

			for(int i=6;i<10;i++){
			Lightarray[0].color = Colors[13-i];
			ColorChanger[0].material = Lights[13-i];
			Lightarray[1].color = Colors[i];
			ColorChanger[1].material = Lights[i];
			yield return new WaitForSeconds(.2925f);
			}
            BCChanger[0].material = BColours[0];
			ColorChanger[0].material = Lights[3];
			Lightarray[0].color = Colors[3];
			Lightarray[1].color = Colors[11];
			ColorChanger[1].material = Lights[11];
			yield return new WaitForSeconds(1.17f);
		}
	#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press X# (Waits for the color X to be in the #th position, then presses it) ||Commands can be chained using X# X#...|| (If your submission includes buttons beyond the current set, they will not be submitted!) | !{0} stage # (Available during stage recovery, begins viewing of that stage, or exits stage recovery mode if already on that stage)";
	#pragma warning restore 414
	 IEnumerator ProcessTwitchCommand(string command)
    {
		bool Valid = true;
	     Match m;
        string[] WhatHaveYouDone = command.Split(' ');
        if ((m = Regex.Match(command, @"^\s*press\s+(?:(.)(\d)\s*)+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)).Success)
        {
			 for (var i = 0; i < m.Groups[1].Captures.Count; i++)
            {
                var letter = m.Groups[1].Captures[i].Value;
                var number = m.Groups[2].Captures[i].Value[0] - '0';
				
				if(Array.IndexOf(BNames,letter)==-1)
					Valid = false;
            }
            if (RecoveryModeActive)
            {
                yield return "sendtochaterror Please use '![0] stage #' when in recovery mode!";
                yield break;
            }
            else if (!Valid)
            {
                yield return "sendtochaterror Incorrect syntax. Valid colors are K,B,C,G,M,O,P,R,W, and Y";
                yield break;
            }
            else if (Inputnum + m.Groups[1].Captures.Count > 10 || GoodOne)
            {
                yield return "sendtochaterror Ignored command to prevent accidental button presses during an animation.";
                yield break;
            }
                yield return null;  // acknowledge to TP that the command was valid

            for (var i = 0; i < m.Groups[1].Captures.Count; i++)
            {
                var letter = m.Groups[1].Captures[i].Value;
                var number = m.Groups[2].Captures[i].Value[0] - '0';
				
                while (Checking)
                    yield return "trycancel";
                while (Array.IndexOf(BCTrack, Array.IndexOf(BNames, letter)) != number)
                    yield return "trycancel";
                Buttons[number].OnInteract();
                yield return new WaitForSeconds(.1f);
            }
        }
        if ((Regex.IsMatch(WhatHaveYouDone[0], @"^\s*stage\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)))
        {
            string check = "";
            for (int i = 1; i < WhatHaveYouDone.Length; i++)
                check = check + "" + WhatHaveYouDone[i];
            bool help = true;
            for (int i = 0; i < check.Length; i++)
            {
                if ("0123456789".IndexOf(check[i]) < 0)
                {
                    help = false;
                    break;
                }
            }
            if (check.Length > 1)
                help = false;
            if (!RecoveryModeActive)
            {
                yield return "sendtochaterror Please use '![0] press X#' when not in recovery mode!";
                yield break;
            }
            else if (!help)
            {
                yield return "sendtochaterror There's no reason to be pressing that many buttons.";
                yield break;
            }

            yield return null;
            for (int i = 0; i < check.Length; i++)
            {
                Buttons[check[i] - '0'].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (Regex.IsMatch(WhatHaveYouDone[0], @"^\s*activatecruel\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            if (Stage != 0)
            {
                yield return "sendtochaterror You can only activate this on the first stage.";
            }
            if (!CruelActivation)
            {
                StartCoroutine(JesusChristWhy());
                Numbers[1].text = "????";
                yield return "sendtochat For your own sanity, PLEASE do not commit to this...";
                yield break;
            }
            else
            {
                StopCoroutine(JesusChristWhy());
                StartCoroutine(YOUMADEAMISTAKE());
                yield return "sendtochat Welp, don't say I didn't warn you.";
                yield break;
            }
        }
        else
            yield return "sendtochaterror Incorrect Syntax. Use '!{1} press X#'.";
	}
	IEnumerator TwitchHandleForcedSolve()
    {
        Autosolving = true;
		while (!submission) //Wait until submission time
            yield return true;
		string[] m = PStorage;
		for(int i=0;i<PStorage.Length;i++){
		string letter = m[i][0].ToString();
        string number = m[i][1].ToString();
		while (Checking)
			yield return new WaitForSeconds(.1f);
        while ((Array.IndexOf(BCTrack, Array.IndexOf(BNames, letter))).ToString() != number)
			yield return new WaitForSeconds(.1f);
		Buttons[int.Parse(number)].OnInteract();
		ASTracker++;
		}
    }
}