//using Pathfinding;
//using UnityEngine;
//using System.Collections;

//[RequireComponent (typeof(Rigidbody2D))]
//[RequireComponent(typeof(Seeker))]
//public class EnemyAI : MonoBehaviour
//{
//    //What to chase?
//    public Transform target;
//    //how many times eatch second we will update our path
//    public float updateRate = 2f;
//    //caching
//    private Seeker seeker;
//    private Rigidbody2D rb;
//    //the calculated path
//    public Path path;
//    //the AI speed per secund
//    public float speed = 300f;
//    public ForceMode2D fMode;

//    [HideInInspector]
//    public bool pathIsEnded = false;

//    //The max distance from the ai to a waypoint for it to continue to the next waypoint
//    public float nextWaypointDistance = 3;

//    //the Waypont we are currently walking too
//    private int currentWaypoint = 0;

//    private bool searchingForPlayer = false;

//    void Start()
//    {
//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();

//        if(target == null)
//        {
//            if (!searchingForPlayer)
//            {
//                searchingForPlayer = true;
//                StartCoroutine(searchingForPlayer());

//            }

//            return;
//        }

//        //start new path
//        seeker.StartPath(transform.position, target.position, OnPathComplete);

//        StartCoroutine(UpdatePath());

//    }

//    IEnumerator SearchForPlayer()
//    {
//        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
//        if(sResult == null)
//        {
//            yield return new WaitForSeconds(0.5f);
//            StartCoroutine(SearchForPlayer());
//        }
//        else
//        {
//            target = sResult.transform;
//            searchingForPlayer = false;
//            StartCoroutine(UpdatePath());
//            yield break;
//        }
//    }

//    IEnumerator UpdatePath()
//    {

//        if (target == null)
//        {
//            if (!searchingForPlayer)
//            {
//                searchingForPlayer = true;
//                StartCoroutine(SearchForPlayer());
//            }
//            yield return false;
//        }

//        else
//        {
//            seeker.StartPath(transform.position, target.position, OnPathComplete);
//            yield return new WaitForSeconds(1f / updateRate);
//            StartCoroutine(UpdatePath());
//        }
//    }


//    public void OnPathComplete(Path p)
//    {
//        Debug.Log("We got a path Did it have an error?" + p.error);
//        if (!p.error)
//        {
//            path = p;
//            currentWaypoint = 0;
//        }
//    }

//    void FixedUpdate()
//    {
//        if (target == null)
//        {
//            if (!searchingForPlayer)
//            {
//                searchingForPlayer = true;
//                StartCoroutine(searchingForPlayer());

//            }

//            return;
//        }

//        if (path == null)
//        {
//            return;
//        }
//        if(currentWaypoint >= path.vectorPath.Count)
//        {
//            if (pathIsEnded)
//                return;

//            Debug.Log("End of path reached.");
//            pathIsEnded = true;
//            return;
//        }
//        pathIsEnded = false;

//        //direction to the next waypoint
//        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
//        dir *= speed * Time.fixedDeltaTime;

//        //move the AI
//        rb.AddForce(dir, fMode);

//        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

//        if (dist < nextWaypointDistance)
//        {
//            currentWaypoint++;

//        }
//    }


//}


using Pathfinding;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    // What to chase?
    public Transform target;
    // How many times each second we will update our path
    public float updateRate = 2f;
    // Caching
    private Seeker seeker;
    private Rigidbody2D rb;
    //The calculated path
    public Path path;
    //The AI’s speed per second
    public float speed = 1f;
    public ForceMode2D fMode;
    [HideInInspector]
    public bool pathIsEnded = false;
    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    private bool searchingForPlayer = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        StartCoroutine(UpdatePath());

        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    private IEnumerator SearchForPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
        }
    }

    private IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }
        else
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        //Always look at player
        /*if (transform.position.x < target.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            //Grypho added this part. When reach the end we gave it players new position.
            StartCoroutine(SearchForPlayer());
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }
}
