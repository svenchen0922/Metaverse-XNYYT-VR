using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : UnitySingleton<GlobalData>
{


	public string apple_photo_one;
	public string apple_photo_two;
	public string apple_photo_three;
	public string apple_photo_four;
	public string apple_photo_five;

	public string VideoURL;
	public string token;
	public string Sex;

	public bool IS_Ready = false;

	public int KeyNumber = 3;
	public string sceneid = "1";

	//访问域名
	public  string baseUrl = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank/vrbank";
	//用户Id
	public  string userId;
	//更新查询位置接口
	public  string positionUrl = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank/vrbank" + "/main/status/update";
	//查询玩家信息接口
	public  string playerUrl = "http://shuziren.dccnet.com.cn:89/icbc/hbfh/metaweb/vrbank/vrbank" + "/main/status/init";

	//是否要聊天
	public  string selfAlone = "-1"; // 69890WPvHC




	public bool IsLogin= true;
}
