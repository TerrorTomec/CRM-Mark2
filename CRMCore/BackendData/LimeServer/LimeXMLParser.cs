using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using MonoTouch.UIKit;
using System.Linq;
using System.Globalization;


namespace LimeServer
{
	public static class LimeXMLParser
	{

		#region Help functions
		static int countOccurences(string sequenceToCount, string completeString)
		{
			return (completeString.Length - completeString.Replace(sequenceToCount,"").Length) / sequenceToCount.Length;
		}

		static string PrepareXMLString(string xml)
		{
			return xml.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&").Replace("<?xml version=\"1.0\" encoding=\"UTF-16\" ?>", null).Replace("<?xml version=\"1.0\" encoding=\"UTF-16\"?>", null).Replace("<?xml version=\"1.0\"?>", null);
		}

		public static DateTime? ParseStringToDateTime(string dateTimeString)
		{
			DateTime? dateTime = null;
			int year, month, day, hour, minute, second;
			year = month = day = hour = minute = second = -1;
			if(dateTimeString != null)
			{
				if(dateTimeString.Length >= 10)
				{
					year = int.Parse(dateTimeString.Substring(0, 4));
					month = int.Parse(dateTimeString.Substring(5, 2));
					day = int.Parse(dateTimeString.Substring(8, 2));
				}
				if(dateTimeString.Length >= 16)
				{
					hour = int.Parse(dateTimeString.Substring(11, 2));
					minute = int.Parse(dateTimeString.Substring(14, 2));
					second = 0;
				}
				if(minute != -1)
					dateTime = new DateTime(year, month, day, hour, minute, second);
				else if(day != -1)
					dateTime = new DateTime(year, month, day);
			}
			return dateTime;
		}

		public static int GetNrOfShownFields(List<string> fields, List<string> hideFields)
		{
			int nrOfShownFields = fields.Count;

			for(int i = 0; i < hideFields.Count; i++)
			{
				string foundFieldName = fields.Find(delegate(string field) {
					return field == hideFields[i];
				});
				if(foundFieldName != null)
					nrOfShownFields--;
			}
			return nrOfShownFields;
		}

		public static string BigNumberDelimiter(string number, int decimalCount)
		{
			if (number == null)
				return null;
			/*string intPart = number;
			string rest = "";
			string[] splittedString = number.Split('.');
			if(splittedString.Length > 1)
			{
				intPart = splittedString[0];
				rest = "";
				for(int i = 1; i < splittedString.Length; i++)
				{
					rest += "." + splittedString[i];
				}
			}

			int nrOfSpaces = (int)Math.Floor((float)((intPart.Length - 1) / 3));
			for(int index = nrOfSpaces*3; index > 0; index-=3)
			{
				intPart = intPart.Insert(intPart.Length - index, delimiter);
			}

			return intPart + rest;*/
			number = number.Replace(" ", null).Replace(LimeHelper.NonBreakSpace, null).Replace(".", ",");
			if(number != "")
			{
				try
				{
					float numberF = float.Parse(number, System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE"));
					number = numberF.ToString("N" + decimalCount, System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE"));

					/*string nrEnd = number.Substring(number.Length-2);
					if(nrEnd == "00")
					{
						number = number.Substring(0, number.Length - 3);
					}*/
					return number;
				}
				catch
				{
					return number;
				}
			}
			else
				return number;

		}

		static string CutDateTimeToDateString(string dateTimeString)
		{
			if(dateTimeString != null && dateTimeString.Length > 10)
				dateTimeString = dateTimeString.Substring(0, 10);
			return dateTimeString;
		}

		static double parseToDouble(string doubleString)
		{
			double retVal = 0;

			if(doubleString != null && doubleString.Trim() != "")
			{
				retVal = double.Parse(doubleString, CultureInfo.InvariantCulture);
			}
			else
			{
				throw new Exception("No GPS-coordinate set");
			}

			return retVal;
		}

		#endregion

		/*
		public static List<NavigationCore.CoworkerGoalValueData> ParseToCoworkerGoalValues(string XML)
		{
			XML = PrepareXMLString(XML);

			ServerQueryType type = ServerQueryType.MyPagesGoalValues;

			List<NavigationCore.CoworkerGoalValueData> coworkerGoalValues = new List<NavigationCore.CoworkerGoalValueData>();

			//Hämtar tabellnamn.
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							var prevCoworker = coworkerGoalValues.Find((obj) => {
								return obj.CoworkerID == reader.GetAttribute("coworker.idcoworker");
							});

							var goalValue = new NavigationCore.GoalValue()
							{
								Budget = LimeHelper.ConvertToNumber(reader.GetAttribute("budget")),
								EndDate = (DateTime)LimeXMLParser.ParseStringToDateTime(reader.GetAttribute("enddate")),
								StartDate = (DateTime)LimeXMLParser.ParseStringToDateTime(reader.GetAttribute("startdate")),
							};

							if(prevCoworker != null)
							{
								prevCoworker.GoalValues.Add(goalValue);
							}
							else
							{
								var coworkerGoalValue = new NavigationCore.CoworkerGoalValueData()
								{
									CoworkerID = reader.GetAttribute("coworker.idcoworker"),
									CoworkerName = reader.GetAttribute("coworker.name"),
									CurrentBudgetForPeriod =  LimeHelper.ConvertToNumber(reader.GetAttribute("coworker.periodsales")),
									CurrentBudgetForYear =  LimeHelper.ConvertToNumber(reader.GetAttribute("coworker.yearlysales")),
									GoalValueForYear =  LimeHelper.ConvertToNumber(reader.GetAttribute("coworker.yearlybudget")),
									GoalValues = new List<NavigationCore.GoalValue>()
									{
										goalValue
									},
								};
								coworkerGoalValues.Add(coworkerGoalValue);
							}

						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}

			}

			return coworkerGoalValues;
		}

		public static List<NavigationCore.CoworkerStatisticsData> ParseToCoworkerStatistics(string XML)
		{
			XML = PrepareXMLString(XML);

			ServerQueryType type = ServerQueryType.MyPagesStatistics;

			List<NavigationCore.CoworkerStatisticsData> statistics = new List<NavigationCore.CoworkerStatisticsData>();

			//Hämtar tabellnamn.
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							var statistic = new NavigationCore.CoworkerStatisticsData()
							{
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),

								BookingCount = (int)LimeHelper.ConvertToNumber(reader.GetAttribute("todaybookings")),
								DemoCount = (int)LimeHelper.ConvertToNumber(reader.GetAttribute("todaydemos")),
								OrderCount = (int)LimeHelper.ConvertToNumber(reader.GetAttribute("todayorders")),
								SalesSum = (int)LimeHelper.ConvertToNumber(reader.GetAttribute("todaysales")),
								VisitCount = (int)LimeHelper.ConvertToNumber(reader.GetAttribute("todayvisits")),
							};
							statistics.Add(statistic);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}

			}

			return statistics;
		}

		public static List<NavigationCore.DistrictData> ParseToDistricts(string XML)
		{
			XML = PrepareXMLString(XML);

			List<NavigationCore.DistrictData> districts = new List<NavigationCore.DistrictData>();

			//Hämtar tabellnamn.
			string tableName = "district";

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							var district = new NavigationCore.DistrictData()
							{
								Code = reader.GetAttribute("code"),
								Name = reader.GetAttribute("name"),
								ID = reader.GetAttribute("id" + tableName)
							};
							districts.Add(district);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}

			}

			return districts;
		}

		public static List<NavigationCore.CoworkerData> ParseLogin(string XML)
		{
			XML = PrepareXMLString(XML);
			List<NavigationCore.CoworkerData> coworkers = new List<NavigationCore.CoworkerData>();

			//Hämtar tabellnamn.
			string tableName = "coworker";

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				reader.ReadToFollowing("data");
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
					{
						coworkers.Add(new NavigationCore.CoworkerData()
						{
							Id = reader.GetAttribute("idcoworker"),
							Name = reader.GetAttribute("name"),
							SaleNr = reader.GetAttribute("saleno"),
							FullAccess = reader.GetAttribute("fullaccess") == "1",
						});
					}
				}
			}
			return coworkers;
		}

		static NavigationCore.CardData ParseToCardData(string XML, ServerQueryType type, Func<XmlReader, ServerQueryType, NavigationCore.CardData> fillerFunction)
		{
			XML = PrepareXMLString(XML);

			NavigationCore.CardData cardData = null;

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							cardData = fillerFunction(reader, type);
							break;
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return cardData;
		}

		public static NavigationCore.CardArticleData ParseToCardArticleData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardArticleData;

			NavigationCore.CardArticleData cardArticleData = (NavigationCore.CardArticleData)ParseToCardData(XML, type, delegate(XmlReader reader, ServerQueryType queryType) {
				string tableName = LimeHelper.GetTableName(queryType);
				return new NavigationCore.CardArticleData() {
					ID = reader.GetAttribute("id" + tableName),
					Active = reader.GetAttribute("active"),
					ArticleName = reader.GetAttribute("name"),
					ArticleNr = reader.GetAttribute("articleno"),
					PriceGroup1 = BigNumberDelimiter(reader.GetAttribute("price1"), 2),
					PriceGroup2 = BigNumberDelimiter(reader.GetAttribute("price2"), 2),
					PriceGroup3 = BigNumberDelimiter(reader.GetAttribute("price3"), 2),
					PriceGroup4 = BigNumberDelimiter(reader.GetAttribute("price4"), 2),
					PriceGroup5 = BigNumberDelimiter(reader.GetAttribute("price5"), 2),
					PriceGroup6 = BigNumberDelimiter(reader.GetAttribute("price6"), 2),
					PriceGroup7 = BigNumberDelimiter(reader.GetAttribute("price7"), 2),
					PriceGroup8 = BigNumberDelimiter(reader.GetAttribute("price8"), 2),
					ProductCategory = reader.GetAttribute("division.name"),
					ProductionGroup = reader.GetAttribute("prodgroup"),
					Vat = reader.GetAttribute("vat")
				};
			});

			return cardArticleData;
		}

		public static List<NavigationCore.ArticleDemo> ParseToArticlesDemoed(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType type = ServerQueryType.DemoedArticles;

			List<NavigationCore.ArticleDemo> articlesDemoed = new List<NavigationCore.ArticleDemo>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							NavigationCore.ArticleDemo articleDemo = new NavigationCore.ArticleDemo()
							{
								ID = reader.GetAttribute("id" + tableName),
								ArticleID = reader.GetAttribute("article.idarticle"),
								ArticleName = reader.GetAttribute("article.name"),
								ArticleNr = reader.GetAttribute("article.articleno"),
								HistoryID = reader.GetAttribute("history.idhistory"),
								Ordered = reader.GetAttribute("ordered") == "1" ? true : false,
							};

							articlesDemoed.Add(articleDemo);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return articlesDemoed;

		}

		public static List<NavigationCore.ArticleResource> ParseToArticleResources(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType type = ServerQueryType.ArticleResource;

			List<NavigationCore.ArticleResource> rowData = new List<NavigationCore.ArticleResource>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							NavigationCore.ArticleResource row = new NavigationCore.ArticleResource()
							{
								ID = reader.GetAttribute("idresource"),
								Link = reader.GetAttribute("link"),
								Name = reader.GetAttribute("name")
							};

							rowData.Add(row);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return rowData;

		}

		public static List<NavigationCore.CampaignRow> ParseToCampaignRows(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType type = ServerQueryType.CampaignRows;

			List<NavigationCore.CampaignRow> rowData = new List<NavigationCore.CampaignRow>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							NavigationCore.CampaignRow row = new NavigationCore.CampaignRow()
							{
								CampaignID = reader.GetAttribute("articlecampagin.idarticlecampagin"),
								ArticleID = reader.GetAttribute("article.idarticle"),
								ArticleName = reader.GetAttribute("article.name"),
								ID = reader.GetAttribute("idarticlecampaignrow"),
								Price = reader.GetAttribute("price"),
								ArticleNumber = reader.GetAttribute("article.articleno"),
							};

							string beforePrice = row.Price;
							try{row.Price = LimeHelper.ConvertToNumber(row.Price).ToString();}
							catch{row.Price = beforePrice;}

							rowData.Add(row);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return rowData;

		}

		public static List<NavigationCore.BusinessRow> ParseToBusinessRows(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType type = ServerQueryType.BusinessRows;

			List<NavigationCore.BusinessRow> rowData = new List<NavigationCore.BusinessRow>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							NavigationCore.BusinessRow row = new NavigationCore.BusinessRow()
							{
								ArticleID = reader.GetAttribute("article.idarticle"),
								BusinessID = reader.GetAttribute("business.idbusiness"),
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
								Price = reader.GetAttribute("price"),
								Quantity = reader.GetAttribute("quantity"),
								TotalPrice = reader.GetAttribute("totalprice"),
								ArticleName = reader.GetAttribute("article.name"),
								ArticleNr = reader.GetAttribute("article.articleno"),
							};

							string beforeQuantity = row.Quantity;
							try{row.Quantity = LimeHelper.ConvertToNumber(row.Quantity).ToString();}
							catch{row.Quantity = beforeQuantity;}

							string beforeTotalPrice = row.TotalPrice;
							try{row.TotalPrice = LimeHelper.ConvertToNumber(row.TotalPrice).ToString();}
							catch{row.TotalPrice = beforeTotalPrice;}

							string beforePrice = row.Price;
							try{row.Price = LimeHelper.ConvertToNumber(row.Price).ToString();}
							catch{row.Price = beforePrice;}

							string selectedValue = reader.GetAttribute("selected");

							row.Selected = selectedValue;

							rowData.Add(row);
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return rowData;

		}


		public static NavigationCore.CardBusinessData ParseToCardBusinessData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardBusinessData;

			NavigationCore.CardBusinessData cardBusinessData = (NavigationCore.CardBusinessData)ParseToCardData(XML, type, delegate(XmlReader reader, ServerQueryType queryType) {
				string tableName = LimeHelper.GetTableName(queryType);
				NavigationCore.CardBusinessData cardData = new NavigationCore.CardBusinessData() {
					ID = reader.GetAttribute("id" + tableName),
					OrderNr = reader.GetAttribute("businessid"),
					CompanyName = reader.GetAttribute("company.name"),
					CompanyID = reader.GetAttribute("company.idcompany"),
					CompanyDeliveryAddress = reader.GetAttribute("deliveryaddress1"),
					CompanyDeliveryAddress2 = reader.GetAttribute("deliveryaddress2"),
					CompanyDeliveryCity = reader.GetAttribute("deliverycity"),
					CompanyDeliveryName = reader.GetAttribute("deliveryname"),
					CompanyDeliveryZipCode = reader.GetAttribute("deliveryzipcode"),
					CompanyReferenceName = reader.GetAttribute("person.name"),
					CompanyReferenceID = reader.GetAttribute("person.idperson"),
					CoworkerName = reader.GetAttribute("coworker.name"),
					CoworkerID = reader.GetAttribute("coworker.idcoworker"),
					EmailConfirmation = reader.GetAttribute("email"),
					Mark = reader.GetAttribute("marking"),
					Note = reader.GetAttribute("note"),
					OrderDate = CutDateTimeToDateString(reader.GetAttribute("orderdate")),
					OrderDeliveryDate = CutDateTimeToDateString(reader.GetAttribute("deliverydate")),
					OrderEndDate = CutDateTimeToDateString(reader.GetAttribute("tenderduedate")),
					OrderStatus = reader.GetAttribute("businesstatus"),
					OrderTerms = reader.GetAttribute("paymentterms"),
					OrderType = reader.GetAttribute("ordertype")
				};
				return cardData;


			});

			return cardBusinessData;
		}


		public static NavigationCore.CardCampaignData ParseToCardCampaignData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardCampaignData;

			NavigationCore.CardCampaignData cardActivityData = (NavigationCore.CardCampaignData)ParseToCardData(XML, type, delegate(XmlReader reader, ServerQueryType queryType) {
				string tableName = LimeHelper.GetTableName(queryType);
				NavigationCore.CardCampaignData cardData =  new NavigationCore.CardCampaignData() {
					ID = reader.GetAttribute("id" + tableName),
					Description = reader.GetAttribute("description"),
					HeadLine = reader.GetAttribute("headline"),
				};
				cardData.StartDate = (DateTime)ParseStringToDateTime(reader.GetAttribute("startdate"));
				cardData.EndDate = (DateTime)ParseStringToDateTime(reader.GetAttribute("enddate"));
				return cardData;
			});

			return cardActivityData;
		}

		public static NavigationCore.CardActivityData ParseToCardActivityData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardActivityData;

			NavigationCore.CardActivityData cardActivityData = (NavigationCore.CardActivityData)ParseToCardData(XML, type, delegate(XmlReader reader, ServerQueryType queryType) {
				string tableName = LimeHelper.GetTableName(queryType);
				NavigationCore.CardActivityData cardData =  new NavigationCore.CardActivityData() {
					ID = reader.GetAttribute("id" + tableName),
					Note = reader.GetAttribute("note"),
					Type = reader.GetAttribute("type"),
					CoworkerID = reader.GetAttribute("coworker.idcoworker"),
				};
				cardData.Date = (DateTime)ParseStringToDateTime(reader.GetAttribute("date"));
				return cardData;
			});

			return cardActivityData;
		}

		public static NavigationCore.CardContactData ParseToCardContactData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardContactData;

			NavigationCore.CardContactData cardContactData = (NavigationCore.CardContactData)ParseToCardData(XML, type, delegate(XmlReader reader, ServerQueryType queryType) {
				string tableName = LimeHelper.GetTableName(queryType);
				return new NavigationCore.CardContactData() {
					ID = reader.GetAttribute("id" + tableName),
					CompanyName = reader.GetAttribute("company.name"),
					CompanyID = reader.GetAttribute("company.idcompany"),
					Email = reader.GetAttribute("email"),
					FirstName = reader.GetAttribute("firstname"),
					LastName = reader.GetAttribute("lastname"),
					Misc = reader.GetAttribute("misc"),
					MobilePhone = reader.GetAttribute("mobilephone"),
					Phone = reader.GetAttribute("phone"),
					Position = reader.GetAttribute("position"),
					Active = reader.GetAttribute("active"),
				};
			});

			return cardContactData;
		}

		public static NavigationCore.CardClientData ParseToCardClientData(string XML, ServerQueryType type)
		{
			XML = PrepareXMLString(XML);

			NavigationCore.CardClientData fullClientData = null;

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			List<string> fields = LimeHelper.GetFields(type);

			List<string> hideFields = LimeHelper.GetHideFields(type);
			hideFields.Add("id" + tableName);

			int nrOfShownFields = GetNrOfShownFields(fields, hideFields);
			List<string> fieldsWithEnumerations = null;

			if (LimeHelper.DatabaseData != null)
			{
				fieldsWithEnumerations = LimeHelper.DatabaseData.GetEnumerationFields(tableName);
			}
			else
			{
				fieldsWithEnumerations = new List<string>();
			}

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							int attributeCounter = 0;

							string[,] fullData = new string[2, nrOfShownFields];
							for (int i = 0; i < fields.Count; i++)
							{
								bool hideField = false;
								for (int j = 0; j < hideFields.Count; j++)
								{
									if (hideFields[j] == fields[i])
									{
										hideField = true;
										break;
									}
								}
								if (hideField)
									continue;

								string fieldNameToAsk = fields[i];


								string fieldName = fields[i];
								string fieldValue = reader.GetAttribute(fieldNameToAsk);



								if (LimeHelper.DatabaseData != null)
								{
									for (int j = 0; j < fieldsWithEnumerations.Count; j++) {
										if (fieldsWithEnumerations[j] == fieldName)
										{
											fieldValue = LimeHelper.DatabaseData.GetEnumerationName(tableName, fieldName, fieldValue);
											break;
										}
									}
									fieldName = LimeHelper.DatabaseData.GetFieldName(tableName, fieldName);

									fieldName = NavigationCore.ServerTranslator.TranslateField(NavigationCore.ServerTranslator.TranslateEnum.CardClient, fieldName);
								}

								if(fieldName == "Aktiv")
								{
									fieldValue = (fieldValue == "1") ? "Ja" : "Nej";
								}

								fullData[0, attributeCounter] = fieldName;
								fullData[1, attributeCounter] = fieldValue;
								attributeCounter++;
							}

							fullClientData = new NavigationCore.CardClientData(){ FieldData = fullData };
							fullClientData.CompanyId = reader.GetAttribute("id" + tableName);
							fullClientData.CreditWarning = reader.GetAttribute("creditwarning");

							double lon;
							double lat;
							bool gotCoordinates = true;
							try
							{
								lon = parseToDouble(reader.GetAttribute("gpslong"));
								lat = parseToDouble(reader.GetAttribute("gpslat"));
							}
							catch
							{
								lon = 0;
								lat = 0;
								gotCoordinates = false;
							}

							if(lon == 0 && lat == 0)
								gotCoordinates = false;

							fullClientData.Longitude = lon;
							fullClientData.Latitude = lat;
							fullClientData.GotCoordinates = gotCoordinates;
						}
						break;
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}

			}

			return fullClientData;
		}


		public static List<NavigationCore.ContactData> ParseToContactData(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataContact;

			List<NavigationCore.ContactData> contactDataList = new List<NavigationCore.ContactData>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							contactDataList.Add(new NavigationCore.ContactData()
							{
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
								Email = reader.GetAttribute("email"),
							});
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return contactDataList;
		}

		public static List<NavigationCore.PaymentTermsData> ParseToPaymentTermsData(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataPaymentTerms;

			List<NavigationCore.PaymentTermsData> paymentTermsDataList = new List<NavigationCore.PaymentTermsData>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							paymentTermsDataList.Add(new NavigationCore.PaymentTermsData()
							{
								Code = reader.GetAttribute("code"),
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
							});
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return paymentTermsDataList;
		}

		public static List<NavigationCore.OrderTypeData> ParseToOrderTypeData(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataOrderType;

			List<NavigationCore.OrderTypeData> orderTypeDataList = new List<NavigationCore.OrderTypeData>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							orderTypeDataList.Add(new NavigationCore.OrderTypeData()
							{
								Code = reader.GetAttribute("code"),
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
							});
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return orderTypeDataList;
		}

		public static bool ParseToSetTranslationData(string XML)
		{
			if(XML == null)
				return false;

			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataTranslation;

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				reader.ReadToFollowing("data");
				while (reader.ReadToFollowing(tableName))
				{
					try
					{

						if (reader.NodeType == XmlNodeType.Element)
						{
							var languages = new List<string>()
							{
								"sv",
								"en_us",
								"no",
								"fi"
							};
							var code = reader.GetAttribute("code");

							foreach (var language in languages)
							{
								var translation = reader.GetAttribute(language);

								NavigationCore.Translator.AddTranslation(language, code, translation);
							}

						}
						
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.StackTrace);
					}
				}
			}
			return true;
		}

		public static List<NavigationCore.CountryData> ParseToCountryData(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataCountry;

			List<NavigationCore.CountryData> countryDataList = new List<NavigationCore.CountryData>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							countryDataList.Add(new NavigationCore.CountryData()
							                    {
								Code = reader.GetAttribute("code"),
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
							});
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return countryDataList;

		}

		public static List<NavigationCore.CompanyCategoryData> ParseToCompanyCategoryData(string XML)
		{
			XML = PrepareXMLString(XML);
			ServerQueryType queryType = ServerQueryType.DataCompanyCategory;

			List<NavigationCore.CompanyCategoryData> companyCategoryDataList = new List<NavigationCore.CompanyCategoryData>();

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(queryType);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							companyCategoryDataList.Add(new NavigationCore.CompanyCategoryData()
							{
								Code = reader.GetAttribute("code"),
								ID = reader.GetAttribute("id" + tableName),
								Name = reader.GetAttribute("name"),
							});
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return companyCategoryDataList;

		}


		static Func<XmlReader, NavigationCore.ClientData> GetClientFillerFunction(ServerQueryType type)
		{
			Func<XmlReader, NavigationCore.ClientData> retFunc = null;

			string tableName = LimeHelper.GetTableName(type);

			switch(type)
			{
				case ServerQueryType.MapsRoute:
					retFunc = delegate(XmlReader reader)
					{
						try
						{
							NavigationCore.ClientData clientData = new NavigationCore.ClientData() {
								Address = reader.GetAttribute("company.deliveryaddress1"),
								City = reader.GetAttribute("company.deliverycity"),
								Email = reader.GetAttribute("company.email"),
								Name = reader.GetAttribute("company.name"),
								Phone = reader.GetAttribute("company.phone"),
								ZipCode = reader.GetAttribute("company.deliveryzipcode"),
								Id = reader.GetAttribute("company.id" + tableName),
								TimeOfMeeting = (DateTime)ParseStringToDateTime(reader.GetAttribute("starttime"))
							};

							double lon;
							double lat;
							try
							{
								lon = parseToDouble(reader.GetAttribute("company.gpslong"));
								lat = parseToDouble(reader.GetAttribute("company.gpslat"));
							}
							catch
							{
								lon = 0;
								lat = 0;
							}

							clientData.Longitude = lon;
							clientData.Latitude = lat;

							return clientData;
						}
						catch(Exception ex)
						{
							Console.WriteLine(ex.StackTrace);
							return null;
						}
					};
				break;
				case ServerQueryType.MapsNearbyClients:
					retFunc = delegate(XmlReader reader)
					{
						try
						{
							NavigationCore.ClientData clientData = new NavigationCore.ClientData() {
								Address = reader.GetAttribute("deliveryaddress1"),
								City = reader.GetAttribute("deliverycity"),
								Email = reader.GetAttribute("email"),
								Name = reader.GetAttribute("name"),
								Phone = reader.GetAttribute("phone"),
								ZipCode = reader.GetAttribute("deliveryzipcode"),
								Id = reader.GetAttribute("id" + tableName),
							};

							double lon;
							double lat;
							try
							{
								lon = parseToDouble(reader.GetAttribute("company.gpslong"));
								lat = parseToDouble(reader.GetAttribute("company.gpslat"));
							}
							catch
							{
								lon = 0;
								lat = 0;
							}

							clientData.Longitude = lon;
							clientData.Latitude = lat;

							return clientData;
						}
						catch(Exception ex)
						{
							Console.WriteLine(ex.StackTrace);
							return null;
						}
					};
				break;
			}
			return retFunc;
		}

		public static List<NavigationCore.ClientData> ParseToClientData(string XML, ServerQueryType type)
		{
			XML = PrepareXMLString(XML);

			List<NavigationCore.ClientData> clientData = new List<NavigationCore.ClientData>();

			Func<XmlReader, NavigationCore.ClientData> fillerFunction = GetClientFillerFunction(type);

			//Hämtar tabellnamn.
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							if (fillerFunction != null)
							{
								NavigationCore.ClientData client = fillerFunction(reader);
								if (client != null)
									clientData.Add(client);
							}
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}

			return clientData;
		}

		static NavigationCore.CardTodoData FillerForCardTodoData(XmlReader reader, ServerQueryType queryType)
		{
			string tableName = LimeHelper.GetTableName(queryType);
			NavigationCore.CardTodoData cardData =  new NavigationCore.CardTodoData() {
				ID = reader.GetAttribute("id" + tableName),
				CompanyID = reader.GetAttribute("company.idcompany"),
				CompanyName = reader.GetAttribute("company.name"),
				CoworkerID = reader.GetAttribute("coworker.idcoworker"),
				CoworkerName = reader.GetAttribute("coworker.name"),
				Note = reader.GetAttribute("note"),
				PersonID = reader.GetAttribute("person.idperson"),
				PersonName = reader.GetAttribute("person.name"),
				Subject = reader.GetAttribute("subject"),
				Done = reader.GetAttribute("done") == "1"
			};
			cardData.StartTime = (DateTime)ParseStringToDateTime(reader.GetAttribute("starttime"));

			DateTime? tryEndDate = ParseStringToDateTime(reader.GetAttribute("endtime"));

			if(tryEndDate != null)
				cardData.EndTime = (DateTime)tryEndDate;
			else
				cardData.EndTime = cardData.StartTime;

			return cardData;
		}

		public static NavigationCore.CardTodoData ParseToCardTodoData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardTodoData;

			NavigationCore.CardTodoData cardTodoData = (NavigationCore.CardTodoData)ParseToCardData(XML, type, FillerForCardTodoData);

			return cardTodoData;
		}

		public static List<NavigationCore.CardTodoData> ParseToListOfCardTodoData(string XML)
		{
			ServerQueryType type = ServerQueryType.CardTodoData;

			List<NavigationCore.CardTodoData> cardTodoDatas = new List<NavigationCore.CardTodoData>();

			XML = PrepareXMLString(XML);

			//Hämtar tabellnamn
			string tableName = LimeHelper.GetTableName(type);

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while (reader.ReadToFollowing(tableName))
					{
						try
						{
							cardTodoDatas.Add(FillerForCardTodoData(reader, type));
						}
						catch
						{

						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}
			}
			return cardTodoDatas;
		}

		public static NavigationCore.TablePopulator.TableDataBundle ParseToTableBundle(string XML, ServerQueryType queryType, bool withSearch = false)
		{
			//Förbereder XML-strängen.
			XML = PrepareXMLString(XML);
			NavigationCore.TablePopulator.TableDataBundle bundle = new NavigationCore.TablePopulator.TableDataBundle();


			//Hämtar vilka attribut som ska finnas i listan om angett.
			List<string> attributeNames = LimeHelper.GetFields(queryType, withSearch);

			List<string> extraFields = LimeHelper.GetExtraFields(queryType, withSearch);
			attributeNames.AddRange(extraFields);

			//Hämtar tabellnamn.
			string tableName = LimeHelper.GetTableName(queryType, withSearch);
			bundle.TableName = tableName;
			if(LimeHelper.DatabaseData != null)
			{
				bundle.TableName = LimeHelper.DatabaseData.GetTableName(bundle.TableName, true);
			}

			int maxRowCount = countOccurences(("<" + tableName), XML);
			maxRowCount = Math.Max(maxRowCount, 1);


			//Hämtar vilka fält som inte ska visas.
			List<string> fieldsToHide = LimeHelper.GetHideFields(queryType, withSearch);
			//fieldsToHide.Add("id" + tableName);

			//Temporär label som används för att se kolumnstorlekarna.
			UILabel label = new UILabel();

			//Parsar XML-strängen till kolumner och kolumndata.
			List<NavigationCore.TablePopulator.TableColumn> columnData = new List<NavigationCore.TablePopulator.TableColumn>();

			List<string> fieldsWithEnumerations = new List<string>();
			if (LimeHelper.DatabaseData != null)
			{
				fieldsWithEnumerations = LimeHelper.DatabaseData.GetEnumerationFields(tableName);
			}

			List<bool> gotEnumerations = new List<bool>();


			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("data");
					while(reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							if(attributeNames.Count > 0)
							{
								for(int i = 0; i < attributeNames.Count; i++)
								{
									bool viewField = true;
									for (int j = 0; j < fieldsToHide.Count; j++)
									{
										if(fieldsToHide[j] == attributeNames[i])
										{
											viewField = false;
											break;
										}
									}
									if(viewField)
									{
										var splitted = attributeNames[i].Split('.');
										var lastPoint = attributeNames[i];
										if(splitted.Length > 0)
										{
											lastPoint = splitted[splitted.Length - 1];
										}
										if (lastPoint.StartsWith("id"))
											viewField = false;

										string DisplayName = attributeNames[i];
										string textName = attributeNames[i];

										if (LimeHelper.DatabaseData != null)
										{
											DisplayName = LimeHelper.DatabaseData.GetFieldName(tableName, DisplayName);
										}
										DisplayName = NavigationCore.ServerTranslator.TranslateField(NavigationCore.ServerTranslator.TranslateEnum.CardClient, DisplayName);

										label.Text = DisplayName;
										int width = (int)label.IntrinsicContentSize.Width;
										if(queryType == ServerQueryType.ArticleSearch)
										{
											if(textName == "name")
												width = Math.Max(450, width);
											else if(textName == "articleno")
												width = Math.Max(230, width);
										}

										columnData.Add(new NavigationCore.TablePopulator.TableColumn() { OriginalText = textName, DisplayText = DisplayName, Width = width, Secret = !viewField });

										bool gotEnumeration = false;
										for (int j = 0; j < fieldsWithEnumerations.Count; j++)
										{
											if(fieldsWithEnumerations[j] == columnData.Last().OriginalText)
											{
												gotEnumeration = true;
												break;
											}
										}

										gotEnumerations.Add(gotEnumeration);
									}
								}
							}
							else
							{
								while(reader.MoveToNextAttribute())
								{
									bool viewField = true;
									for (int j = 0; j < fieldsToHide.Count; j++)
									{
										if(fieldsToHide[j] == reader.Name)
										{
											viewField = false;
											break;
										}
									}
									if(viewField)
									{
										var splitted = reader.Name.Split('.');
										var lastPoint = reader.Name;
										if(splitted.Length > 0)
										{
											lastPoint = splitted[splitted.Length - 1];
										}
										if (lastPoint.StartsWith("id"))
											viewField = false;


										string DisplayName = reader.Name;

										if (LimeHelper.DatabaseData != null)
										{
											DisplayName = LimeHelper.DatabaseData.GetFieldName(tableName, DisplayName);
										}

										label.Text = DisplayName;
										int width =  (int)label.IntrinsicContentSize.Width;

										columnData.Add(new NavigationCore.TablePopulator.TableColumn() { OriginalText = reader.Name, DisplayText = DisplayName, Width = width, Secret = !viewField });

										bool gotEnumeration = false;
										for (int j = 0; j < fieldsWithEnumerations.Count; j++)
										{
											if(fieldsWithEnumerations[j] == columnData.Last().OriginalText)
											{
												gotEnumeration = true;
												break;
											}
										}

										gotEnumerations.Add(gotEnumeration);
									}
								}
							}
							break;
						}
					}
				}
				catch(Exception ex)
				{
					columnData.Add(new NavigationCore.TablePopulator.TableColumn() { OriginalText = "(TEMP) Felmeddelande: " + ex.Message });
				}

			}



			//Parsar XML-strängen och fyller tabelldatan med rader.
			List<List<string>> contentData = new List<List<string>>();
			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				int elementCount = 0;
				try
				{
					reader.ReadToFollowing("data");
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element && reader.LocalName == tableName)
						{
							contentData.Add(new List<string>());
							for(int i = 0; i < columnData.Count; i++)
							{
								string attribute = reader.GetAttribute(columnData[i].OriginalText);

								if (gotEnumerations[i])
								{
									attribute = LimeHelper.DatabaseData.GetEnumerationName(tableName, columnData[i].OriginalText, attribute);
								}

								if (columnData[i].OriginalText.EndsWith("price1") ||
									columnData[i].OriginalText.EndsWith("price2") ||
									columnData[i].OriginalText.EndsWith("price3") ||
									columnData[i].OriginalText.EndsWith("price4") ||
									columnData[i].OriginalText.EndsWith("price5") ||
									columnData[i].OriginalText.EndsWith("price6") ||
									columnData[i].OriginalText.EndsWith("price7") ||
									columnData[i].OriginalText.EndsWith("price8") ||
									columnData[i].OriginalText.EndsWith("totalprice") ||
									columnData[i].OriginalText.EndsWith("quantity") ||
									columnData[i].OriginalText.EndsWith("price") ||
									columnData[i].OriginalText.EndsWith("businessvalue"))
								{

									int nrOfDecimals = 0;

									if(columnData[i].OriginalText.Contains("price") || columnData[i].OriginalText.Contains("businessvalue"))
										nrOfDecimals = 2;

									string attrBeforeParse = attribute;
									try
									{
										float nr = LimeHelper.ConvertToNumber(attribute);
										attribute = nr.ToString();
									}
									catch
									{
										attribute = attrBeforeParse;
									}


									attribute = BigNumberDelimiter(attribute, nrOfDecimals);
								}
								else if(columnData[i].OriginalText == "document__size")
								{
									string[] units = new string[]{"B", "kB", "MB", "GB", "TB"};
									int t = (int)Math.Floor((float)((attribute.Length - 1) / 3));
									if (t > 0)
										attribute = Math.Round(int.Parse(attribute) / (Math.Pow(1000, t))).ToString();
									attribute += " " + units[t];
								}
								else if(columnData[i].OriginalText.Contains("active"))
								{
									attribute = attribute == "1" ? "Ja" : "Nej";
								}
								else if(columnData[i].OriginalText.Contains("orderdate") || columnData[i].OriginalText.Contains("startdate") || columnData[i].OriginalText.Contains("enddate"))
								{
									attribute = CutDateTimeToDateString(attribute);
								}
								else if (queryType == ServerQueryType.ActivityAlarms && columnData[i].OriginalText == "latesthistory")
								{
									if (attribute != null)
									{		
										DateTime dateObj = (DateTime)ParseStringToDateTime(attribute);

										DateTime Now = DateTime.Now;
										attribute = String.Format("{0} dagar sedan", Math.Floor( (DateTime.Now - dateObj).TotalDays ));
									}
									else
									{
										attribute = "Aldrig";
									}
								}

								label.Text = attribute;
								columnData[i].Width = (int)Math.Max(label.IntrinsicContentSize.Width, columnData[i].Width);
								contentData[contentData.Count - 1].Add(attribute);
							}
							elementCount += 1;
							if(elementCount >= maxRowCount)
								break;
						}
					}
				}
				catch(Exception ex)
				{
					contentData.Add(new List<string>() { "(TEMP) Felmeddelande: " + ex.Message });
				}

			}

			label = null;

			bundle.Content = contentData;
			bundle.Columns = columnData;


			return bundle;
		}

		public static LimeService.UploadDataResult ParseToUploadDataResult(string XML, ServerQueryType type)
		{
			XML = PrepareXMLString(XML);
			string transactionId = null;
			string tableName = LimeHelper.GetTableName(type);
			string newID = null;
			bool isCreated = false;
			bool isEdited = true;
			bool success = false;
			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					reader.ReadToFollowing("UpdateDataResult");
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "xml")
							{
								transactionId = reader.GetAttribute("transactionid");
							}
							else if (reader.LocalName == "record")
							{
								if (reader.GetAttribute("table") == tableName)
								{
									string oldId = reader.GetAttribute("idold");
									if (oldId != null && int.Parse(oldId) < 0)
									{
										newID = reader.GetAttribute("idnew");
										break;
									}
								}
							}

						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
					return LimeService.UploadDataResult.ErrorResult();
				}

			}

			if(transactionId != null)
				success = true;
			if(newID != null)
			{
				isCreated = true;
				isEdited = false;
			}

			return new LimeService.UploadDataResult() { TransactionId = transactionId, NewId = newID, Success = success, TableName = tableName, IsCreated = isCreated, IsEdited = isEdited };
		}

		public static DatabaseStructure.DatabaseData ParseDataStructure(string XML)
		{
			XML = PrepareXMLString(XML);
			DatabaseStructure.DatabaseData db = new DatabaseStructure.DatabaseData() { TableData = new List<DatabaseStructure.TableData>() };
			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					while(reader.ReadToFollowing("table"))
					{
						string tableName = reader.GetAttribute("name");
						string translatedTableName = null;
						string translatedPluralTableName = null;
						if (NavigationCore.ServerTranslator.ContainsTable(tableName))
						{
							translatedTableName = NavigationCore.ServerTranslator.TranslateTable(tableName, false);//reader.GetAttribute("localname");
							translatedPluralTableName = NavigationCore.ServerTranslator.TranslateTable(tableName, true);//reader.GetAttribute("localnameplural");
						}
						else
						{
							translatedTableName = reader.GetAttribute("localname");
							translatedPluralTableName = reader.GetAttribute("localnameplural");
						}


						db.TableData.Add(new DatabaseStructure.TableData(){Fields = new List<DatabaseStructure.FieldData>(), TableName = tableName, TranslatedName = translatedTableName, TranslatedNamePlural = translatedPluralTableName});

						bool fieldRead = reader.ReadToDescendant("field");
						while(fieldRead)
						{
							string fieldName = reader.GetAttribute("name");
							string filedTranslatedName = reader.GetAttribute("localname");;

							if (NavigationCore.ServerTranslator.ContainsField(NavigationCore.ServerTranslator.TranslateEnum.PriceGroups, filedTranslatedName))
							{
								filedTranslatedName = NavigationCore.ServerTranslator.TranslateField(NavigationCore.ServerTranslator.TranslateEnum.PriceGroups, filedTranslatedName);
							}

							db.TableData.Last().Fields.Add(new DatabaseStructure.FieldData() {FieldName = fieldName, TranslatedName = filedTranslatedName, Enumerations = new List<DatabaseStructure.TableEnumeration>()});
							string type = reader.GetAttribute("fieldtype");
							if (type == "21")
							{
								bool stringRead = reader.ReadToDescendant("string");
								while(stringRead)
								{
									string text = reader.GetAttribute("text");
									if (!(text.Trim() == ""))
										db.TableData.Last().Fields.Last().Enumerations.Add(new DatabaseStructure.TableEnumeration() {Id = reader.GetAttribute("idstring"), Key = reader.GetAttribute("key"), Text = text});
									stringRead = reader.ReadToNextSibling("string");
								}

							}
							fieldRead = reader.ReadToNextSibling("field");
						}
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
				}

			}
			return db;
		}

		static string SaveFile(string fileName, string content)
		{
			string documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string tmp = Path.Combine (documents, "..", "tmp");
			string wholePath = Path.Combine(tmp, fileName);

			byte[] bytes = Convert.FromBase64String(content);

			using (FileStream file = new FileStream(wholePath, FileMode.Create))
			{
				file.Write(bytes, 0, bytes.Length);
			}
			return wholePath;
		}

		public static List<string> ParseToFile(string XML, ServerQueryType type)
		{
			XML = PrepareXMLString(XML);
			string tableName = LimeHelper.GetTableName(type);

			List<string> filePaths = new List<string>();

			using (XmlReader reader = XmlReader.Create(new StringReader(XML)))
			{
				try
				{
					while(reader.ReadToFollowing(tableName))
					{
						string fileName = reader.GetAttribute("iddocument");
						string fileExtension = reader.GetAttribute("document__fileextension");
						string contentOfFile = reader.GetAttribute("document__data");
						if (fileName != null && fileExtension != null && contentOfFile != null)
						{
							string fullFileName = fileName + "." + fileExtension;
							string wholePath = SaveFile(fullFileName, contentOfFile);
							filePaths.Add(wholePath);
						}

					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.StackTrace);
					return null;
				}

			}
			return filePaths;
		}
		*/
	}
}

