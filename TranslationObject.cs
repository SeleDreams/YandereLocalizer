public class TranslationObject : ITranslationObject
{
	public TranslationObject(string text, string translation)
	{
		Text = text;
		Translation = translation;
	}
	public string Translate(string text)
	{
		if (!string.IsNullOrEmpty(text) && text.Length == Text.Length && text == Text)
		{
			return Translation;
		}
		else
		{
			return null;
		}
	}

	readonly string Text;
	readonly string Translation;
}

