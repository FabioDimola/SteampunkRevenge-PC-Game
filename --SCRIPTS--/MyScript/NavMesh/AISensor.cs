using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AISensor : MonoBehaviour
{
    public float distance = 10;
    public float height = 5;
    public float angle = 30;
    public Color meshColor = Color.blue;

    Mesh mesh;

    EnemyPatroling enemyPatroling;
    private GameObject player;
    private Animator animator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;

    //Scan
    public int scanFrequency = 30;
    public LayerMask playerLayers;
    public LayerMask occlusionLayer;
    Collider[] colliders = new Collider[50];
    int count;
    float scanInterval;
    float scanTimer;

    public List<GameObject> Objects = new List<GameObject>();



    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;


        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;


        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] =topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;



        //far side
        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = topLeft;
        vertices[vert++] = bottomLeft;

        //top
        vertices[vert++] = topCenter;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;


        //bottom
        vertices[vert++] = bottomCenter; 
        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;


        for(int i = 0; i < numVertices; ++i)
        {

            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;

    }


    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }


    private void Start()
    {
        scanTimer = 1.0f / scanFrequency;
        enemyPatroling = GetComponent<EnemyPatroling>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {
        scanTimer -= Time.deltaTime;

        if(scanTimer < 0.0f )
        {
            scanTimer += scanInterval;
            Scan();
        }

        if(IsInSight(player))
        {
            Shoot();

        }
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position,transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance);

        for(int i=0; i < count; ++i)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;
        foreach(var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if(direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;

        float deltaAngle = Vector3.Angle(direction, transform.forward);

        if(deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if(Physics.Linecast(origin,dest,occlusionLayer)) { // se il nemico ha un ostacolo davanti non vede il giocatore

            return false;
        }
        return true;
    }


    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, playerLayers, QueryTriggerInteraction.Collide); //return the number of objects that enter in the colliders array

        Objects.Clear();
        for(int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);
              
            }
        }



    }




    void Shoot()
    {
        enemyPatroling.Stop();
        animator.SetTrigger("Fire");
        Vector3 target = (player.transform.position - transform.position).normalized;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(target, Vector3.up));

    }
}
