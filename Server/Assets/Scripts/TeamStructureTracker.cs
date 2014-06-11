using UnityEngine;
using System.Collections;

public delegate void StructureCompleteHandler();
public delegate void StructureProgressHandler(float progress);

public class TeamStructureTracker
{
    public event StructureCompleteHandler OnCompletion;
    public event StructureProgressHandler OnProgressChange;

    private Structure<Color?> goal;

    private int totalblockCount;
    private int correctBlockCount = 0;
    private int wrongBlockCount = 0;

    public float Progress
    {
        get
        {
            return Mathf.Max(0.0f, (correctBlockCount - (wrongBlockCount / 2f)) / totalblockCount);
        }
    }

    public TeamStructureTracker(Structure<Color?> goalStructure)
    {
        this.goal = goalStructure;

        initializeCorrectness();
    }

    private void initializeCorrectness()
    {
        foreach (Color? color in goal)
        {
            if (color != null)
            {
                this.totalblockCount++;
            }
        }
    }

    public Vector3 Normalize(Vector3 position, float scale)
    {
        Vector3 normalized = position / scale;
        normalized.x += goal.GetLength(0) / 2;
        normalized.z += goal.GetLength(2) / 2;
        return normalized;
    }

    public bool CheckBlock(Vector3 position, Color? color)
    {
        bool correct = checkColor(position, color);

        if (correct)
        {
            if (color == null) wrongBlockCount--;
            else correctBlockCount++;
        }

        else
        {
            if (color == null) correctBlockCount--;
            else wrongBlockCount++;
        }

        invokeHandlers();

        return correct;
    }

    private bool checkColor(Vector3 position, Color? color)
    {
        return this.goal[position] == color;
    }

    private void invokeHandlers()
    {
        if (OnProgressChange != null)
        {
            OnProgressChange(Progress);
        }

        if (Progress == 1 && OnCompletion != null)
        {
            OnCompletion();
        }
    }
}
