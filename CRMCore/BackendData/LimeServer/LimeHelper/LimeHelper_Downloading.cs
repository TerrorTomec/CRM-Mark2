using System;
using System.Collections.Generic;

namespace LimeServer
{
	public partial class LimeHelper //Downloading Queries
	{
		/*
		// Nerladdning av data
		public static List<string> GetDistrictCodes()
		{
			List<string> districtCodes = new List<string>();
			if(LoggedInCoworker != null && LoggedInCoworker.Districts != null)
			{
				for(int i = 0; i < LoggedInCoworker.Districts.Count; i++)
				{
					districtCodes.Add(LoggedInCoworker.Districts[i].Code);
				}
			}
			return districtCodes;
		}

		// Nerladdning av data
		public static string GetTableQuery(ServerQueryType type, List<Condition> specificConditions = null, QueryAttributes queryAttributes = null, string clientId = null, bool withSearch = false)
		{
			string query = null;

			string fieldString = getFieldString(type, withSearch);
			string tableString = GetTableName(type, withSearch);
			List<Condition> standardConditions = GetConditions(type, clientId, withSearch);

			if(type == ServerQueryType.ClientArticlesBought ||
				type == ServerQueryType.ClientArticlesDemoed)
			{
				if(queryAttributes != null)
				{
					queryAttributes.Distinct = true;
				}
				else
				{
					queryAttributes = new QueryAttributes() { Distinct = true };
				}
			}

			//if (specificConditions != null)
			//	standardConditions.AddRange(specificConditions);

			string queryAttributesString = getQueryAttributesString(queryAttributes);
			string conditionString = getConditionString(standardConditions, specificConditions);

			if(tableString != null)
			{
				query = "<![CDATA[<query" + queryAttributesString + ">" +
					"<tables>" +
					"<table>" + tableString + "</table>" +
					"</tables>" +
					fieldString +
					conditionString +
					"</query>]]>";
			}
			return query;
		}

		// Nerladdning av data
		public static string GetLoginQuery(string _salesId, string _password)
		{
			List<Condition> conditions = new List<Condition>()
			{
				new Condition()
				{
					ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
				},
				//HACK It skips checking password, so all you need is a correct salesNr.
//				new Condition()
//				{
//					isOR = false,
//
//					LeftValue = new List<string>(){ "password" },
//					LeftType = Condition.ValueTypeEnum.Field,
//
//					Operator = Condition.OperatorEnum.EqualTo,
//
//					RightValue = _password,
//					RightType = Condition.ValueTypeEnum.String
//				},
				new Condition()
				{
					isOR = false,

					LeftValue = new List<string>(){ "saleno" },
					LeftType =  Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = _salesId,
					RightType = Condition.ValueTypeEnum.String
				},
				new Condition()
				{
					isOR = false,

					LeftValue = new List<string>(){ "active" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,
					RightValue = "1",
					RightType = Condition.ValueTypeEnum.Numeric
				},
				new Condition()
				{
					ConditionType = Condition.ConditionTypeEnum.RightParenthesis
				},

				new Condition()
				{
					isOR = true,
					ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
				},

				new Condition()
				{
					isOR = false,	

					LeftValue = new List<string>(){ "maincoworker.saleno" },
					LeftType =  Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = _salesId,
					RightType = Condition.ValueTypeEnum.String
				},

				new Condition()
				{
					ConditionType = Condition.ConditionTypeEnum.RightParenthesis
				}
			};
			string conditionString = getConditionString(conditions);
			string query = "<![CDATA[<query>" +
				"<tables>" +
				"<table>coworker</table>" +
				"</tables>" +
				conditionString +
				"</query>]]>";

			return query;
		}

		// Nerladdning av data
		public static string GetDistrictQuery(List<string> _salesIds, bool fullAccess)
		{
			List<Condition> conditions = new List<Condition>();
			if(!fullAccess)
			{
				for(int i = 0; i < _salesIds.Count; i++)
				{
					conditions.Add(new Condition() {
						isOR = true,

						LeftValue = new List<string>() { "coworker.idcoworker" },
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = _salesIds[i],
						RightType = Condition.ValueTypeEnum.Numeric

					});
				}
			}

			string conditionString = getConditionString(conditions);
			string query = "<![CDATA[<query>" +
				"<tables>" +
				"<table>district</table>" +
				"</tables>" +
				conditionString +
				"</query>]]>";


			return query;
		}

		// Nerladdning av data
		public static string GetCardClientQuery(string clientId)
		{
			List<Condition> conditions = new List<Condition>();

			conditions.Add(new Condition()
				{
					LeftValue = new List<string>(){ "idcompany" },
					LeftType = Condition.ValueTypeEnum.Field,

					Operator = Condition.OperatorEnum.EqualTo,

					RightValue = clientId,
					RightType = Condition.ValueTypeEnum.Numeric

				});

			string fieldString = getFieldString(ServerQueryType.CardClientData);

			string conditionString = getConditionString(conditions);
			string query = "<![CDATA[<query top=\"1\">" +
				"<tables>" +
				"<table>company</table>" +
				"</tables>" +
				fieldString +
				conditionString +
				"</query>]]>";
			return query;
		}
		*/
	}
}

