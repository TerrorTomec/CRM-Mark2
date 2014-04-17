using System;
using System.Collections.Generic;

namespace NavigationCore
{
	public static class ServerTranslator
	{
		#region Custom translation for fields and tables on server.
		internal struct TableData
		{
			public Dictionary<string, string> FieldDictionary { get; set; }
		}

		internal struct TableNameData
		{
			public string PluralName { get; set; }
			public string SingularName { get; set; }
		}

		private static Dictionary<TranslateEnum, TableData> FieldTranslaterDictionary = new Dictionary<TranslateEnum, TableData>();
		private static Dictionary<string, TableNameData> TableTranslaterDictionary = new Dictionary<string, TableNameData>();
		private static bool dataFilled = false;

		/// <summary>
		/// Translates the field in views for example: in "kundkort" "Fakturaadress 1" => "Fakturaadress".
		/// </summary>
		/// <returns>The fieldName</returns>
		/// <param name="tableName">Table name. example: "kundkort"</param>
		/// <param name="fieldName">Field name. example: "Fakturaadress 1"</param>
		public static string TranslateField(TranslateEnum type, string fieldName)
		{
			if(!dataFilled)
			{
				FillAllData();
			}


			string translatedString = fieldName;

			if(FieldTranslaterDictionary.ContainsKey(type))
			{
				TableData table = FieldTranslaterDictionary[type];
				if(table.FieldDictionary.ContainsKey(fieldName))
				{
					translatedString = table.FieldDictionary[fieldName];
				}
			}

			return translatedString;
		}


		public static bool ContainsTable(string tableName)
		{
			if(!dataFilled)
			{
				FillAllData();
			}

			return TableTranslaterDictionary.ContainsKey(tableName);
		}

		public static bool ContainsField(TranslateEnum type, string fieldName)
		{
			if(!dataFilled)
			{
				FillAllData();
			}

			return (FieldTranslaterDictionary.ContainsKey(type) && FieldTranslaterDictionary[type].FieldDictionary.ContainsKey(fieldName));
		}

		/// <summary>
		/// Translates the table in databaseTranslator for example: "Medarbetare" => "Säljare"
		/// </summary>
		/// <returns>The tableName.</returns>
		/// <param name="tableName">Table name to translate.</param>
		/// <param name="isPlural">If set to <c>true</c> pluralName.</param>
		public static string TranslateTable(string tableName, bool isPlural)
		{
			if(!dataFilled)
			{
				FillAllData();
			}


			string translatedTableName = tableName;
			if(TableTranslaterDictionary.ContainsKey(tableName))
			{
				if(isPlural)
					translatedTableName = TableTranslaterDictionary[tableName].PluralName;
				else
					translatedTableName = TableTranslaterDictionary[tableName].SingularName;

			}
			return translatedTableName;
		}

		private static void FillTableData()
		{
			TableTranslaterDictionary.Add("coworker", new TableNameData() {PluralName = "Säljare", SingularName = "Säljare"});
		}

		public enum TranslateEnum
		{
			None,

			CardClient,
			PriceGroups,
		};

		private static void FillFieldData()
		{
			TableData tableData = new TableData();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Fakturaadress 1", "Fakturaadress");
			dictionary.Add("Leveransadress 1", "Leveransadress");
			dictionary.Add("Namn (land)", "Land");
			dictionary.Add("Namn (företagskategori)", "Företagskategori");
			dictionary.Add("Kundnamn (leverans)", "Företagsnamn (leverans)");
			dictionary.Add("Namn (distrikt)", "Distrikt");
			dictionary.Add("Littranummer", "Referens-/littranr");
			tableData.FieldDictionary = dictionary;
			FieldTranslaterDictionary.Add(TranslateEnum.CardClient, tableData);


			tableData = new TableData();
			dictionary = new Dictionary<string, string>();
			dictionary.Add("Prisgrupp 1", "A+");
			dictionary.Add("Prisgrupp 2", "A");
			dictionary.Add("Prisgrupp 3", "B");
			dictionary.Add("Prisgrupp 4", "C");
			dictionary.Add("Prisgrupp 5", "D");
			dictionary.Add("Prisgrupp 6", "E");
			tableData.FieldDictionary = dictionary;
			FieldTranslaterDictionary.Add(TranslateEnum.PriceGroups, tableData);

			dataFilled = true;
		}

		private static void FillAllData()
		{
			FillFieldData();
			FillTableData();

			dataFilled = true;
		}
		#endregion


	}
}

