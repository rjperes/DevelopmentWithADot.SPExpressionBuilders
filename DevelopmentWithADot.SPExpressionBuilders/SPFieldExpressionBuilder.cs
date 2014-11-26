using System;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPField")]
	public sealed class SPFieldExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String fieldName, Type propertyType)
		{
			var fieldValue = SPContext.Current.ListItem[fieldName];

			return (Convert(fieldValue, propertyType));
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
