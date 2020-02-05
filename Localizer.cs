using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Localizer
{
	public static ITranslationObject[] Translations;
	public static ITranslationObject[] SharedTranslations;

	public static void StartLocalizer()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		UpdateLabels();
	}

	public static void UpdateLabels()
	{
		LoadTranslations();
		UILabel[] Labels = Object.FindObjectsOfType<UILabel>();

		if (Labels.Length <= 0)
		{
			Debug.LogError("No UILabels found in the scene!");
		}
		else if (Translations == null || Translations.Length == 0)
		{
			Debug.LogError("The Translations Array is null or empty !");
		}
		InitializeTranslators(Labels);
	}

	public static void LoadTranslations()
	{
		if (SharedTranslations == null)
		{
			SharedTranslations = LocalizerJSON.LoadFromJSON(Application.streamingAssetsPath + "/Yandere_Next/Mods/default/Localization/French/Shared/Localization.json");
		}
		try
		{
			Translations = LocalizerJSON.LoadFromJSON(Application.streamingAssetsPath + "/Yandere_Next/Mods/default/Localization/French/" + SceneManager.GetActiveScene().name + "/Localization.json").Union(SharedTranslations).ToArray();
		}
		catch
		{
			Translations = SharedTranslations;
		}
	}

	static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateLabels();
	}

	static void InitializeTranslators(UILabel[] Labels)
	{
		foreach (UILabel label in Labels)
		{
			Translator ts = label.gameObject.GetComponent<Translator>();
			if (ts != null)
			{
				ts.Label = label;
			}
			else
			{
				ts = label.gameObject.AddComponent<Translator>();
				ts.Label = label;
			}
		}
	}
}
