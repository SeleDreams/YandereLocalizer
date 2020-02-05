using System.Collections.Generic;
using System.Text.RegularExpressions;

public class RegexObject : ITranslationObject
{
	public RegexObject(string regex, string translation)
	{
		Regex = new Regex(regex);
		Translation = translation;
	}

	public string Translate(string text)
	{
		if (Regex.IsMatch(text))
		{
			MatchCollection TranslationVariablesCollection = TranslationVariablesRegex.Matches(Translation);
			Match RegexMatch = Regex.Match(text);

			var Variables = new Dictionary<int, string>();

			// Creates a dictionnary with all the variables of the translation ordered by id.
			foreach (Match Match in TranslationVariablesCollection)
			{
				Variables.Add(int.Parse(Match.Groups[2].Value), Match.Groups[1].Value);
			}
			// Replaces all the variables by their value in the translation
			string NewTranslation = Translation;
			for (int VariableID = 0; VariableID < RegexMatch.Groups.Count; VariableID++)
			{
				if (Variables.ContainsKey(VariableID))
				{
					NewTranslation = NewTranslation.Replace(Variables[VariableID], RegexMatch.Groups[VariableID].Value);
				}
			}
			return NewTranslation;
		}
		else
		{
			return null;
		}
	}


	public Regex TranslationVariablesRegex = new Regex("(\\${MATCH(\\d)})");
	public Regex Regex;
	public string Translation;
}
