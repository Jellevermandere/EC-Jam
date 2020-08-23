using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RoadController : MonoBehaviour
{
    [SerializeField]
    private SpriteShapeController otherSpriteShape;
    [SerializeField]
    private float tangentLength, maxDistance, minDistance, maxOffset;
    [SerializeField]
    private int nrStartPoints;
    [SerializeField]
    private GameObject carTrigger;

    public List<Vector2> roadPoints = new List<Vector2>();

    private int nrOfDeletetPoints;

    float timer;


    private SpriteShapeController spriteShape;

    // Start is called before the first frame update
    void Start()
    {
        spriteShape = GetComponent<SpriteShapeController>();

        CreateRoad();
        SetOtherSprite();
        carTrigger.transform.position = spriteShape.spline.GetPosition(nrStartPoints / 2);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        timer += Time.deltaTime;

        if(timer >= 2)
        {
            UpdateRoad(2);
            timer = 0;
            SetOtherSprite();
        }
        */

        

    }
    // updates the other spriteshape to mirror the edge collider
    void SetOtherSprite()
	{
        otherSpriteShape.spline.Clear();

        for (int i = spriteShape.spline.GetPointCount(); i > 0; i--)
        {
            otherSpriteShape.spline.InsertPointAt(0, spriteShape.spline.GetPosition(i-1));

            otherSpriteShape.spline.SetTangentMode(0, ShapeTangentMode.Continuous);
            otherSpriteShape.spline.SetLeftTangent(0, spriteShape.spline.GetLeftTangent(i - 1));
            otherSpriteShape.spline.SetRightTangent(0, spriteShape.spline.GetRightTangent(i - 1));

        }
	}

    // create a startroad
    void CreateRoad()
    {
        spriteShape.spline.Clear();

        for (int i = 0; i < nrStartPoints; i++)
        {
            AddRoadPoint(i);
            
        }
    }

    //updates the road so the roads repeats indefinetly
    public void UpdateRoad(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            spriteShape.spline.RemovePointAt(0);
            nrOfDeletetPoints++;
        }
        for (int i = spriteShape.spline.GetPointCount(); i < nrStartPoints; i++)
        {
            AddRoadPoint(i);
        }
        SetOtherSprite();
    }

    // adds a point at position i on the road spline
    void AddRoadPoint(int i)
    {
        if (i % 2 == 0)
        {
            spriteShape.spline.InsertPointAt(i, Vector2.zero + new Vector2(Random.Range(0, maxOffset), (i+nrOfDeletetPoints) * maxDistance));
        }
        else
        {
            spriteShape.spline.InsertPointAt(i, Vector2.zero + new Vector2(Random.Range(0, -maxOffset), (i + nrOfDeletetPoints) * maxDistance));
        }

        spriteShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

        spriteShape.spline.SetLeftTangent(i, Vector3.down * tangentLength);
        spriteShape.spline.SetRightTangent(i, Vector3.up * tangentLength);


        if (i == 0)
        {
            spriteShape.spline.SetPosition(0, Vector2.zero);
        }

        roadPoints.Add(spriteShape.spline.GetPosition(i));
    }

    public void MoveTrigger()
    {
        UpdateRoad(2);
        carTrigger.transform.position = spriteShape.spline.GetPosition(nrStartPoints / 2);
    }
}
