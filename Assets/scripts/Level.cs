using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : MonoBehaviour {

	public static Level instance;

	public Tile start;
	public Tile end;
	Tile current;

	public GameObject playerPrefab;
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

	ArrayList movementHints = new ArrayList ();
	public GameObject movementHintPrefab;
	public GameObject teleportHintPrefab;

    //public int playerState = (int) PlayerState.stationary;

	public void Start() {
		Level.instance = this;
		Debug.Log ("Start is " + start.name);
		Debug.Log ("End is " + end.name);

		this.current = start;
		this.total_prisoners = this.prisoners.Length;
		createMovementHints ();

		/* Create the player located at start */
	}
	public void destoryMovementHints() {
		/*TODO: If movement hints already exists signal them transition out and remove from the array*/
		foreach (GameObject obj in movementHints) {
			GameObject.Destroy (obj);		
		}
	}

	public void createMovementHints() {
		Quaternion right = Quaternion.Euler (0, 270, 0);
		Quaternion up = Quaternion.Euler (0, 0, 0);
		Quaternion left = Quaternion.Euler (0, 90, 0);
		Quaternion down = Quaternion.Euler (0, 180, 0);
		destoryMovementHints ();

		var candidates = this.current.candidateMoves ();
		foreach (var candidate in candidates) {
			if (candidate.type != MovementType.TELEPORT) {
				GameObject hint = GameObject.Instantiate (movementHintPrefab);
				Vector3 position = candidate.tile.transform.position;
				position.y += 0.2f;
				hint.transform.position = position;
				movementHints.Add (hint);

				switch (candidate.type) {
				case MovementType.RIGHT:
					hint.transform.rotation = right;
					break;
				case MovementType.DOWN:
					hint.transform.rotation = down;
					break;
				case MovementType.LEFT:
					hint.transform.rotation = left;
					break;
				case MovementType.UP:
					hint.transform.rotation = up;
					break;
				}
			} else {
				GameObject hint = GameObject.Instantiate (teleportHintPrefab);
				Vector3 position = candidate.tile.transform.position;
				position.y += 1.0f;
				hint.transform.position = position;
				movementHints.Add (hint);
			}
		}
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

				destoryMovementHints ();
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
		createMovementHints ();
    }
}
