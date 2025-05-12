//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class HealthbarPlatforming : MonoBehaviour
//{
//    [SerializeField] private Health playerHealth;
//    [SerializeField] private Image totalhealthBar;
//    [SerializeField] private Image currenthealthBar;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Ensure the health bar starts based on the current health in the platforming scene
//        totalhealthBar.fillAmount = playerHealth.currentHealth / 10f;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Update the current health bar
//        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f;
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class HealthbarPlatforming : MonoBehaviour
//{
//    [SerializeField] private Health playerHealth;
//    [SerializeField] private Image totalhealthBar;
//    [SerializeField] private Image currenthealthBar;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Set the health bar to be decreased by a tenth of the current health
//        totalhealthBar.fillAmount = playerHealth.currentHealth / 10f;

//        // If you want to ensure the health bar starts already decreased by a tenth, 
//        // you can directly modify the value like this:
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Update the current health bar
//        if(currenthealthBar != null){
//            currenthealthBar.fillAmount = playerHealth.currentHealth / 10f;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class HealthbarPlatforming : MonoBehaviour
{
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    void Start()
    {
        // Set initial happiness bars
        //totalhealthBar.fillAmount = HappinessManager.maxHappiness / 10f;
        totalhealthBar.fillAmount = HappinessManager.maxHappiness;
        currenthealthBar.fillAmount = HappinessManager.currentHappiness / 10f;
    }

    void Update()
    {
        // Continuously reflect current happiness
        currenthealthBar.fillAmount = HappinessManager.currentHappiness / 10f;
    }
}
