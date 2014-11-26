using System;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPPublishingPage")]
	public sealed class SPPublishingPageExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String expression, Type propertyType)
		{
			var page = PublishingPage.GetPublishingPage(SPContext.Current.ListItem);
			var expressionValue = DataBinder.Eval(page, expression.Trim().Replace('\'', '"'));

			return (Convert(expressionValue, propertyType));
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
