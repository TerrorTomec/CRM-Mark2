using System;
using System.Collections.Generic;

namespace NavigationCore
{
	public static class Translator
	{
		#region Translation for all UI from server.
		private static readonly Dictionary<string, Dictionary<string, string>> TranslationDictionary = new Dictionary<string, Dictionary<string, string>>();

		public static void AddTranslation(string langage, string code, string translation)
		{
			if(langage == null || code == null || translation == null)
				return;

			if(!TranslationDictionary.ContainsKey(langage))
			{
				TranslationDictionary.Add(langage, new Dictionary<string, string>());
			}
			TranslationDictionary[langage].Add(code, translation);
		}

		public static string GetTranslation(string language, string code)
		{
			if(TranslationDictionary.ContainsKey(language))
			{
				var dictionaryWithLanguage = TranslationDictionary[language];
				if(dictionaryWithLanguage.ContainsKey(code))
				{
					return dictionaryWithLanguage[code];
				}
				else
					Console.WriteLine("code: \"" + code + "\" does not exist.");
			}
			else
				Console.WriteLine("Language: \"" + language + "\" does not exist.");
			return null;

		}
		#endregion
	}
}

