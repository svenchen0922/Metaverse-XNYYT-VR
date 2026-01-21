using LitJson;

public class PlayerInfo
{

    public string UserId { get; private set; }

    public string Phone { get; private set; }

    public string NickName { get; private set; }

    //0:ÄÐ 1£ºÅ®
    public string Gender { get; private set; }

    public string ModelId { get; private set; }

    public string Token { get; private set; }

    private static PlayerInfo instance;

    public static PlayerInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerInfo();
            }
            return instance;
        }
    }

    private PlayerInfo() { Clear(); }


    public void SetLoginData(JsonData jsonData)
    {
      //  this.UserId = (string)jsonData["userId"];
        this.Phone = (string)jsonData["phone"];
        this.NickName = (string)jsonData["nickName"];
        this.Gender = (string)jsonData["gender"];
        this.ModelId = (string)jsonData["modelId"];
        this.Token = (string)jsonData["token"];
    }

    public void Clear()
    {
        this.UserId = null;
        this.Phone = null;
        this.NickName = null;
        this.Gender = null;
        this.ModelId = null;
        this.Token = null;
    }
}
