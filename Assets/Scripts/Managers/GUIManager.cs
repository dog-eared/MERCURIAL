using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	/* GUI MANAGER

	Main manager for our GUI, handling logs, health/shields, communications, etc.
	All non-intuitive kinds of feedback should go through this manager!

	As a general rule, this manager should have functions that receive data and
	translate them into the GUI -- it shouldn't directly receive the data.

	--Contains a reference to our GUI organizing GameObject
	--Contains references to hull/shield and methods for updating them to be
	called upon damage/healing.
	--Contains references for our log objects and methods for updating them,
	hiding and re-arranging them.
	--Contains long version of our log as well.

	*/

	[Header("References:")]

	public GameObject _guiGameObject;

	[Space(5)]
	public Text _textLog;
	public Text _combatLog;
	[Space(5)]
	public Image _hullBar;
	public Image _shieldBar;

	//lists of 4 items maximum that we'll actually display
	List<string> textLogToDisplay;
	List<string> combatLogToDisplay;

	[Space(10)]
	[Header("Configuration:")]
	public float fadeSpeed = 10f;
	public float idleFadeOutTime = 4f;

	float textLogFade = 1;

	GUIBehaviour _guiBehaviour;

	int guiState; //0 = bottom, 1 = top, 2 = off

	IEnumerator currentTextFade;
	IEnumerator currentCombatFade;


	void Awake() {
		_guiBehaviour = _guiGameObject.GetComponent<GUIBehaviour>();
		combatLogToDisplay = new List<string>();
		textLogToDisplay = new List<string>();

		combatLogToDisplay.Add("");
		textLogToDisplay.Add("");

		Invoke("AutoFade", idleFadeOutTime);
	}


	public void PostMessage(string message, bool combatLog = false) {
		KillCurrentFades();

		//Expand later to log more data
		string newLogText;

		if (combatLog) {

			FadeLogs(false, true, 1);

			newLogText = combatLogToDisplay[0];
			combatLogToDisplay.Add(message);
			if (combatLogToDisplay.Count > 3) {
				combatLogToDisplay.RemoveAt(0);
			}

			for (int x = 0; x < combatLogToDisplay.Count; x++) {
				newLogText = newLogText + "\n" + combatLogToDisplay[x];
			}

			_combatLog.text = newLogText;

		} else {
			FadeLogs(true, false, 1);
			newLogText = textLogToDisplay[0];
			textLogToDisplay.Add(message);
			if (textLogToDisplay.Count > 3) {
				textLogToDisplay.RemoveAt(0);
			}

			for (int x = 0; x < textLogToDisplay.Count; x++) {
				newLogText = newLogText + "\n" + textLogToDisplay[x];
			}

			_textLog.text = newLogText;
		}
		CancelInvoke();
		Invoke("AutoFade", idleFadeOutTime);
	}


	void FadeLogs(bool fadeText, bool fadeCombat, float target) {
		if (fadeText) {
			currentTextFade = FadeLogCoroutine(_textLog, target, fadeSpeed);
			StartCoroutine(currentTextFade);
		}

		if (fadeCombat) {
			currentCombatFade = FadeLogCoroutine(_combatLog, target, fadeSpeed);
			StartCoroutine(currentCombatFade);
		}

	}


	void KillCurrentFades() {
		//So we can interrupt these if we get a new msg before invoke fires
		if (currentTextFade != null) {
			StopCoroutine(currentTextFade);
		}
		if (currentCombatFade !=null) {
			StopCoroutine(currentCombatFade);
		}
	}


	public void AutoFade() {
		//Automatically fade both down after x number of secs
		KillCurrentFades();
		FadeLogs(true, true, 0);
	}


	public void MoveLogs() {
		guiState++;
		if (guiState > 2) {
			guiState = 0;
		}

		if (guiState == 0) {
			FadeLogs(true, true, 1);
			//NOT A FIXME: This is ugly and not DRY, but Unity's UI is finicky enough
			//that I'd prefer to be as explicit as possible when moving UI options.
			_guiBehaviour._textLog.pivot = new Vector2(0.5f, 0f);
			_guiBehaviour._textLog.anchorMin = new Vector2(0f, 0f);
			_guiBehaviour._textLog.anchorMax = new Vector2(0.5f, 0f);
			_guiBehaviour._textLog.anchoredPosition = new Vector2(40, 20);

			_guiBehaviour._combatLog.pivot = new Vector2(1f, 0f);
			_guiBehaviour._combatLog.anchorMin = new Vector2(0.5f, 0f);
			_guiBehaviour._combatLog.anchorMax = new Vector2(1f, 0f);
			_guiBehaviour._combatLog.anchoredPosition = new Vector2(40, 20);
		} else if (guiState == 1) {
			FadeLogs(true, true, 1);
			_guiBehaviour._textLog.pivot = new Vector2(0.5f, 1f);
			_guiBehaviour._textLog.anchorMin = new Vector2(0f, 1f);
			_guiBehaviour._textLog.anchorMax = new Vector2(0.5f, 1f);
			_guiBehaviour._textLog.anchoredPosition = new Vector2(40, 0);

			_guiBehaviour._combatLog.pivot = new Vector2(1f, 1f);
			_guiBehaviour._combatLog.anchorMin = new Vector2(0.5f, 1f);
			_guiBehaviour._combatLog.anchorMax = new Vector2(1f, 1f);
			_guiBehaviour._combatLog.anchoredPosition = new Vector2(40, 0);
		} else {
			FadeLogs(true, true, 0);
		}
	}


	IEnumerator FadeLogCoroutine(Text targetLog, float target, float fadeSpeed) {
		//Coroutine for fading the text in/out. Target will usually be 0/1, but
		//maybe we'll have partial fades at some point.
		Color changedColor = targetLog.color;
		float startAlpha = textLogFade;
		float elapsedTime = 0;

		while (targetLog.color.a != target) {
			changedColor.a = Mathf.Lerp(startAlpha, target, elapsedTime);
			elapsedTime += Time.deltaTime * fadeSpeed;
			textLogFade = changedColor.a;
			targetLog.color = changedColor;
			yield return null;
		}


	}


	public void SetBarAmount(string bar, float fillPercentage) {
		if (bar == "hull") {
			_hullBar.fillAmount = fillPercentage;
		} else if (bar == "shield") {
			_shieldBar.fillAmount = fillPercentage;
		}
	}

}
