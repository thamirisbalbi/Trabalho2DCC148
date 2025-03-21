using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //recarrega a cena ativa

    }

    public void Quit()
    {
        SceneManager.LoadScene("StartScene");
        
    }
}
