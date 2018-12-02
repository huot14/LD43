using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour {

	public static Level instance;

	public Tile start;
	public Tile end;
	Tile current;

	public Transform player;

	public GameObject[] prisoners;

	/*Statistics*/
	public int actionsPerformed = 0;
	public int total_prisoners = 0;
	public int killed_prisoners = 0;
	public int traps_triggered = 0;

    enum PlayerState
    {
        STATIONARY,
        MOVING,
        TELEPORTING
    }

    PlayerState playerState = PlayerState.STATIONARY;

    //public int playerState = (int) PlayerState.stationary;

	public void Start() {
		Level.instance = this;
		Debug.Log ("Start is " + start.name);
		Debug.Log ("End is " + end.name);

		this.current = start;
		this.total_prisoners = this.prisoners.Length;

		/* Create the player located at start */
	}

	public bool validMovement(Tile to) {
		var candidates = current.candidateMoves ();
		bool valid = false;
		foreach (ValidMove candidate in candidates) {
			if (candidate.tile == to) {
				valid = true;
				break;
			}
		}

		return valid;
	}

	public bool movePlayer(Tile to) {

        if (playerState == PlayerState.STATIONARY)
        {
            if (this.validMovement(to))
            {
                playerState = PlayerState.MOVING;
                /*TODO: FIX THIS UP*/
                this.actionsPerformed++;
                Vector3 newPosition = to.transform.position;
                newPosition.y = this.player.position.y;
                this.current = to;

                StartCoroutine(MoveWithSpeed(this.player.gameObject, newPosition, 1f));

                /*Trigger Traps!*/
                var traps = to.GetComponents<Trap>();
                foreach (var trap in traps)
                {
                    if (!trap.activated())
                    {
                        trap.activate();
                    }
                }


                return true; ;
            }
        }
		return false;
	}


    IEnumerator MoveWithSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        while(objectToMove.transform.position != end)
        {
            objectToMove.transform.LookAt(end);
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        playerState = PlayerState.STATIONARY;
    }
}
