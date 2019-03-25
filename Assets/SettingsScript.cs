using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartOnClick()
    {
        TimerSettings.DeploymentMinutes = int.Parse(GameObject.Find("DeploymentInput").GetComponent<InputField>().text);
        TimerSettings.Round1Minutes = int.Parse(GameObject.Find("Round1Input").GetComponent<InputField>().text);
        TimerSettings.Round2Minutes = int.Parse(GameObject.Find("Round2Input").GetComponent<InputField>().text);
        TimerSettings.Round3Minutes = int.Parse(GameObject.Find("Round3Input").GetComponent<InputField>().text);

        SceneManager.LoadScene("Timer");
    }
}
