using System.Collections;
using System.Collections.Generic;
using System;
//using System.IO;
using UnityEngine;

public class GUIBehaviour : MonoBehaviour {
	/*GUI Behaviour

	Simple class holding references to pieces of GUI so we can manipulate them
	easier.

	Also acts as a listener for our GUI manager so we can later have it posted
	to our player log files as well

	*/

	public GUIManager _guiManager;

	List<string> sessionTextLog;
	List<string> sessionCombatLog;

	public RectTransform _textLog;
	public RectTransform _combatLog;


	void Awake() {
		sessionTextLog = new List<string>();
		sessionCombatLog = new List<string>();

		DateTime today = DateTime.Now;

		sessionTextLog.Add("Log-" + today.Date + "-" + DateTime.Now);
		sessionCombatLog.Add("Combat-" + today.Date + "-" + DateTime.Now);
	}


	public void ReceiveMessage(string message, bool combatLog) {
		_guiManager.PostMessage(message, combatLog);

		if (combatLog) {
			AddToLog(message, true);
		} else {
			AddToLog(message, false);
		}

	}


	void AddToLog(string message, bool combatLog) {
		message = DateTime.Now.TimeOfDay + " : " + message;
		if (combatLog) {
			sessionCombatLog.Add(message);
		} else {
			sessionTextLog.Add(message);
		}
	}

	/* FIXME: Eventually we'll want to be able to export our logs to files.
		But for now, let's just aim to have it display in a GUI in game!

	void ExportToLog(List<string> logToExport) {
		string logName = DateTime.Today.ToString("MM-dd-yyyy");
		string logText = "";
		string logPath = Application.dataPath + "/Logs/" + logName;

		if (!AssetDatabase.IsValidFolder(Application.dataPath + "/Logs")) {
			AssetDatabase.CreateFolder(Application.dataPath, "Logs");
		}

		for (var x = 1; x < logToExport.Count; x++) {
			logText = logText + "\n" + logToExport[x];
		}

		if (!File.Exists(logPath)) {
			File.Create(logPath);
		}

		using (StreamWriter streamWriter = new StreamWriter(logPath, false)) {
			streamWriter.Write(logText);
		}
	} */

}
