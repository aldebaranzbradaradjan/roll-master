using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script de l'ui et des outils de création de carte
/// </summary>

public class ui : MonoBehaviour
{
  public int mode = 0;
  public GameObject prefab;
  public bool moveFlag;
  public bool overGUI;
  public bool gridmode = true;
  public RaycastHit2D target;
  public Vector3 mousePosition;
  public Vector3 mousePos;
  public Vector2 mousePos2D;

  void Start()
  {
    gridmode = true;
  }

  void Update()
  {
    overGUI = EventSystem.current.IsPointerOverGameObject();
	mousePosition = Input.mousePosition;
	mousePos = Camera.main.ScreenToWorldPoint(mousePosition);

	if (gridmode) {
	  mousePos2D = new Vector2 (Mathf.Round (mousePos.x), Mathf.Round (mousePos.y));
	}
	else {
	  mousePos2D = new Vector2 (mousePos.x, mousePos.y);
	}
	
	if (Input.GetButtonDown ("Fire1") && !overGUI) {
		if (mode == 0) {
			Instantiate(prefab, mousePos2D, Quaternion.identity);
		}

		else if (mode == 1) {
			target = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (target.collider != null) {
				Debug.Log(target.collider.gameObject.name);
				Destroy(target.collider.gameObject);
			}
		}
		else if (mode == 2 && moveFlag == false) {
			target = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (target.collider != null) {
				Debug.Log(target.collider.gameObject.name);
				moveFlag = true;
			}
		}
	}

	if (Input.GetButtonUp ("Fire1")) {
		moveFlag = false;
	}

	if (moveFlag) {
		if (target.collider != null) {
			Debug.Log (target.collider.gameObject.name);
			target.collider.transform.position = mousePos2D;
		}
	}
  }

  public void add(string buttonName)
  {
    mode = 0;
  }
  public void remove(string buttonName)
  {
    mode = 1;
  }
  public void move(string buttonName)
  {
    mode = 2;
  }

  public void gridToggle(string buttonName)
  {
	gridmode = !gridmode;
  }

}

/*

to do :

- écrire une meilleure fonction pour la selection et le déplacement d'un token (do)

- ajouter une notion de grille et une touche pour faire du positionnement fin (do ez)
  (positionnement fin = système actuel)

- construire une ihm permetant de selectionner une prefab dans un répertoire
  et l'utiliser dans ce script à la place de la prefab linkée

- encapsuler les prefabs dans une classe token ?

- ajouter une option pour modifier la visibilité d'un token, ajouter la notion
  de calque

- ajouter les fonctions charger/sauvegarder la scene dans un répertoire cible

*/
