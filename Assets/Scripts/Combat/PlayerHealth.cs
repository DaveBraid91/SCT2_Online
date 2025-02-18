using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : BaseHealth
{
    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
