using System;
using System.Collections.Generic;

namespace LimeServer
{
	public partial class LimeHelper //Conditions
	{
		/*
		static List<Condition> GetConditions(ServerQueryType type, string clientId = null, bool withSearch = false)
		{
			List<string> districtCodes = GetDistrictCodes();

			List<Condition> conditions = new List<Condition>();
			switch(type)
			{
				case ServerQueryType.BusinessRows:
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"selected"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "233001",
						RightType = Condition.ValueTypeEnum.Numeric,
					});
				break;
				case ServerQueryType.ArticleBoughtBy:
					//De finns i LimeService i en egen funktion.
				break;
				case ServerQueryType.DataTranslation:
					conditions.Add(new Condition()
						{
							ConditionType =  Condition.ConditionTypeEnum.Statement,
							isOR = false,

							LeftType = Condition.ValueTypeEnum.String,
							LeftValue = new List<string>(){"ipadcrm"},

							Operator = Condition.OperatorEnum.EqualTo,

							RightType = Condition.ValueTypeEnum.Field,
							RightValue = "owner",
						});
				break;
				case ServerQueryType.DataOrderType:
					conditions.Add(new Condition()
						{
							LeftValue = new List<string>(){"active"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "1",
							RightType = Condition.ValueTypeEnum.Numeric
						});
				break;
				case ServerQueryType.DataPaymentTerms:
					conditions.Add(new Condition()
						{
							LeftValue = new List<string>(){"availableinapp"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "1",
							RightType = Condition.ValueTypeEnum.Numeric
						});
				break;
				case ServerQueryType.MapsRoute:
					if(LoggedInCoworker != null && !LoggedInCoworker.FullAccess)
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>() { LoggedInCoworker.Id },
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "coworker.idcoworker",
							RightType = Condition.ValueTypeEnum.Field,
						});
					}
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"company.gpslong"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNotNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"company.gpslat"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNotNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Numeric,
						});

					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"267301"},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "subject",
							RightType = Condition.ValueTypeEnum.Field,
						});

					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"0"},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "done",
							RightType = Condition.ValueTypeEnum.Field,
						});


					string today = DateTime.Today.ToString(ServerDateFormat);
					string dayBegin = today + " 00:00";
					string dayEnd = today + " 23:59";
					conditions.Add(new Condition() { isOR = false, ConditionType = Condition.ConditionTypeEnum.LeftParenthesis });
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){dayBegin},
							LeftType = Condition.ValueTypeEnum.Date,

							Operator = Condition.OperatorEnum.LessOrEqual,

							RightValue = "starttime",
							RightType = Condition.ValueTypeEnum.Field,
						});
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){dayEnd},
							LeftType = Condition.ValueTypeEnum.Date,

							Operator = Condition.OperatorEnum.GreaterOrEqual,

							RightValue = "starttime",
							RightType = Condition.ValueTypeEnum.Field,
						});
					conditions.Add(new Condition() { ConditionType = Condition.ConditionTypeEnum.RightParenthesis });

				break;
				case ServerQueryType.MapsNearbyClients:
					conditions.Add(new Condition()
						{
							LeftValue = districtCodes,
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "district.code",
							RightType = Condition.ValueTypeEnum.Field,
						});

					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"gpslong"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNotNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"gpslat"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNotNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Numeric,
						});

				break;
				case ServerQueryType.ActivityPlanning:
					if(LoggedInCoworker != null && !LoggedInCoworker.FullAccess)
					{
						conditions.Add(new Condition()
							{
								LeftValue = new List<string>() { LoggedInCoworker.Id },
								LeftType = Condition.ValueTypeEnum.Numeric,

								Operator = Condition.OperatorEnum.EqualTo,

								RightValue = "coworker.idcoworker",
								RightType = Condition.ValueTypeEnum.Field,
							});
					}
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"canceled"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "0",
							RightType = Condition.ValueTypeEnum.Numeric,
						});
				break;
				case ServerQueryType.ClientToDo:
					if(LoggedInCoworker != null && !LoggedInCoworker.FullAccess)
					{
						conditions.Add(new Condition()
							{
								isOR = false,

								LeftValue = new List<string>() { LoggedInCoworker.Id },
								LeftType = Condition.ValueTypeEnum.Numeric,

								Operator = Condition.OperatorEnum.EqualTo,

								RightValue = "coworker.idcoworker",
								RightType = Condition.ValueTypeEnum.Field,
							});
					}
					if(clientId != null)
					{
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"company.idcompany"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = clientId,
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					}
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"canceled"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "0",
						RightType = Condition.ValueTypeEnum.Numeric,
					});
				break;
				case ServerQueryType.ClientHistory:
					if(clientId != null)
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>(){"company.idcompany"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = clientId,
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					}
				break;
				case ServerQueryType.ClientOrderOffers:
					if(clientId != null)
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>(){"company.idcompany"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = clientId,
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					}
				break;
				case ServerQueryType.ClientArticlesBought:
					conditions.Add(new Condition() {
						ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
					});
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"business.company.idcompany"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = clientId,
						RightType = Condition.ValueTypeEnum.Numeric,
					});
					conditions.Add(new Condition() {
						isOR = false,
						ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
					});
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"14901", "15001"},
						LeftType = Condition.ValueTypeEnum.Numeric,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "business.businesstatus",
						RightType = Condition.ValueTypeEnum.Field,
					});
					conditions.Add(new Condition() {
						isOR = true,
						ConditionType = Condition.ConditionTypeEnum.RightParenthesis
					});
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"selected"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "233001",
						RightType = Condition.ValueTypeEnum.Numeric,
					});
					conditions.Add(new Condition() {
						isOR = true,
						ConditionType = Condition.ConditionTypeEnum.RightParenthesis
					});
				break;
				case ServerQueryType.ClientArticlesDemoed:
					//( history.company.idcompany == clientId )
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"history.company.idcompany"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = clientId,
						RightType = Condition.ValueTypeEnum.Numeric,
					});
				break;
				case ServerQueryType.ClientContacts:
					conditions.Add(new Condition()
						{
							LeftValue = districtCodes,
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "company.district.code",
							RightType = Condition.ValueTypeEnum.Field,
						});
					if(clientId != null)
					{
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"company.idcompany"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = clientId,
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					}
				break;
				case ServerQueryType.ClientDocuments:
					if(clientId != null)
					{
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"company.idcompany"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = clientId,
							RightType = Condition.ValueTypeEnum.Numeric,
						});
					}
				break;
				case ServerQueryType.MyPagesStatistics:
					conditions.Add(new Condition()
						{
							LeftValue = new List<string>(){"active"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "1",
							RightType = Condition.ValueTypeEnum.Numeric,
						});
				break;
				case ServerQueryType.MyPagesGoalValues:
					DateTime now = DateTime.Now;

//					DateTime startMonth = now.AddDays(-((now.Day - 1)));
//					DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);

					conditions.Add(new Condition()
						{
							LeftValue = new List<string>(){"startdate"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.LessOrEqual,

							RightValue = DateTime.Now.ToString(LimeHelper.ServerDateFormat),
							RightType = Condition.ValueTypeEnum.Date,
						});
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"enddate"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.GreaterOrEqual,

							RightValue = DateTime.Now.ToString(LimeHelper.ServerDateFormat),
							RightType = Condition.ValueTypeEnum.Date,
						});
				break;
				case ServerQueryType.ClientSearch:
					conditions.Add(new Condition()
						{
							LeftValue = districtCodes,
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "district.code",
							RightType = Condition.ValueTypeEnum.Field,
						});
				break;
				case ServerQueryType.ActivityHistory:
					conditions.Add(new Condition()
						{
							LeftValue = districtCodes,
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "company.district.code",
							RightType = Condition.ValueTypeEnum.Field,
						});
				break;
				case ServerQueryType.ActivityAlarms:
					conditions.Add(new Condition() {
						LeftValue = districtCodes,
						LeftType = Condition.ValueTypeEnum.String,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "district.code",
						RightType = Condition.ValueTypeEnum.Field,
					});
					conditions.Add(new Condition() { isOR = false, ConditionType = Condition.ConditionTypeEnum.LeftParenthesis });
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"latesthistory"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Date,
						});
					conditions.Add(new Condition()
						{
							isOR = true,

							LeftValue = new List<string>(){DateTime.Now.AddMonths(-3).ToString(ServerDateFormat)},
							LeftType = Condition.ValueTypeEnum.Date,

							Operator = Condition.OperatorEnum.GreaterOrEqual,

							RightValue = "latesthistory",
							RightType = Condition.ValueTypeEnum.Field,
						});

					conditions.Add(new Condition() { ConditionType = Condition.ConditionTypeEnum.RightParenthesis });
				break;
				case ServerQueryType.OrderOfferHistory:
					conditions.Add(new Condition()
						{
							LeftValue = districtCodes,
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "company.district.code",
							RightType = Condition.ValueTypeEnum.Field,
						});
					conditions.Add(new Condition()
						{
							isOR = false,

							LeftValue = new List<string>(){"15301"},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.NotEqualTo,

							RightValue = "businesstatus",
							RightType = Condition.ValueTypeEnum.Field,
						});
				break;
				case ServerQueryType.OrderOfferUrgents:
					conditions.Add(new Condition() {
						ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
					});
					conditions.Add(new Condition() {
						LeftValue = districtCodes,
						LeftType = Condition.ValueTypeEnum.String,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "company.district.code",
						RightType = Condition.ValueTypeEnum.Field,
					});
					conditions.Add(new Condition() {
						ConditionType = Condition.ConditionTypeEnum.RightParenthesis
					});
					conditions.Add(new Condition() {
						isOR = false,
						LeftValue = new List<string>(){"15301"},
						LeftType = Condition.ValueTypeEnum.Numeric,

						Operator = Condition.OperatorEnum.EqualTo,

						RightValue = "businesstatus",
						RightType = Condition.ValueTypeEnum.Field,
					});
				break;
				case ServerQueryType.ArticleCampaigns:
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"startdate"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.LessOrEqual,

						RightValue = DateTime.Now.ToString(ServerDateFormat),
						RightType = Condition.ValueTypeEnum.Date,
					});
					conditions.Add(new Condition() {
						isOR = false,

						LeftValue = new List<string>(){"enddate"},
						LeftType = Condition.ValueTypeEnum.Field,

						Operator = Condition.OperatorEnum.GreaterOrEqual,

						RightValue = DateTime.Now.ToString(ServerDateFormat),
						RightType = Condition.ValueTypeEnum.Date,
					});
				break;
				case ServerQueryType.ArticleSearch:

				break;
				case ServerQueryType.AddArticleDemoed:
					if(withSearch)
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>(){"1"},
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "active",
							RightType = Condition.ValueTypeEnum.Field
						});
					}
					else
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>(){LoggedInCoworker.Id},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "history.coworker.idcoworker",
							RightType = Condition.ValueTypeEnum.Field
						});
					}
				break;
				case ServerQueryType.AddArticleBusiness:
					if(withSearch)
					{
						conditions.Add(new Condition() {
							LeftValue = new List<string>(){"1"},
							LeftType = Condition.ValueTypeEnum.String,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "active",
							RightType = Condition.ValueTypeEnum.Field
						});
					}
					else
					{
						conditions.Add(new Condition() {
							ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
						});
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){LoggedInCoworker.Id},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "business.coworker.idcoworker",
							RightType = Condition.ValueTypeEnum.Field
						});
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"article"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.IsNotNull,

							RightValue = "",
							RightType = Condition.ValueTypeEnum.Numeric
						});
						conditions.Add(new Condition() {
							isOR = false,
							ConditionType = Condition.ConditionTypeEnum.LeftParenthesis
						});
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"14901", "15001", "14401"},
							LeftType = Condition.ValueTypeEnum.Numeric,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "business.businesstatus",
							RightType = Condition.ValueTypeEnum.Field,
						});
						conditions.Add(new Condition() {
							isOR = true,
							ConditionType = Condition.ConditionTypeEnum.RightParenthesis
						});
						conditions.Add(new Condition() {
							isOR = false,

							LeftValue = new List<string>(){"selected"},
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = "233001",
							RightType = Condition.ValueTypeEnum.Numeric,
						});
						conditions.Add(new Condition() {
							isOR = true,
							ConditionType = Condition.ConditionTypeEnum.RightParenthesis
						});


					}
				break;

			}
			return conditions;
		}

		static string getValueString(Condition.ValueTypeEnum type)
		{
			string valueType = "";
			switch(type)
			{
				case Condition.ValueTypeEnum.String:
					valueType = "string";
				break;

				case Condition.ValueTypeEnum.Numeric:
					valueType = "numeric";
				break;

				case Condition.ValueTypeEnum.Field:
					valueType = "field";
				break;

				case Condition.ValueTypeEnum.Date:
					valueType = "date";
				break;

				case Condition.ValueTypeEnum.Query:
					valueType = "query";
				break;
			}
			return valueType;
		}

		static string getOperatorString(Condition.OperatorEnum operatorEnum)
		{
			string operatorString = null;
			switch(operatorEnum)
			{
				case Condition.OperatorEnum.Contains:
					operatorString = "%LIKE%";
				break;

				case Condition.OperatorEnum.EndsWith:
					operatorString = "%LIKE";
				break;

				case Condition.OperatorEnum.StartsWith:
					operatorString = "LIKE%";
				break;

				case Condition.OperatorEnum.EqualTo:
					operatorString = "=";
				break;

				case Condition.OperatorEnum.GreaterOrEqual:
					operatorString = "GreaterThanOrEqualTo";
				break;

				case Condition.OperatorEnum.LessOrEqual:
					operatorString = "LessThanOrEqualTo";
				break;

				case Condition.OperatorEnum.In:
					operatorString = "IN";
				break;

				case Condition.OperatorEnum.GreaterThan:
					operatorString = "GreaterThan";
				break;

				case Condition.OperatorEnum.LessThan:
					operatorString = "LessThan";
				break;

				case Condition.OperatorEnum.NotEqualTo:
					operatorString = "NotEqualTo";
				break;

				case Condition.OperatorEnum.Any:
					operatorString = "ANY";
				break;

				case Condition.OperatorEnum.All:
					operatorString = "ALL";
				break;

				case Condition.OperatorEnum.NotLike:
					operatorString = "NOT LIKE";
				break;

				case Condition.OperatorEnum.NotStartsWith:
					operatorString = "NOT LIKE%";
				break;

				case Condition.OperatorEnum.NotEndsWith:
					operatorString = "NOT %LIKE";
				break;

				case Condition.OperatorEnum.NotIn:
					operatorString = "NOT IN";
				break;

				case Condition.OperatorEnum.IsNull:
					operatorString = "IS";
				break;

				case Condition.OperatorEnum.IsNotNull:
					operatorString = "IS NOT";
				break;
			}
			return operatorString;
		}

		static string appendConditionString(ref string conditionString, Condition condition)
		{
			string operatorString = null;
			string ORstring = null;

			List<string> LeftValue = null;
			Condition.ValueTypeEnum LeftType;

			string RightValue = null;
			Condition.ValueTypeEnum RightType;

			ORstring = "0";
			if(condition.isOR)
			{
				ORstring = "1";
			}

			operatorString = getOperatorString(condition.Operator);

			LeftValue = condition.LeftValue;
			LeftType = condition.LeftType;

			RightValue = condition.RightValue;
			RightType = condition.RightType;


			Condition.ConditionTypeEnum conditionType = condition.ConditionType;

			if(conditionType == Condition.ConditionTypeEnum.Statement)
			{
				if(LeftValue != null && LeftValue.Count > 0 && RightValue != null)
				{
					string leftTypeString = getValueString(LeftType);
					string rightTypeString = getValueString(RightType);

					string ORstringOnStatements = "1";
					if(LeftValue.Count > 1)
					{
						conditionString += "<condition or=\"" + ORstring + "\">" +
							"<exp type=\"(\"/>" +
							"</condition>";
					}
					else
						ORstringOnStatements = ORstring;

					for(int j = 0; j < LeftValue.Count; j++)
					{
						conditionString += "<condition operator=\"" + operatorString + "\" or=\"" + ORstringOnStatements + "\">" +
							"<exp type=\"" + leftTypeString + "\">" + LeftValue[j] + "</exp>" +
							"<exp type=\"" + rightTypeString + "\">" + RightValue + "</exp>" +
							"</condition>";
					}
					if(LeftValue.Count > 1)
					{
						conditionString += "<condition>" +
							"<exp type=\")\"/>" +
							"</condition>";
					}

//					conditionString += "<condition operator=\"" + operatorString + "\" or=\"" + ORstring + "\">" +
//												"<exp type=\"field\">" + fieldName + "</exp>" +
//												"<exp type=\"string\">" + searchString + "</exp>" +
//											"</condition>";
				}
			}
			else if(conditionType == Condition.ConditionTypeEnum.LeftParenthesis)
			{
				conditionString += "<condition or=\"" + ORstring + "\">" +
					"<exp type=\"(\"/>" +
					"</condition>";
			}
			else if(conditionType == Condition.ConditionTypeEnum.RightParenthesis)
			{
				conditionString += "<condition>" +
					"<exp type=\")\"/>" +
					"</condition>";
			}
			return conditionString;
		}

		static string getConditionString(List<Condition> conditions, List<Condition> extraConditions = null)
		{
			if(extraConditions == null)
				extraConditions = new List<Condition>();
			if(conditions == null)
				conditions = new List<Condition>();
//			Exempel på conditionString
//				<conditions>
//					<condition operator="=">
//						<exp type="field">city</exp>
//						<exp type="string">Lund</exp>
//					</condition>
//					<condition operator="in" or="0">
//						<exp type="field">category</exp>
//						<exp type="string">Customer;Prospect</exp>
//					</condition>
//				</conditions>


			string conditionString = "";
			if(conditions.Count > 0 || extraConditions.Count > 0)
			{
				conditionString += "<conditions>";


				for(int i = 0; i < conditions.Count; i++)
				{
					appendConditionString(ref conditionString, conditions[i]);
				}


				for(int i = 0; i < extraConditions.Count; i++)
				{
					appendConditionString(ref conditionString, extraConditions[i]);
				}

				conditionString += "</conditions>";
			}

			return conditionString;
		}

		static string getQueryAttributesString(QueryAttributes queryAttributes)
		{
			string queryAttributesString = "";
			if(queryAttributes != null)
			{
				if(queryAttributes.Distinct)
				{
					queryAttributesString += " distinct=\"1\"";
				}
				if(queryAttributes.First > 0)
				{
					queryAttributesString += " first=\"" + queryAttributes.First + "\"";
				}
				if(queryAttributes.Top >= 0)
				{
					queryAttributesString += " top=\"" + queryAttributes.Top + "\"";
				}
			}
			return queryAttributesString;
		}

		public static List<Condition> GetSearchConditions(string _searchString, ServerQueryType queryType, bool withSearch = false)
		{
			_searchString = SecurityElement.Escape(_searchString);

			List<Condition> searchConditions =  new List<Condition>();
			if(_searchString.Length > 0)
			{
				Condition.OperatorEnum searhType = Condition.OperatorEnum.Contains;

				bool isSplitted = false;
				if(_searchString[0] == '*' && _searchString[_searchString.Length - 1] == '*')
					searhType = Condition.OperatorEnum.Contains;
				else if(_searchString[0] == '*')
					searhType = Condition.OperatorEnum.EndsWith;
				else if(_searchString[_searchString.Length - 1] == '*')
					searhType = Condition.OperatorEnum.StartsWith;
				else
				{
					isSplitted = true;
					searhType = Condition.OperatorEnum.Contains;
					_searchString = _searchString.Replace("*", "");
					string[] splitted = _searchString.Split(' ');

					for(int i = 0; i < splitted.Length; i++)
					{
						List<string> fields = LimeHelper.GetSearchFields(queryType, withSearch);
						List<string> numericFields = GetNumericFieldsAndRemoveThemFromList(ref fields, splitted[i], queryType);

						if(fields.Count > 0 && numericFields.Count > 0)
							searchConditions.Add(new Condition() { isOR = false, ConditionType = Condition.ConditionTypeEnum.LeftParenthesis });
						if(fields.Count > 0)
						{
							searchConditions.Add(new Condition() {
								isOR = false,

								LeftValue = fields,
								LeftType = Condition.ValueTypeEnum.Field,

								Operator = searhType,

								RightValue = splitted[i],
								RightType = Condition.ValueTypeEnum.String
							});
						}
						if(numericFields.Count > 0)
						{
							searchConditions.Add(new Condition()
								{
									isOR = fields.Count > 0 && numericFields.Count > 0,

									LeftValue = numericFields,
									LeftType = Condition.ValueTypeEnum.Field,

									Operator = Condition.OperatorEnum.EqualTo,

									RightValue = splitted[i],
									RightType = Condition.ValueTypeEnum.Numeric
								});
						}
						if(fields.Count > 0 && numericFields.Count > 0)
							searchConditions.Add(new Condition() { ConditionType = Condition.ConditionTypeEnum.RightParenthesis });
					}
				}

				if (!isSplitted)
				{
					string value = _searchString.Replace("*", "");


					List<string> fields = LimeHelper.GetSearchFields(queryType, withSearch);
					List<string> numericFields = GetNumericFieldsAndRemoveThemFromList(ref fields, value, queryType);


					if(fields.Count > 0 && numericFields.Count > 0)
						searchConditions.Add(new Condition() { isOR = false, ConditionType = Condition.ConditionTypeEnum.LeftParenthesis });

					if(fields.Count > 0)
					{
						searchConditions.Add(new Condition() {
							LeftValue = fields,
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = searhType,

							RightValue = value,
							RightType = Condition.ValueTypeEnum.String

						});
					}

					if(numericFields.Count > 0)
					{
						searchConditions.Add(new Condition() {
							isOR = fields.Count > 0 && numericFields.Count > 0,

							LeftValue = numericFields,
							LeftType = Condition.ValueTypeEnum.Field,

							Operator = Condition.OperatorEnum.EqualTo,

							RightValue = value,
							RightType = Condition.ValueTypeEnum.Numeric

						});
					}

					if(fields.Count > 0 && numericFields.Count > 0)
						searchConditions.Add(new Condition() { ConditionType = Condition.ConditionTypeEnum.RightParenthesis });
				}

			}
			return searchConditions;
		}
		*/
	}

	#region ConditionClasses

	public class Condition
	{
		public List<string> LeftValue { get; set; }
		public ValueTypeEnum LeftType { get; set; }

		public string RightValue { get; set; }
		public ValueTypeEnum RightType { get; set; }

		public OperatorEnum Operator { get; set; }
		public bool isOR { get; set; } // Om true så är det "OR" mellan föregående och aktuell. annars är det en "AND"
		public ConditionTypeEnum ConditionType { get; set; }

		public enum OperatorEnum
		{
			None,

			EqualTo,
			GreaterThan,
			LessThan,
			GreaterOrEqual,
			LessOrEqual,
			NotEqualTo,
			Contains,
			In,
			Any,
			All,
			StartsWith,
			EndsWith,
			NotLike,
			NotStartsWith,
			NotEndsWith,
			NotIn,
			IsNull,
			IsNotNull,
		}


		public enum ValueTypeEnum
		{
			String,
			Numeric,
			Field,
			Date,
			Query
		}

		public enum ConditionTypeEnum
		{
			Statement,
			LeftParenthesis,
			RightParenthesis
		}


		public Condition()
		{
			this.ConditionType = ConditionTypeEnum.Statement;

			this.isOR = false;

			this.LeftValue = null;
			this.LeftType = ValueTypeEnum.Field;

			this.Operator = Condition.OperatorEnum.None;

			this.RightValue = null;
			this.RightType = ValueTypeEnum.String;
		}
	}

	public class QueryAttributes
	{
		public int Top { get; set; }
		public int First { get; set; }
		public bool Distinct { get; set; }

		public QueryAttributes()
		{
			this.Top = 0;
			this.First = 0;
			this.Distinct = false;
		}
	}
	#endregion
}

