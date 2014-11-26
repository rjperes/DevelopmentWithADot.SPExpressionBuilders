using System;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPQueryString")]
	public sealed class SPQueryStringExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String parameterName, Type propertyType)
		{
			var parameterValue = HttpContext.Current.Request.QueryString[parameterName];

			return (Convert(parameterValue, propertyType));
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
