using System;
using System.Collections.Generic;

namespace LimeServer.DatabaseStructure
{
	//Databasstrukturer
	public class TableEnumeration
	{
		public string Text { get; set; }
		public string Key { get; set; }
		public string Id { get; set; }
	}

	public class FieldData
	{
		public string FieldName { get; set; }
		public string TranslatedName { get; set; }
		public List<TableEnumeration> Enumerations { get; set; }
	}

	public class TableData
	{
		public string TableName { get; set; }
		public string TranslatedName { get; set; }
		public string TranslatedNamePlural { get; set; }
		public List<FieldData> Fields { get; set; }
	}

	public class DatabaseData
	{
		public List<TableData> TableData { get; set; }

		public List<string> GetTranslatedFields(ServerQueryType queryType)
		{
			string tableName = LimeHelper.GetTableName(queryType);
			List<string> fields = LimeHelper.GetFields(queryType);
			List<string> hideFields = LimeHelper.GetHideFields(queryType);
			hideFields.Add("id" + tableName);
			for(int i = 0; i < hideFields.Count; i++)
			{
				fields.Remove(hideFields[i]);
			}

			NavigationCore.ServerTranslator.TranslateEnum TranslatorEnum = NavigationCore.ServerTranslator.TranslateEnum.None;

			switch(queryType)
			{
				case ServerQueryType.CardClientData:
					TranslatorEnum = NavigationCore.ServerTranslator.TranslateEnum.CardClient;
				break;
			}


			for(int i = 0; i < fields.Count; i++)
			{
				fields[i] = GetFieldName(tableName, fields[i]);
				if(TranslatorEnum != NavigationCore.ServerTranslator.TranslateEnum.None)
					fields[i] = NavigationCore.ServerTranslator.TranslateField(TranslatorEnum, fields[i]);
			}
			return fields;
		}

		public string GetFieldName(string tableName, string fieldName)
		{
			string[] splitted = fieldName.Split('.');
			if(splitted.Length > 1)
			{
				string nameOfField = GetFieldName(splitted[0], fieldName.Substring(splitted[0].Length + 1));
				if(splitted.Length == 2)
				{
					nameOfField += " (" + GetTableName(splitted[0], false).ToLower() + ")";
				}

				return nameOfField;
				//return GetFieldName(tableName, splitted[0]);
			}

			TableData tableData = TableData.Find(delegate(TableData table) {
				return table.TableName == tableName;
			});
			if(tableData != null)
			{
				FieldData fieldData = tableData.Fields.Find(delegate(FieldData field) {
					return field.FieldName == fieldName;
				});
				if (fieldData != null)
					return fieldData.TranslatedName;
			}
			return fieldName;
		}
		public string GetTableName(string tableName, bool plural)
		{
			TableData tableData = TableData.Find(delegate(TableData table) {
				return table.TableName == tableName;
			});
			if(tableData != null)
			{
				if(plural)
					return tableData.TranslatedNamePlural;
				else
					return tableData.TranslatedName;
			}
			return tableName;

		}
		public string GetEnumerationName(string tableName, string fieldName, string enumerationId)
		{
			TableData tableData = TableData.Find(delegate(TableData table) {
				return table.TableName == tableName;
			});
			if(tableData != null)
			{
				FieldData fieldData = tableData.Fields.Find(delegate(FieldData field) {
					return field.FieldName == fieldName;
				});
				if(fieldData != null)
				{
					TableEnumeration tableEnumeration = fieldData.Enumerations.Find(delegate(TableEnumeration enumeration) {
						return enumeration.Id == enumerationId;
					});
					if (tableEnumeration != null)
						return tableEnumeration.Text;
				}
			}
			return "";
		}
		public List<string> GetEnumerationFields(string tableName)
		{
			List<string> fieldNames = new List<string>();

			TableData tableData = TableData.Find(delegate(TableData table) {
				return table.TableName == tableName;
			});
			if(tableData != null)
			{
				List<FieldData> enumerationFields = tableData.Fields.FindAll(delegate(FieldData field) {
					return field.Enumerations.Count > 0;
				});
				if(enumerationFields != null)
				{
					for(int i = 0; i < enumerationFields.Count; i++)
					{
						fieldNames.Add(enumerationFields[i].FieldName);
					}
				}
			}
			return fieldNames;
		}
		public List<TableEnumeration> GetEnumerationValues(string tableName, string fieldName)
		{
			List<TableEnumeration> enumerations = null;
			TableData tableData = TableData.Find(delegate(TableData table) {
				return table.TableName == tableName;
			});
			if(tableData != null)
			{
				FieldData fieldData = tableData.Fields.Find(delegate(FieldData field) {
					return field.FieldName == fieldName;
				});
				if(fieldData != null)
				{
					enumerations = fieldData.Enumerations;
				}
			}
			return enumerations;
		}
		/*
		public NavigationCore.CardClientData GetEmptyCompany()
		{
			ServerQueryType queryType = ServerQueryType.CardClientData;

			List<TableEnumeration> segments = LimeServer.LimeHelper.DatabaseData.GetEnumerationValues("company", "creditwarning");
			TableEnumeration standardCreditWarning = segments[0];

			string tableName = LimeHelper.GetTableName(queryType);
			List<string> fields = LimeHelper.GetFields(queryType);
			List<string> hideFields = LimeHelper.GetHideFields(queryType);
			hideFields.Add("id" + tableName);
			int nrOfShownFields = LimeXMLParser.GetNrOfShown(fields, hideFields);

			NavigationCore.CardClientData emptyClient = new NavigationCore.CardClientData() { FieldData = new string[2, nrOfShownFields] };

			int fieldCount = 0;
			for(int i = 0; i < fields.Count; i++)
			{
				bool hideField = false;
				for(int j = 0; j < hideFields.Count; j++)
				{
					if(hideFields[j] == fields[i])
					{
						hideField = true;
						break;
					}
				}
				if(hideField)
					continue;

				string fieldNameToAsk = fields[i];


				string fieldName = fields[i];
				string fieldValue = "";

				if (LimeHelper.DatabaseData != null)
				{
					fieldName = LimeHelper.DatabaseData.GetFieldName(tableName, fieldName);
					fieldName = NavigationCore.ServerTranslator.TranslateField(NavigationCore.ServerTranslator.TranslateEnum.CardClient, fieldName);
				}

				if(fieldName == "Kreditvarning")
				{

					fieldValue = standardCreditWarning.Text;
				}

				emptyClient.FieldData[0, fieldCount] = fieldName;
				emptyClient.FieldData[1, fieldCount] = fieldValue;

				fieldCount++;
			}
			emptyClient.CompanyId = "-1";
			emptyClient.CreditWarning = standardCreditWarning.Id;
			return emptyClient;

		}
		*/
	}
}

