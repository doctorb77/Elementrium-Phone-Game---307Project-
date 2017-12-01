using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Initialization;

public class TriumGODispenser : MonoBehaviour {
    public static TriumGODispenser Instance { get { return instance; } }
    private static TriumGODispenser instance;
    public static List<GameObject> dispenserList = new List<GameObject>();

    public GameObject prefabH;
    public GameObject prefabHe;
    public GameObject prefabLi;
    public GameObject prefabBe;
    public GameObject prefabB;
    public GameObject prefabC;
    public GameObject prefabN;
    public GameObject prefabO;
    public GameObject prefabF;
    public GameObject prefabNe;
    public GameObject prefabNa;
    public GameObject prefabMg;
    public GameObject prefabAl;
    public GameObject prefabSi;
    public GameObject prefabP;
    public GameObject prefabS;
    public GameObject prefabCl;
    public GameObject prefabAr;
    public GameObject prefabK;
    public GameObject prefabCa;
    public GameObject prefabSc;
    public GameObject prefabTi;
    public GameObject prefabV;
    public GameObject prefabCr;
    public GameObject prefabMn;
    public GameObject prefabFe;
    public GameObject prefabCo;
    public GameObject prefabNi;
    public GameObject prefabCu;
    public GameObject prefabZn;
    public GameObject prefabGa;
    public GameObject prefabGe;
    public GameObject prefabAs;
    public GameObject prefabSe;
    public GameObject prefabBr;
    public GameObject prefabKr;
    public GameObject prefabRb;
    public GameObject prefabSr;
    public GameObject prefabY;
    public GameObject prefabZr;
    public GameObject prefabNb;
    public GameObject prefabMo;
    public GameObject prefabTc;
    public GameObject prefabRu;
    public GameObject prefabRh;
    public GameObject prefabPd;
    public GameObject prefabAg;
    public GameObject prefabCd;
    public GameObject prefabIn;
    public GameObject prefabSn;
    public GameObject prefabSb;
    public GameObject prefabTe;
    public GameObject prefabI;
    public GameObject prefabXe;
    public GameObject prefabCs;
    public GameObject prefabBa;
    public GameObject prefabLa;
    public GameObject prefabCe;
    public GameObject prefabPr;
    public GameObject prefabNd;
    public GameObject prefabPm;
    public GameObject prefabSm;
    public GameObject prefabEu;
    public GameObject prefabGd;
    public GameObject prefabTb;
    public GameObject prefabDy;
    public GameObject prefabHo;
    public GameObject prefabEr;
    public GameObject prefabTm;
    public GameObject prefabYb;
    public GameObject prefabLu;
    public GameObject prefabHf;
    public GameObject prefabTa;
    public GameObject prefabW;
    public GameObject prefabRe;
    public GameObject prefabOs;
    public GameObject prefabIr;
    public GameObject prefabPt;
    public GameObject prefabAu;
    public GameObject prefabHg;
    public GameObject prefabTl;
    public GameObject prefabPb;
    public GameObject prefabBi;
    public GameObject prefabPo;
    public GameObject prefabAt;
    public GameObject prefabRn;
    public GameObject prefabFr;
    public GameObject prefabRa;
    public GameObject prefabAc;
    public GameObject prefabTh;
    public GameObject prefabPa;
    public GameObject prefabU;
    //molecules--
    public GameObject prefabLiH;
    public GameObject prefabOH;
    public GameObject prefabBeO;
    public GameObject prefabO2;
    public GameObject prefabH2O;
    public GameObject prefabC2;
    public GameObject prefabNO;
    public GameObject prefabH2;
	public GameObject prefabN2;
	public GameObject prefabBO3;
	public GameObject prefabCO;
	public GameObject prefabN3;
	public GameObject prefabNO3;
	public GameObject prefabCO3;
	public GameObject prefabF2;
	public GameObject prefabNaN3;
	public GameObject prefabNaH;
	public GameObject prefabNaOH;
	public GameObject prefabMgOHOH;
    public GameObject prefabO3;


	void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        dispenserList.Add(prefabH);
        dispenserList.Add(prefabHe);
        dispenserList.Add(prefabLi);
        dispenserList.Add(prefabBe);
        dispenserList.Add(prefabB);
        dispenserList.Add(prefabC);
        dispenserList.Add(prefabN);
        dispenserList.Add(prefabO);
        dispenserList.Add(prefabF);
        dispenserList.Add(prefabNe);
        dispenserList.Add(prefabNa);
        dispenserList.Add(prefabMg);
        dispenserList.Add(prefabAl);
        dispenserList.Add(prefabSi);
        dispenserList.Add(prefabP);
        dispenserList.Add(prefabS);
        dispenserList.Add(prefabCl);
        dispenserList.Add(prefabAr);
        dispenserList.Add(prefabK);
        dispenserList.Add(prefabCa);
        dispenserList.Add(prefabSc);
        dispenserList.Add(prefabTi);
        dispenserList.Add(prefabV);
        dispenserList.Add(prefabCr);
        dispenserList.Add(prefabMn);
        dispenserList.Add(prefabFe);
        dispenserList.Add(prefabCo);
        dispenserList.Add(prefabNi);
        dispenserList.Add(prefabCu);
        dispenserList.Add(prefabZn);
        dispenserList.Add(prefabGa);
        dispenserList.Add(prefabGe);
        dispenserList.Add(prefabAs);
        dispenserList.Add(prefabSe);
        dispenserList.Add(prefabBr);
        dispenserList.Add(prefabKr);
        dispenserList.Add(prefabRb);
        dispenserList.Add(prefabSr);
        dispenserList.Add(prefabY);
        dispenserList.Add(prefabZr);
        dispenserList.Add(prefabNb);
        dispenserList.Add(prefabMo);
        dispenserList.Add(prefabTc);
        dispenserList.Add(prefabRu);
        dispenserList.Add(prefabRh);
        dispenserList.Add(prefabPd);
        dispenserList.Add(prefabAg);
        dispenserList.Add(prefabCd);
        dispenserList.Add(prefabIn);
        dispenserList.Add(prefabSn);
        dispenserList.Add(prefabSb);
        dispenserList.Add(prefabTe);
        dispenserList.Add(prefabI);
        dispenserList.Add(prefabXe);
        dispenserList.Add(prefabCs);
        dispenserList.Add(prefabBa);
        dispenserList.Add(prefabLa);
        dispenserList.Add(prefabCe);
        dispenserList.Add(prefabPr);
        dispenserList.Add(prefabNd);
        dispenserList.Add(prefabPm);
        dispenserList.Add(prefabSm);
        dispenserList.Add(prefabEu);
        dispenserList.Add(prefabGd);
        dispenserList.Add(prefabTb);
        dispenserList.Add(prefabDy);
        dispenserList.Add(prefabHo);
        dispenserList.Add(prefabEr);
        dispenserList.Add(prefabTm);
        dispenserList.Add(prefabYb);
        dispenserList.Add(prefabLu);
        dispenserList.Add(prefabHf);
        dispenserList.Add(prefabTa);
        dispenserList.Add(prefabW);
        dispenserList.Add(prefabRe);
        dispenserList.Add(prefabOs);
        dispenserList.Add(prefabIr);
        dispenserList.Add(prefabPt);
        dispenserList.Add(prefabAu);
        dispenserList.Add(prefabHg);
        dispenserList.Add(prefabTl);
        dispenserList.Add(prefabPb);
        dispenserList.Add(prefabBi);
        dispenserList.Add(prefabPo);
        dispenserList.Add(prefabAt);
        dispenserList.Add(prefabRn);
        dispenserList.Add(prefabFr);
        dispenserList.Add(prefabRa);
        dispenserList.Add(prefabAc);
        dispenserList.Add(prefabTh);
        dispenserList.Add(prefabPa);
        dispenserList.Add(prefabU);//91
        dispenserList.Add(prefabLiH);//94
        dispenserList.Add(prefabOH);//95
        dispenserList.Add(prefabBeO);//96
        dispenserList.Add(prefabO2);//97
        dispenserList.Add(prefabH2O);//98
        dispenserList.Add(prefabC2);
        dispenserList.Add(prefabNO);
        dispenserList.Add(prefabH2);
		dispenserList.Add(prefabN2);
		dispenserList.Add(prefabBO3);
		dispenserList.Add(prefabCO);
		dispenserList.Add(prefabN3);
		dispenserList.Add(prefabNO3);
		dispenserList.Add(prefabCO3);
		dispenserList.Add(prefabF2);
		dispenserList.Add(prefabNaN3);
		dispenserList.Add(prefabNaH);
		dispenserList.Add(prefabNaOH);
		dispenserList.Add(prefabMgOHOH);
		dispenserList.Add(prefabO3);
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
