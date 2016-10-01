using UnityEngine;

// Calculates trajectory and draws a line
public class TrajectoryGizmo : AgentComponent
{
    private Vector3? force;
    private Vector3? startPoint;
    private LineRenderer trajectoryLine;
    private int samples;
    private int samplesDistance;

    public TrajectoryGizmo(int samples, int samplesDistance)
    {
        this.samples = samples;
        this.samplesDistance = samplesDistance;
        trajectoryLine = (GameObject.Instantiate(Resources.Load(GameConfig.TRAJECOTRY_GIZMO_PATH)) as GameObject).GetComponent<LineRenderer>();
    }

    #region AgentComponent implementation
    public void FrameFeed()
    {
        if (!startPoint.HasValue || !force.HasValue || trajectoryLine == null)
        {
            DisableLine();
            return;
        }

        CalculateLine();

        force = null;
        startPoint = null;
    }
    #endregion

    public void Display(Vector3 startPoint, Vector3 force)
    {
        this.force = force;
        this.startPoint = startPoint;
    }

    private void CalculateLine()
    {
        Vector3[] points = new Vector3[samples];
        Vector3 shootVector = force.Value;
        bool detectCollision = false;

        for (int i = 0; i < samples; i ++)
        {
            // resets position of left points
            if (detectCollision)
            {
                points[i] = points[i - 1];
                continue;
            }
            // calculate nex point in trajectory
            else
            {
                Vector3 shootDir = shootVector * Time.fixedDeltaTime * samplesDistance;
                Vector3 calcDir = (i == 0) ? startPoint.Value + shootDir : points[i - 1] + shootDir;

                // Check for collisions
                if (i != 0 && !detectCollision)
                {
                    RaycastHit hitInfo = new RaycastHit();
                    bool isColliding = Physics.Linecast(points[i - 1], calcDir, out hitInfo);
                    if (isColliding)
                    {
                        calcDir = hitInfo.point;
                        detectCollision = true;
                    }
                }

                // update points
                points[i] = calcDir;
                shootVector += (Physics.gravity * Time.fixedDeltaTime * samplesDistance);
            }
        }

        trajectoryLine.enabled = true;
        trajectoryLine.SetPositions(points);
    }

    private void DisableLine()
    {
        trajectoryLine.enabled = false;
    }
}
