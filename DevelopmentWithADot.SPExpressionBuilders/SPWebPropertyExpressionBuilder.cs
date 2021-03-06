﻿using System;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPWebProperty")]
	public sealed class SPWebPropertyExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String propertyName, Type propertyType)
		{
			var parts = propertyName.Split('/');
			var web = SPContext.Current.Web;

			foreach (var part in parts)
			{
				web = web.Webs[part];
			}

			var propertyValue = web.AllProperties[propertyName];

			return (Convert(propertyValue, propertyType));
		}

		#endregion

		#region Public override methods
		public override Object EvaluateExpression(Object target, BoundPropertyEntry entry, Object parsedData, ExpressionBuilderContext context)
		{
			return (GetValue(entry.Expression, entry.PropertyInfo.PropertyType));
		}

		#endregion
	}
}
