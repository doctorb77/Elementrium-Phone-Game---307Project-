using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Initialization;

public class GlossaryUIDispenser : MonoBehaviour {
	public static GlossaryUIDispenser Instance { get { return instance; } }
	private static GlossaryUIDispenser instance;
	public static List<Sprite> sprites = new List<Sprite>();

	public Sprite sprH;
	public Sprite sprHe;
	public Sprite sprLi;
	public Sprite sprBe;
	public Sprite sprB;
	public Sprite sprC;
	public Sprite sprN;
	public Sprite sprO;
	public Sprite sprF;
	public Sprite sprNe;
	public Sprite sprNa;
	public Sprite sprMg;
	public Sprite sprAl;
	public Sprite sprSi;
	public Sprite sprP;
	public Sprite sprS;
	public Sprite sprCl;
	public Sprite sprAr;
	public Sprite sprK;
	public Sprite sprCa;
	public Sprite sprSc;
	public Sprite sprTi;
	public Sprite sprV;
	public Sprite sprCr;
	public Sprite sprMn;
	public Sprite sprFe;
	public Sprite sprCo;
	public Sprite sprNi;
	public Sprite sprCu;
	public Sprite sprZn;
	public Sprite sprGa;
	public Sprite sprGe;
	public Sprite sprAs;
	public Sprite sprSe;
	public Sprite sprBr;
	public Sprite sprKr;
	public Sprite sprRb;
	public Sprite sprSr;
	public Sprite sprY;
	public Sprite sprZr;
	public Sprite sprNb;
	public Sprite sprMo;
	public Sprite sprTc;
	public Sprite sprRu;
	public Sprite sprRh;
	public Sprite sprPd;
	public Sprite sprAg;
	public Sprite sprCd;
	public Sprite sprIn;
	public Sprite sprSn;
	public Sprite sprSb;
	public Sprite sprTe;
	public Sprite sprI;
	public Sprite sprXe;
	public Sprite sprCs;
	public Sprite sprBa;
	public Sprite sprLa;
	public Sprite sprCe;
	public Sprite sprPr;
	public Sprite sprNd;
	public Sprite sprPm;
	public Sprite sprSm;
	public Sprite sprEu;
	public Sprite sprGd;
	public Sprite sprTb;
	public Sprite sprDy;
	public Sprite sprHo;
	public Sprite sprEr;
	public Sprite sprTm;
	public Sprite sprYb;
	public Sprite sprLu;
	public Sprite sprHf;
	public Sprite sprTa;
	public Sprite sprW;
	public Sprite sprRe;
	public Sprite sprOs;
	public Sprite sprIr;
	public Sprite sprPt;
	public Sprite sprAu;
	public Sprite sprHg;
	public Sprite sprTl;
	public Sprite sprPb;
	public Sprite sprBi;
	public Sprite sprPo;
	public Sprite sprAt;
	public Sprite sprRn;
	public Sprite sprFr;
	public Sprite sprRa;
	public Sprite sprAc;
	public Sprite sprTh;
	public Sprite sprPa;
	public Sprite sprU;
	//molecules--
	public Sprite sprLiH;
	public Sprite sprOH;
	public Sprite sprBeO;
	public Sprite sprO2;
	public Sprite sprH2O;
	public Sprite sprC2;
	public Sprite sprNO;
	public Sprite sprH2;
    public Sprite sprN2;
    public Sprite sprBO3;
    public Sprite sprCO;
    public Sprite sprN3;
    public Sprite sprNO3;
    public Sprite sprCO3;
    public Sprite sprF2;
    public Sprite sprNaN3;
    public Sprite sprNaH;
    public Sprite sprNaOH;
    public Sprite sprMgOHOH;
    public Sprite sprO3;


	void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		sprites.Add (sprH);
		sprites.Add (sprHe);
		sprites.Add (sprLi);
		sprites.Add (sprBe);
		sprites.Add (sprB);
		sprites.Add (sprC);
		sprites.Add (sprN);
		sprites.Add (sprO);
		sprites.Add (sprF);
		sprites.Add (sprNe);
		sprites.Add (sprNa);
		sprites.Add (sprMg);
		sprites.Add (sprAl);
		sprites.Add (sprSi);
		sprites.Add (sprP);
		sprites.Add (sprS);
		sprites.Add (sprCl);
		sprites.Add (sprAr);
		sprites.Add (sprK);
		sprites.Add (sprCa);
		sprites.Add (sprSc);
		sprites.Add (sprTi);
		sprites.Add (sprV);
		sprites.Add (sprCr);
		sprites.Add (sprMn);
		sprites.Add (sprFe);
		sprites.Add (sprCo);
		sprites.Add (sprNi);
		sprites.Add (sprCu);
		sprites.Add (sprZn);
		sprites.Add (sprGa);
		sprites.Add (sprGe);
		sprites.Add (sprAs);
		sprites.Add (sprSe);
		sprites.Add (sprBr);
		sprites.Add (sprKr);
		sprites.Add (sprRb);
		sprites.Add (sprSr);
		sprites.Add (sprY);
		sprites.Add (sprZr);
		sprites.Add (sprNb);
		sprites.Add (sprMo);
		sprites.Add (sprTc);
		sprites.Add (sprRu);
		sprites.Add (sprRh);
		sprites.Add (sprPd);
		sprites.Add (sprAg);
		sprites.Add (sprCd);
		sprites.Add (sprIn);
		sprites.Add (sprSn);
		sprites.Add (sprSb);
		sprites.Add (sprTe);
		sprites.Add (sprI);
		sprites.Add (sprXe);
		sprites.Add (sprCs);
		sprites.Add (sprBa);
		sprites.Add (sprLa);
		sprites.Add (sprCe);
		sprites.Add (sprPr);
		sprites.Add (sprNd);
		sprites.Add (sprPm);
		sprites.Add (sprSm);
		sprites.Add (sprEu);
		sprites.Add (sprGd);
		sprites.Add (sprTb);
		sprites.Add (sprDy);
		sprites.Add (sprHo);
		sprites.Add (sprEr);
		sprites.Add (sprTm);
		sprites.Add (sprYb);
		sprites.Add (sprLu);
		sprites.Add (sprHf);
		sprites.Add (sprTa);
		sprites.Add (sprW);
		sprites.Add (sprRe);
		sprites.Add (sprOs);
		sprites.Add (sprIr);
		sprites.Add (sprPt);
		sprites.Add (sprAu);
		sprites.Add (sprHg);
		sprites.Add (sprHe);
		sprites.Add (sprTl);
		sprites.Add (sprPb);
		sprites.Add (sprBi);
		sprites.Add (sprPo);
		sprites.Add (sprAt);
		sprites.Add (sprRn);
		sprites.Add (sprFr);
		sprites.Add (sprRa);
		sprites.Add (sprAc);
		sprites.Add (sprTh);
		sprites.Add (sprPa);
		sprites.Add (sprU);//91
		sprites.Add (sprLiH);//94
		sprites.Add (sprOH);//95
		sprites.Add (sprBeO);//96
		sprites.Add (sprO2);//97
		sprites.Add (sprH2O);//98
		sprites.Add (sprC2);
		sprites.Add (sprNO);
		sprites.Add (sprH2);
        sprites.Add(sprN2);
        sprites.Add(sprBO3);
        sprites.Add(sprCO);
        sprites.Add(sprN3);
        sprites.Add(sprNO3);
        sprites.Add(sprCO3);
        sprites.Add(sprF2);
        sprites.Add(sprNaN3);
        sprites.Add(sprNaH);
        sprites.Add(sprNaOH);
        sprites.Add(sprMgOHOH);
        sprites.Add(sprO3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
