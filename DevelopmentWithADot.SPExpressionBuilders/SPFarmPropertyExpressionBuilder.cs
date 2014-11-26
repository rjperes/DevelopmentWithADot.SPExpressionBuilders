using System;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.SharePoint.Administration;

namespace DevelopmentWithADot.SPExpressionBuilders
{
	[ExpressionPrefix("SPFarmProperty")]
	public sealed class SPFarmPropertyExpressionBuilder : ConvertedExpressionBuilder
	{
		#region Public static methods
		public static Object GetValue(String propertyName, Type propertyType)
		{
			var propertyValue = SPFarm.Local.Properties[propertyName];

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
