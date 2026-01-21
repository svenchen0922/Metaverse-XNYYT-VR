using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelSelectUI : MonoBehaviour
{

    [System.Serializable]
    public class Model
    {
        public string modelId;

        public string gender; //0-ÄÐ£» 1-Å®

        public Sprite image;
    }

    [SerializeField]
    private List<Model> models;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Button previous;

    [SerializeField]
    private Button next;

    private int modelIndex = 0;

    public Model CurrentModel { get { return models[modelIndex]; } }

    void Start()
    {
        this.modelIndex = 0;
        this.image.sprite = models[this.modelIndex].image;

        this.previous.onClick.AddListener(OnPrevious);
        this.next.onClick.AddListener(OnNext);
    }

    public void OnPrevious()
    {
        if (this.modelIndex <= 0)
        {
            return;
        }
        else
        {
            this.modelIndex--;
            this.image.sprite = models[this.modelIndex].image;
        }

        if(this.modelIndex == 0)
        {
            this.previous.enabled = false;
        }

        if (this.modelIndex < models.Count - 1)
        {
            this.next.enabled = true;
        }
    }

    public void OnNext()
    {
        if (this.modelIndex >= models.Count - 1)
        {
            return;
        }
        else
        {
            this.modelIndex++;
            this.image.sprite = models[this.modelIndex].image;
        }

        if (this.modelIndex == models.Count - 1)
        {
            this.next.enabled = false;
        }

        if (this.modelIndex > 0)
        {
            this.previous.enabled = true;
        }
    }
}
