using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdatePos : MonoBehaviour
{
	public float currentTime = 10.0f;

	// 定义移动速度
	public float MoveSpeed = 1f;

	//定义一个返回数据的Action，处理其他人的位置信息
	private Action<bool, string> actionRes;

	//定义一个返回数据的Action，处理自己的位置信息
	private Action<bool, string> playerRes;

	//玩家的属性，用于获取位置信息
	public Transform playerTransform;

	public Transform fatherTransform;

	//定义一个用户位置类，避免重复new
	public PlayerPos pos = new PlayerPos();

	bool isInit = false;


	void Start()
	{
		InitAction();
	}

	// Update is called once per frame
	
  void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            //Debug.Log(string.Format("currentTime={0}", Time.time));
            currentTime = 10.0f;

            //每隔5s发送网络请求，上送自己位置，获取其他人物模型，坐标等信息
            if (playerTransform) {
                pos.x = playerTransform.localPosition.x;
                pos.y = playerTransform.localPosition.y;
                pos.z = playerTransform.localPosition.z;
                pos.ax = playerTransform.eulerAngles.x;
                pos.ay = playerTransform.eulerAngles.y;
                pos.az = playerTransform.eulerAngles.z;

                print(SceneManager.GetActiveScene().name);
                if (SceneManager.GetActiveScene().name == "VR_Office")
                {
                    GlobalData.Instance.sceneid = "1";
                  
                }
                else
                {
                    GlobalData.Instance.sceneid = "2";
                    
                }
                pos.sceneid = GlobalData.Instance.sceneid;
                GlobalData.Instance.userId = PlayerInfo.Instance.UserId;
                pos.userId = GlobalData.Instance.userId;
                
              
                string data = JsonUtility.ToJson(pos);
                
                //Debug.Log(string.Format("PlayerPos={0}", data));
                //将最后一次位置也保存到本地
                PlayerPrefs.SetString("playerPos", data);


                GlobalData.Instance.positionUrl = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank/vrbank" + "/main/status/update";
                GlobalData.Instance.playerUrl = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank/vrbank" + "/main/status/init";

			


                HttpRestful.Instance.Post(GlobalData.Instance.positionUrl, data, actionRes);
                print(data);
              
             
            }

            ////如果userId已从客户端赋值，且还没有从本地初始化，则初始化本人
            //if (!isInit && !GlobalData.Instance.userId.Equals("")) {
            //    //xiu - 可修改为从本地缓存获取
            //    string data = "{\"userId\":\"" + GlobalData.Instance.userId + "\"}";
            //    HttpRestful.Instance.Post(GlobalData.Instance.playerUrl, data, playerRes);
            //    isInit = true;
            //}
        }
    }
	




	//其他人物移动
	IEnumerator move(GameObject obj, PersonData data)
	{
		Animation m_anim = obj.GetComponent<Animation>();
		NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
	
		m_anim.Play("走");

        agent.destination = obj.transform.localPosition;



        yield return null;
	}



    private void InitAction()
    {
        actionRes = new Action<bool, string>((bl, str) =>
        {
            if (bl)
            {
                Debug.Log(string.Format("actionRes httperror:{0}", str));
            }
            else
            {
                Debug.Log(string.Format("actionRes:{0}", str));

                Person person = JsonUtility.FromJson<Person>(str);
                if (person.retCode.Equals("0"))
                {
                    GlobalData.Instance.selfAlone = person.selfAloneFlag;

                    StartCoroutine(generatePersonDelay(person));
                }

            }
        });

        playerRes = new Action<bool, string>((bl, str) =>
        {
            if (bl)
            {
                Debug.Log(string.Format("playerRes httperror:{0}", str));
            }
            else
            {
                Debug.Log(string.Format("playerRes:{0}", str));
           
                PlayerData data = JsonUtility.FromJson<PlayerData>(str);

                if (data.retCode.Equals("0"))
                {
                    //将数据存储到本地，后面可以从本地去加载和初始化
                    PlayerPrefs.SetString("player", str);

                    generatePerson(data.selfInfo);
                    playerTransform = GameObject.FindWithTag("Player").transform;
                }

            }
        });
    }

    IEnumerator generatePersonDelay(Person person)
    {
        foreach (PersonData item in person.stranger)
        {
            //保存用户信息，用于talk
            string ostr = JsonUtility.ToJson(item, true);
            PlayerPrefs.SetString(item.userId, ostr);

            //间隔一定间隔加载一个人物
            yield return new WaitForSeconds(0.2f);
            this.generatePerson(item);
        }
    }
      private void generatePerson(PersonData data) {

       
        GameObject obj = GameObject.Find("Player/" + data.userId);


		//if (SceneManager.GetActiveScene().name== "VR_Office"  &&data.)
		//{

		//}




        //若存在，则移动
        //不存在则动态创建
        if (obj)
        {
           

            //人物走到目的地
            
            StartCoroutine(move(obj, data));

     

        }
        else 
        {
            Debug.Log(string.Format("create one:{0},{1}", data.userId, data.avatar));

            //加载Resoureces文件夹下名字叫做display的预制物体
            GameObject pre = Resources.Load<GameObject>(data.avatar); 
               
            //实例化到场景中
            GameObject instancess = Instantiate(pre) as GameObject;

            //设置名字
            instancess.name = data.userId;

            //将物体绑定到父物体下面
            instancess.transform.parent = fatherTransform;
            //给物体赋值坐标
            instancess.transform.localPosition = new Vector3(data.lastX, data.lastY, data.lastZ);
            //instancess.transform.localPosition = new Vector3(0f, 0.41f, -7f);

            //给物体大小赋值
           // instancess.transform.localScale = new Vector3(1, 1, 1);


            //给物体的角度赋值
            //Quaternion disquter = Quaternion.Euler(data.lastAngleX, data.lastAngleY, data.lastAngleZ);
            //nstancess.transform.localRotation = disquter;

            //修改昵称和姓名
            //GameObject nickText = GameObject.Find("Player/"+data.userId + "/NickName/NickName");
            //nickText.GetComponent<Text>().text = data.nickName;
            //GameObject realText = GameObject.Find("Player/" + data.userId + "/NickName/RealName");
            //realText.GetComponent<Text>().text = "<" + data.userName + ">";

        

        }

    }
}
