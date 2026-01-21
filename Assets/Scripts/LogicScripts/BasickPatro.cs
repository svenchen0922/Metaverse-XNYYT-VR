using UnityEngine;
using UnityEngine.AI;

public class BasickPatro : MonoBehaviour
{
    private Animation animation;
    public Transform[] waypoints;
    NavMeshAgent agent;
    int index;

    private void Awake()
    {

        animation = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        //不断向当前位置点前进
        InvokeRepeating("Tick", 0.5f, 0.5f);
        if (waypoints.Length > 0)
        {
            //5秒进入下个位置点
            InvokeRepeating("Patrol", 0,10f);
        }

    }
    void Patrol()
    {
        //位置点是否到了最后一个，如果是重回第一个位置点，否则到下一个位置点
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }
    void Tick()
    {
        agent.destination = waypoints[index].position;
    }

	private void Update()
	{
      

		if (agent.remainingDistance > 0f)
		{
            animation.Play("走");

		}
		else
		{
            animation.Play("待机2");
		}
	}
}