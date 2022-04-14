using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartExp : MonoBehaviour
{
    public bool lvl_one = true;
    public bool lvl_two, lvl_three = false;
    public bool exp_done = false;
    private static int targetsHit, targetsMissed = 0;
    private static int hit_one, missed_one, hit_two, missed_two, hit_three, missed_three = 0;
    private static float acc_one, acc_two, acc_three, acc_ovr = 100f;
    public GameObject Targets_Lvl_One, Targets_Lvl_Two, Targets_Lvl_Three;
    public GameObject Target_Obj;
    private float time_one, time_two, time_three, time_passed = 0f;
    public List<Transform> corners = new List<Transform>(8);
    public List<Transform> walls = new List<Transform>(4);
    private static List<List<string>> results = new List<List<string>>(4);
    private enum Scene {
        Results_Scene,
    }

    public static List<List<string>> getResults() {
        return results;
    }
    //WALL ARRANGEMENTS:
    // WALL_1: 1, 2, 5, 6  --> Y axis free
    // WALL_2: 2, 4, 6, 8  --> X axis free
    // WALL_3: 3, 4, 7, 8  --> Y axis free
    // WALL_4: 1, 3, 5, 7  --> X axis free

    // Start is called before the first frame update
    void Start()
    {
        exp_done = false;
        if (lvl_one) { CreateLevelOne();}
        else if (lvl_two) { CreateLevelTwo();}
        else if (lvl_three) { CreateLevelThree();}
    }

    void CreateLevelOne() {
        // GameObject targets_one = Instantiate(Targets_Lvl_One);
        createRandomLvl(12);
        time_passed = Time.time;
    }
    void CreateLevelTwo() {
        // GameObject targets_two = Instantiate(Targets_Lvl_Two);
        createRandomLvl(16);
        time_passed = Time.time;
    }
    void CreateLevelThree() {
        // GameObject targets_three = Instantiate(Targets_Lvl_Three);
        createRandomLvl(20);
        time_passed = Time.time;
    }

    private void createRandomLvl(int numTargets) {
        int targetsPerWall = numTargets / 4;
        for (int wall = 0; wall < walls.Count; wall++) { // for each wall
            List<Transform> wallCorners = new List<Transform>(4);
            if (wall == 0) { // WALL_1: 1, 2, 5, 6  --> Y axis free -- corner 1 max y
                wallCorners.Add(corners[0]);
                wallCorners.Add(corners[1]);
                wallCorners.Add(corners[4]);
                wallCorners.Add(corners[5]);
            } else if (wall == 1) { // WALL_2: 2, 4, 6, 8  --> X axis free
                wallCorners.Add(corners[1]);
                wallCorners.Add(corners[3]);
                wallCorners.Add(corners[5]);
                wallCorners.Add(corners[7]);
            } else if (wall == 2) { // WALL_3: 3, 4, 7, 8  --> Y axis free
                wallCorners.Add(corners[2]);
                wallCorners.Add(corners[3]);
                wallCorners.Add(corners[6]);
                wallCorners.Add(corners[7]);
            } else { // WALL_4: 1, 3, 5, 7  --> X axis free
                wallCorners.Add(corners[0]);
                wallCorners.Add(corners[2]);
                wallCorners.Add(corners[4]);
                wallCorners.Add(corners[6]);
            }
            for (int target = 0; target < targetsPerWall; target++) {
                Vector3 targetPosition;
                if (wall == 0) { // y free -- c2 min
                    targetPosition = new Vector3(wallCorners[0].position.x, Random.Range(wallCorners[0].position.y + 1, wallCorners[2].position.y - 1), 
                                                                        Random.Range(wallCorners[0].position.z + 2, wallCorners[1].position.z - 2));
                } else if (wall == 1) { // x free -- c2 minimum
                    targetPosition = new Vector3(Random.Range(wallCorners[0].position.x + 2, wallCorners[1].position.x - 2), 
                                                Random.Range(wallCorners[0].position.y + 1, wallCorners[2].position.y - 1), wallCorners[0].position.z);
                } else if (wall == 2) { // y free
                    targetPosition = new Vector3(wallCorners[0].position.x, Random.Range(wallCorners[0].position.y + 1, wallCorners[2].position.y - 1), 
                                                                        Random.Range(wallCorners[0].position.z + 2, wallCorners[1].position.z - 2));
                } else { // x free
                    targetPosition = new Vector3(Random.Range(wallCorners[0].position.x + 2, wallCorners[1].position.x - 2), 
                                                Random.Range(wallCorners[0].position.y + 1, wallCorners[2].position.y - 1), wallCorners[0].position.z);
                }
                GameObject targ = Instantiate(Target_Obj, targetPosition, walls[wall].rotation);
            }
        }
    }



    public static int getMissed() {
        return targetsMissed;
    }
    public static int getHit() {
        return targetsHit;
    }
    public static void setMissed(int missed) {
        targetsMissed = missed;
    }
    public static void setHit(int hit) {
        targetsHit = hit;
    }

    // Update is called once per frame
    void Update()
    {
        if (lvl_one && targetsHit == 12) {
            lvl_one = false;
            lvl_two = true;
            hit_one = targetsHit;
            missed_one = targetsMissed;
            acc_one = (float)targetsHit  / (float)(targetsHit + targetsMissed);
            time_one = Time.time - time_passed;
            Debug.Log("Targets Hit = " + targetsHit + "\nTargets Missed = " + targetsMissed);
            Debug.Log("Level One Accuracy = " + acc_one + "\nCompleted in " + time_one + " seconds.");
            targetsHit = 0;
            targetsMissed = 0;
            CreateLevelTwo();
        }
        if (lvl_two && targetsHit == 16) {
            lvl_two = false;
            lvl_three = true;
            hit_two = targetsHit;
            missed_two = targetsMissed;
            acc_two = (float)targetsHit / (float)(targetsHit + targetsMissed);
            time_two = Time.time - time_passed;
            Debug.Log("Targets Hit = " + targetsHit + "\nTargets Missed = " + targetsMissed);
            Debug.Log("Level Two Accuracy = " + acc_two + "\nCompleted in " + time_two + " seconds.");
            targetsHit = 0;
            targetsMissed = 0;
            CreateLevelThree();
        }
        if (lvl_three && targetsHit == 20) {//finished experiment
            lvl_three = false;
            hit_three = targetsHit;
            missed_three = targetsMissed;
            acc_three = (float)targetsHit / (float)(targetsHit + targetsMissed);
            time_three = Time.time - time_passed;
            Debug.Log("Targets Hit = " + targetsHit + "\nTargets Missed = " + targetsMissed);
            Debug.Log("Level Three Accuracy = " + acc_three + "\nCompleted in " + time_three + " seconds.");
            exp_done = true;
            targetsHit = 0;
            targetsMissed = 0;
        }
        if (exp_done) {
            //Do stuff with UI
            acc_ovr = (acc_one + acc_two + acc_three) / 3f;
            Debug.Log("Overall Accuracy = " + acc_ovr);
            List<string> s_one = new List<string>(4);
            List<string> s_two = new List<string>(4);
            List<string> s_three = new List<string>(4);
            List<string> s_total = new List<string>(4);
            s_one.Add(hit_one.ToString()); s_one.Add(missed_one.ToString()); s_one.Add(acc_one.ToString("#.###")); s_one.Add(time_one.ToString("#.###"));
            s_two.Add(hit_two.ToString()); s_two.Add(missed_two.ToString()); s_two.Add(acc_two.ToString("#.###")); s_two.Add(time_two.ToString("#.###"));
            s_three.Add(hit_three.ToString()); s_three.Add(missed_three.ToString()); s_three.Add(acc_three.ToString("#.###")); s_three.Add(time_three.ToString("#.###"));
            int total_hit = hit_one+hit_two+hit_three;
            int total_missed = missed_one+missed_two+missed_three;
            float total_time = time_one + time_two + time_three;
            s_total.Add(total_hit.ToString()); s_total.Add(total_missed.ToString()); s_total.Add(acc_ovr.ToString("#.###")); s_total.Add(total_time.ToString("#.###"));
            results.Add(s_one); results.Add(s_two); results.Add(s_three); results.Add(s_total);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(Scene.Results_Scene.ToString());
        }
    }
}
