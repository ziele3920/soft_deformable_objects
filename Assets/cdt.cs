using UnityEngine;

public class cdt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter() {

        Debug.Log("collision");
    }

    private void OnTriggerEnter() {
        Debug.Log("collision");

    }
}
