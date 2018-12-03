using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ScoreCalculator {
	int score ();
}

[System.Serializable]
public class Level : MonoBehaviour {

	public static Level instance;

	public Tile start;
	public Tile end;
	Tile current;

	public GameObject playerPrefab;
	public Transform player;

	/*This Object MUST implement ScoreCalculator*/
	public Object scoreCalculator;
	public int score {
		get {
			if (scoreCalculator != null) {
				var calc = scoreCalculator as ScoreCalculator;
				return calc.score ();
			}
			return 0;
		}
	}
	public GameObject[] prisoners;
	int prisoners_idx = 0;
    public GameObject trapMarkerPrefab;

	/*Statistics*/
	public int actionsPerformed = 0;
	public int total_prisoners = 0;
	int _killed_prisoners = 0;
	public int killed_prisoners {
		get {return _killed_prisoners;}
		set {
			if (value <= total_prisoners) {
				_killed_prisoners = total_prisoners;
			}
		}
	}

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
		this.total_prisoners = this.prisoners.Length;

		Debug.Log ("Start is " + start.name);
		Debug.Log ("End is " + end.name);
		Debug.Log ("Score is " + this.score);

		this.current = start;
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

        if (playerState != PlayerState.MOVING)
        {
            if (this.validMovement(to))
            {
				Tile _from = this.current;
                // If we are already on a teleporter and want to teleport back
                if(to.GetComponent<Teleporter>() != null && current.GetComponent<Teleporter>() != null)
                {
                    playerState = PlayerState.TELEPORTING;
                }

                /*TODO: FIX THIS UP*/
                this.actionsPerformed++;
                Vector3 newPosition = to.transform.position;
                newPosition.y = this.player.position.y;
                this.current = to;

				destoryMovementHints ();

                if (playerState == PlayerState.STATIONARY)
                {
                    playerState = PlayerState.MOVING;
					StartCoroutine(MoveWithSpeed(this.player.gameObject, newPosition, 1f, _from, to));
                }
                else if(playerState == PlayerState.TELEPORTING)
                {
                    StartCoroutine(Teleport(this.player.gameObject, newPosition, to));
                }

                    return true;
            }
        }
		return false;
	}


    IEnumerator MoveWithSpeed(GameObject objectToMove, Vector3 end, float speed, Tile _from, Tile to)
    {
        while(objectToMove.transform.position != end)
        {
            objectToMove.transform.LookAt(end);
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        playerState = PlayerState.STATIONARY;
		createMovementHints ();

        // Check if we have arrived at a teleporter, automatically teleport to the connected tile
        var teleporter = to.GetComponent<Teleporter>();
        if (teleporter != null)
        {
            Tile dest = teleporter.destination();
            playerState = PlayerState.TELEPORTING;
            movePlayer(dest);
        }

        /*Trigger Traps!*/
        var traps = to.GetComponents<Trap>();
        foreach (var trap in traps)
        {
            if (!trap.activated())
            {
                trap.activate();
				this.killPrisoner ();
                markTrap(to);
            }
        }

		/*Color Transition Logic*/
		var maybeToColor = to.GetComponent<TileColor> ();
		var maybeToTransition = to.GetComponent<TileTransition> ();

		var maybeFromColor = _from.GetComponent<TileColor> ();
		var maybeFromTransition = _from.GetComponent<TileTransition>();

		if (maybeToTransition != null) {
			var toTransition = maybeToTransition;
			if (maybeFromColor != null) {
				var fromColor = maybeFromColor;
				bool allowed = toTransition.isAllowed (fromColor.color);
				if (!allowed) {
					this.killPrisoner ();
				}
			}
		} else if (maybeFromTransition != null) {
			var fromTransition = maybeFromTransition;
			if (maybeToColor != null) {
				var toColor = maybeToColor;
				bool allowed = fromTransition.isAllowed (toColor.color);
				if (!allowed) {
					this.killPrisoner ();
				}
			}
		} else if (maybeFromColor != null && maybeToColor != null) {
			if (maybeFromColor.color.color != maybeToColor.color.color) {
				this.killPrisoner ();
			}
		}
			
    }
	

	public void killPrisoner() {
		if (this.prisoners_idx < this.prisoners.Length) {
			prisoners[prisoners_idx].GetComponent<Prisoner>().kill();
			killed_prisoners++;
			this.prisoners_idx++;
		}
	}

    IEnumerator Teleport(GameObject objectToMove, Vector3 end, Tile to)
    {
        while (objectToMove.transform.position != end)
        {
            // TODO: Create teleporting effect
            objectToMove.transform.position = end;
            yield return new WaitForEndOfFrame();
        }

        playerState = PlayerState.STATIONARY;
        createMovementHints();
    }

    void markTrap(Tile tile)
    {
        GameObject trapMarker = GameObject.Instantiate(trapMarkerPrefab);
        Vector3 position = tile.transform.position;
        position.y += 0.14f;
        trapMarker.transform.position = position;
    }
}
