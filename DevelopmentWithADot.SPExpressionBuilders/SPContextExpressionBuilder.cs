using System;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPContext")]
	public sealed class SPContextExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String expression, Type propertyType)
		{
			var context = SPContext.Current;
			var expressionValue = DataBinder.Eval(context, expression.Trim().Replace('\'', '"'));

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
